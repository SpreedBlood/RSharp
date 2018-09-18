using RSharp.API.Entity;
using RSharp.API.Players;
using RSharp.API.SQL;
using RSharp.Player.Models.Components.Skill;
using System.Collections.Generic;
using System.Data.Common;

namespace RSharp.Player.Models
{
    internal class PlayerData : WalkingEntity, IPlayer
    {
        public uint Id { get; }
        public string Username { get; }
        public string Password { get; set; }
        public IList<ISkill> Skills => _skillComponent.Skills;

        private readonly SkillComponent _skillComponent;

        internal PlayerData(DbDataReader reader)
            : base(new Position(3093, 3244))
        {
            Id = reader.ReadData<uint>("id");
            Username = reader.ReadData<string>("username");
            Password = reader.ReadData<string>("password");

            _skillComponent = new SkillComponent();
        }
    }
}