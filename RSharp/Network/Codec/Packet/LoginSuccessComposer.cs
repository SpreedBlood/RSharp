using RSharp.API.Packets;

namespace RSharp.Network.Codec.Packet
{
    internal class LoginSuccessComposer : ServerPacket
    {
        public LoginSuccessComposer()
        {
            WriteByte(2);
            WriteByte(2);

            WriteByte(0);
        }
    }
}
