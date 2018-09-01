using RSharp.API.Packets;
using RSharp.Network.Codec;

namespace RSharp.Handshake.Handles.Composers
{
    internal class InitializeFrameComposer : ServerPacket
    {
        public InitializeFrameComposer(int id, ISAACCipher cipher)
        {
            WriteEncByte((byte)id, cipher);
        }
    }
}