using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Handles;
using RSharp.API.Players;
using RSharp.Player.Handles;

namespace RSharp.Player
{
    internal class PlayerService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<PlayerDao>();
            collection.AddSingleton<PlayerRepository>();
            collection.AddSingleton<IPlayerController, PlayerController>();

            AddPackets(collection);
        }

        private static void AddPackets(IServiceCollection collection)
        {
            //collection.AddSingleton<IAsyncHandle, PostLoginHandle>();
        }
    }
}
