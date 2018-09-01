using RSharp.API.Players;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSharp.Player
{
    internal class PlayerRepository
    {
        private readonly PlayerDao _playerDao;
        private readonly IDictionary<(string, string), IPlayer> _cachedPlayers;
        private readonly IList<string> _cachedUsernames;

        public PlayerRepository(PlayerDao playerDao)
        {
            _playerDao = playerDao;
            _cachedPlayers = new Dictionary<(string, string), IPlayer>();
            _cachedUsernames = new List<string>();
        }

        /// <summary>
        /// Tries to login with the given details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Player entry upon task completion.</returns>
        internal async Task<IPlayer> GetPlayer(string username, string password)
        {
            if (_cachedPlayers.TryGetValue((username, password), out IPlayer player))
            {
                if (!_cachedUsernames.Contains(username)) _cachedUsernames.Add(username);
                return player;
            }

            IPlayer playerEntry = await _playerDao.GetPlayer(username, password);
            if (playerEntry != null) //Make sure to cache the username for later use.
                _cachedUsernames.Add(playerEntry.Username);

            return playerEntry;
        }

        internal async Task<bool> TryLogin(string username, string password)
        {
            if (_cachedPlayers.ContainsKey((username, password)))
            {
                return true;
            }

            IPlayer player = await GetPlayer(username, password);
            if (player != null) //Make sure we cache the entry to fetch the player instance easy lateron.
            {
                _cachedPlayers.Add((player.Username, player.Password), player);
                _cachedUsernames.Add(player.Username);
            }

            return player != null;
        }
        
        /// <summary>
        /// Caches the player in the dictionary, TODO: Make sure to insert the new fields
        /// in the database.
        /// </summary>
        /// <param name="player">The player entry to cache.</param>
        internal void CachePlayer(IPlayer player)
        {
            _cachedPlayers.Add((player.Username, player.Password), player);
        }

        /// <summary>
        /// Tries to register, if it fails, then try to login & returns the player entry.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The player upon task completion.</returns>
        internal async Task<bool> TryRegister(string username, string password)
        {
            RegisterMessage regMsg = await _playerDao.TryRegister(username, password);
            if (regMsg == RegisterMessage.USERNAME_IN_USE)
                return false;

            return await TryLogin(username, password);
        }

        /// <summary>
        /// Checks if the username exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal async Task<bool> UserExists(string username)
        {
            if (_cachedUsernames.Contains(username))
                return true;

            return await _playerDao.UserExists(username);
        }
    }
}