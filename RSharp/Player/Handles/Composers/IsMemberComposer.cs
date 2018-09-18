using RSharp.API.Packets;

namespace RSharp.Player.Handles.Composers
{
    internal class IsMemberComposer : ServerPacket
    {
        public IsMemberComposer(uint playerId, bool isMember)
            : base(249)
        {
            WriteByteAdd((byte)(isMember ? 1 : 0));
            WriteBigShortAdd((int)playerId);
        }
    }
}