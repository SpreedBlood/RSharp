using RSharp.API.Entity;
using RSharp.API.Players;
using RSharp.API.Players.Equipment;
using RSharp.API.Players.Skill;
using RSharp.API.SQL;
using RSharp.Player.Models.Components.Equipment;
using RSharp.Player.Models.Components.Prayer;
using RSharp.Player.Models.Components.Skill;
using System.Collections.Generic;
using System.Data.Common;

namespace RSharp.Player.Models
{
    internal class PlayerData : WalkingEntity, IPlayer
    {
        private readonly EquipmentComponent _equipmentComponent;
        private readonly PrayerComponent _prayerComponent;
        private readonly SkillComponent _skillComponent;

        public uint Id { get; }
        public string Username { get; }
        public string Password { get; set; }
        public IList<ISkill> Skills => _skillComponent.Skills;
        public IList<IEquipment> Equipment => _equipmentComponent.Equipment;

        internal PlayerData(DbDataReader reader)
            : base(new Position(3093, 3244))
        {
            Id = reader.ReadData<uint>("id");
            Username = reader.ReadData<string>("username");
            Password = reader.ReadData<string>("password");

            _equipmentComponent = new EquipmentComponent();
            _prayerComponent = new PrayerComponent();
            _skillComponent = new SkillComponent();
        }
    }
}