using RSharp.API.Players;
using RSharp.API.SQL;
using System.Data.Common;

namespace RSharp.Player.Models
{
    internal class PlayerData : IPlayer
    {
        public uint Id { get; }
        public string Username { get; }
        public string Password { get; set; }

        internal PlayerData(DbDataReader reader)
        {
            Id = reader.ReadData<uint>("id");
            Username = reader.ReadData<string>("username");
            Password = reader.ReadData<string>("password");
        }
    }
}