using RSharp.API.Packets;
using RSharp.API.Sessions;
using System.Threading.Tasks;

namespace RSharp.API.Handles
{
    public interface IAsyncHandle
    {
        /// <summary>
        /// The op code that the handle is supposed to serve.
        /// </summary>
        int OpCode { get; }

        /// <summary>
        /// Invokes the callback asynchronously & passes the TArgs, if TArgs is of type
        /// <see cref="EmptyArgs"/> then the invoke will be called with <see langword="null"/>
        /// as arguments.
        /// </summary>
        /// <param name="session">The session to invoke the method for.</param>
        /// <param name="clientPacket">The incoming data.</param>
        /// <returns>The callback handled upon task completion.</returns>
        Task Handle(ISession session, IClientPacket incomingData);
    }
}