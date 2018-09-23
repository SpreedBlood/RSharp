using RSharp.API.Packets;
using RSharp.API.Players.Skill;

namespace RSharp.Player.Handles.Composers
{
    internal class SkillLevelComposer : ServerPacket
    {
        public SkillLevelComposer(ISkill skill)
            : base(134)
        {
            WriteByte(skill.Id);
            WriteMiddleInt(skill.XP);
            WriteByte(skill.Level);
        }
    }
}
