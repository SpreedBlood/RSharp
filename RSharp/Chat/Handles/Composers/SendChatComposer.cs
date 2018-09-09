using RSharp.API.Packets;

namespace RSharp.Chat.Handles.Composers
{
    public class SendChatComposer : ServerPacket
    {
        public SendChatComposer(string test)
            : base(253)
        {
            WriteString(test);
        }
    }
}
