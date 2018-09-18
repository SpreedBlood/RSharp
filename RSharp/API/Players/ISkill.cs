namespace RSharp.API.Players
{
    public interface ISkill
    {
        /// <summary>
        /// Returns the id of the skill.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Return the amount of xp that this skill has.
        /// </summary>
        int XP { get; }

        /// <summary>
        /// Returns the level of the skill.
        /// </summary>
        int Level { get; }
    }
}
