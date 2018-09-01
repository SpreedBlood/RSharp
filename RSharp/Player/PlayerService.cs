using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Players;

namespace RSharp.Player
{
    internal class PlayerService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<PlayerDao>();
            collection.AddSingleton<PlayerRepository>();
            collection.AddSingleton<IPlayerController, PlayerController>();
        }
    }
}
