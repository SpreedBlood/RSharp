using System.Threading.Tasks;

namespace RSharp.API.Players
{
    public interface IPlayerController
    {
        /// <summary>
        /// Will try to register!
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The register result upon task completion.</returns>
        Task<bool> TryRegister(string username, string password);

        /// <summary>
        /// Tries to login with the given details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The login result upon task completion.</returns>
        Task<bool> TryLogin(string username, string password);

        /// <summary>
        /// Checks if the user exists.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The result upon task completion.</returns>
        Task<bool> UserExists(string username);

        /// <summary>
        /// Gets the player with the given details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The player entry upon task completion.</returns>
        Task<IPlayer> GetPlayer(string username, string password);
    }
}