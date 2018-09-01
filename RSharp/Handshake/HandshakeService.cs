using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Handles;
using RSharp.Handshake.Handles;

namespace RSharp.Handshake
{
    internal class HandshakeService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            RegisterPackets(collection);
        }

        private static void RegisterPackets(IServiceCollection collection)
        {
            collection.AddSingleton<IAsyncHandle, PostLoginHandle>();
        }
    }
}
