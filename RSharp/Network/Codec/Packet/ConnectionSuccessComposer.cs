using RSharp.API.Packets;

namespace RSharp.Network.Codec.Packet
{
    internal class ConnectionSuccessComposer : ServerPacket
    {
        public ConnectionSuccessComposer()
            : base(-1)
        {
            WriteLong(0);
            WriteByte(0);
            WriteLong(SecureRandom.NextLong());
        }
    }
}
