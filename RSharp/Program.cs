using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Handles;
using RSharp.API.Network;
using RSharp.Chat;
using RSharp.Network;
using RSharp.Player;
using RSharp.Session;
using RSharp.Uv;
using RSharp.World;
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
            Loop loop = new Loop();
            Timer timer = loop.NewTimer(() =>
            {
                Console.WriteLine(DateTime.Now);
            });

            timer.Start(0, 500);
            loop.RunDefault();

            IList<IService> services = new List<IService>
            {
                new NetworkService(),
                new PlayerService(),
                new SessionService(),
                new ChatService(),
                new WorldService()
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