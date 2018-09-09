using RSharp.API.Handles;
using RSharp.API.Packets;

namespace RSharp.Handshake.Handles.Args
{
    internal class ChatArgs : HandleArgs
    {
        internal int TextEffects { get; private set; }
        internal int TextColor { get; private set; }
        internal int TextSize { get; private set; }

        public override void ParseArgs(IClientPacket clientPacket)
        {
            TextEffects = clientPacket.ReadUByteS();
            TextColor = clientPacket.ReadUByteS();
            TextSize = clientPacket.Size - 2;
        }
    }
}
