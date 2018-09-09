using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Handles;
using RSharp.API.Network;
using RSharp.Chat;
using RSharp.Handshake;
using RSharp.Network;
using RSharp.Player;
using RSharp.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSharp
{
    class Program
    {
        private readonly IServiceProvider _serviceProvider;

        private Program()
        {
            IList<IService> services = new List<IService>
            {
                new NetworkService(),
                new HandshakeService(),
                new PlayerService(),
                new SessionService(),
                new ChatService()
            };

            IServiceCollection serviceCollection = new ServiceCollection();
            foreach (IService service in services)
            {
                service.SetupService(serviceCollection);
            }

            serviceCollection.AddSingleton<IHandleProvider, HandleProvider>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private async Task Run()
        {
            INetworkListener listener = _serviceProvider.GetService<INetworkListener>();
            await listener.Listen(43594);

            Console.ReadKey();
        }

        static Task Main() => new Program().Run();
    }
}