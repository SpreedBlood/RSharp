using System.Threading.Tasks;
using RSharp.API.Handles;
using RSharp.API.Sessions;
using RSharp.Chat.Handles.Args;
using RSharp.Chat.Handles.Composers;

namespace RSharp.Chat.Handles
{
    internal class OnPlayerChat : AbstractHandle<ChatArgs>
    {
        public override int OpCode => 4;

        public override async Task Invoke(ISession session, ChatArgs args)
        {
            await session.WriteAndFlushAsync(new SendChatComposer("Testing!"));
        }
    }
}
