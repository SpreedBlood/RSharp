using RSharp.API.Packets;
using RSharp.API.Sessions;
using System.Threading.Tasks;

namespace RSharp.API.Handles
{
    public interface IHandleProvider
    {
        /// <summary>
        /// Handles the given packet asynchronously.
        /// </summary>
        /// <param name="session">The session that's incoming.</param>
        /// <param name="clientPacket">The packet associated with the incoming data.</param>
        /// <returns>The packet handled upon task completion.</returns>
        Task Handle(ISession session, IClientPacket clientPacket);
    }
}