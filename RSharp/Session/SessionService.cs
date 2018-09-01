using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Sessions;

namespace RSharp.Session
{
    internal class SessionService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<SessionRepository>();
            collection.AddSingleton<ISessionController, SessionController>();
        }
    }
}
