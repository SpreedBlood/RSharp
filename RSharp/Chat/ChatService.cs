using Microsoft.Extensions.DependencyInjection;
using RSharp.API;
using RSharp.API.Handles;
using RSharp.Chat.Handles;

namespace RSharp.Chat
{
    internal class ChatService : IService
    {
        public void SetupService(IServiceCollection collection)
        {
            collection.AddSingleton<IAsyncHandle, OnPlayerChat>();
        }
    }
}
