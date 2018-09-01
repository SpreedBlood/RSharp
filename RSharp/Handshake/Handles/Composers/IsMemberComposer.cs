using RSharp.API.Packets;

namespace RSharp.Handshake.Handles.Composers
{
    internal class IsMemberComposer : ServerPacket
    {
        public IsMemberComposer(bool isMember)
        {
            WriteByte(isMember ? (byte)1 : (byte)0);
        }
    }
}