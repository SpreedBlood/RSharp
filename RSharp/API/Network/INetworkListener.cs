using System.Threading.Tasks;

namespace RSharp.API.Network
{
    public interface INetworkListener
    {
        /// <summary>
        /// Starts to listen on the given port.
        /// </summary>
        /// <param name="port">The port to listen</param>
        /// <returns>Trying to setup a listener upon task completion.</returns>
        Task Listen(int port);
    }
}
