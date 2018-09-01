using RSharp.API.Players;
using System.Threading.Tasks;

namespace RSharp.Player
{
    internal class PlayerController : IPlayerController
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<bool> TryRegister(string username, string password) =>
            _playerRepository.TryRegister(username, password);

        public void CachePlayer(IPlayer player) =>
            _playerRepository.CachePlayer(player);

        public Task<bool> TryLogin(string username, string password) =>
            _playerRepository.TryLogin(username, password);

        public Task<bool> UserExists(string username) =>
            _playerRepository.UserExists(username);

        public Task<IPlayer> GetPlayer(string username, string password) =>
            _playerRepository.GetPlayer(username, password);
    }
}