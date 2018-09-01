using RSharp.API.Handshake;
using RSharp.API.Packets;

namespace RSharp.Network.Codec.Packet
{
    internal class LoginErrorComposer : ServerPacket
    {
        public LoginErrorComposer(LoginMessage code)
        {
            WriteByte((byte)code);
        }
    }
}