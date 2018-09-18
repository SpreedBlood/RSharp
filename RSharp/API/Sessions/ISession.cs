using RSharp.API.Packets;
using RSharp.API.Players;
using System.Threading.Tasks;

namespace RSharp.API.Sessions
{
    public interface ISession
    {
        /// <summary>
        /// Gets the player instance associated with the session. This is null
        /// until logged in.
        /// </summary>
        IPlayer Player { get; set; }

        /// <summary>
        /// Writes the packet asynchronously.
        /// </summary>
        /// <param name="serverPacket">The packet to write.</param>
        /// <returns>Written the packet upon task completion.</returns>
        Task WriteAsync(ServerPacket serverPacket);

        /// <summary>
        /// Writes and flushes all the written packets asynchronously.
        /// </summary>
        /// <param name="serverPacket">The packet to write and flush.</param>
        /// <returns>Written and flushed all the packets upon task completion.</returns>
        Task WriteAndFlushAsync(ServerPacket serverPacket);

        /// <summary>
        /// Flushes the queue that's associated with the session.
        /// </summary>
        void Flush();

        /// <summary>
        /// Closes the client asynchronously.
        /// </summary>
        /// <returns>Closes the client upon task completion.</returns>
        Task CloseAsync();
    }
}