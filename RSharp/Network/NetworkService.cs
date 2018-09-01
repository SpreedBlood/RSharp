using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Network;

namespace RSharp.Network
{
    internal class NetworkService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<NetworkInitializer>();
            collection.AddSingleton<INetworkListener, NetworkListener>();
        }
    }
}
