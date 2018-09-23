using RSharp.API.Players.Skill;
using System.Collections.Generic;

namespace RSharp.Player.Models.Components.Skill
{
    internal class SkillComponent
    {
        internal IList<ISkill> Skills { get; }

        internal SkillComponent()
        {
            Skills = new List<ISkill>(SkillData.Amount);

            //Initalizes the skills.
            for (byte i = 0; i < SkillData.Amount; i++)
            {
                Skills.Add(new Skill(i));
            }
        }
    }
}
