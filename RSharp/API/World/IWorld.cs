using RSharp.API.Sessions;

namespace RSharp.API.World
{
    public interface IWorld
    {
        /// <summary>
        /// Adds a player to the world.
        /// </summary>
        /// <param name="session">The player to add.</param>
        void AddSession(ISession session);

        /// <summary>
        /// Tries to remove the player from the world.
        /// </summary>
        /// <param name="session">The player to remove.</param>
        /// <returns>The remove success/fail.</returns>
        bool RemoveSession(ISession session);
    }
}
