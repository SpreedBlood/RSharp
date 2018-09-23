using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.World;

namespace RSharp.World
{
    internal class WorldService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<IWorld, World>();
        }
    }
}
