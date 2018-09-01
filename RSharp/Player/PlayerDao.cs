using RSharp.API.Players;
using RSharp.API.SQL;
using RSharp.Player.Models;
using System.Threading.Tasks;

namespace RSharp.Player
{
    internal class PlayerDao : BaseDao
    {
        internal async Task<RegisterMessage> TryRegister(string username, string password)
        {
            RegisterMessage success = RegisterMessage.SUCCESS;
            await CreateTransaction(async transaction =>
            {
                int id = await Insert(transaction, "INSERT INTO players(username, password) VALUES(@0, @1)", username, password);

                //Failed!
                if (id == -1) success = RegisterMessage.USERNAME_IN_USE;
            });

            return success;
        }

        internal async Task<IPlayer> GetPlayer(string username, string password)
        {
            IPlayer player = null;
            await CreateTransaction(async transaction =>
            {
                await Select(transaction, async reader =>
                {
                    if (await reader.ReadAsync())
                    {
                        player = new PlayerData(reader);
                    }
                }, "SELECT id, username, password FROM players WHERE username = @0 AND password = @1 LIMIT 1", username, password);
            });

            return player;
        }

        internal async Task<bool> UserExists(string username)
        {
            bool userExists = false;
            await CreateTransaction(async transaction =>
            {
                await Select(transaction, async reader =>
                {
                    userExists = await reader.ReadAsync();
                }, "SELECT id FROM players WHERE username = @0 LIMIT 1", username);
            });

            return userExists;
        }
    }
}
