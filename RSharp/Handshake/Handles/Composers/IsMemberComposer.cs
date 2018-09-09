using RSharp.API.Packets;

namespace RSharp.Handshake.Handles.Composers
{
    internal class IsMemberComposer : ServerPacket
    {
        public IsMemberComposer(uint playerId, bool isMember)
            : base(249)
        {
            WriteByteAdd((byte)(isMember ? 1 : 0));
            WriteShortAddBig((int)playerId);
        }
    }
}