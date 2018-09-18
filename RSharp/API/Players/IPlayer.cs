using RSharp.API.Entity;
using System.Collections.Generic;

namespace RSharp.API.Players
{
    public interface IPlayer
    {
        /// <summary>
        /// Gets the unique id of the player.
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// Gets the username of the player.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the password of the player.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets the skills of the player.
        /// </summary>
        IList<ISkill> Skills { get; }

        /// <summary>
        /// Gets the current position of the player.
        /// </summary>
        IPosition Position { get; }
    }
}
