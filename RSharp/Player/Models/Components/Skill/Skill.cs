using RSharp.API.Players;
using RSharp.API.Players.Skill;

namespace RSharp.Player.Models.Components.Skill
{
    internal class Skill : ISkill
    {
        public int Id { get; }
        public int XP { get; private set; }
        public int Level { get; private set; }

        internal Skill(int id)
        {
            Id = id;
            XP = 0;
            Level = 10;
        }
    }
}
