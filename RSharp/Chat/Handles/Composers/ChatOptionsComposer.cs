using RSharp.API.Packets;

namespace RSharp.Chat.Handles.Composers
{
    public class ChatOptionsComposer : ServerPacket
    {
        public ChatOptionsComposer(int publicChat, int privateChat, int tradeBlock)
            : base(206)
        {
            WriteByte(publicChat);
            WriteByte(privateChat);
            WriteByte(tradeBlock);
        }
    }
}
