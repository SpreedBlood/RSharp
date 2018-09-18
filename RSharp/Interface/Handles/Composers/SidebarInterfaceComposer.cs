using RSharp.API.Packets;

namespace RSharp.Interface.Handles.Composers
{
    public class SidebarInterfaceComposer : ServerPacket
    {
        public SidebarInterfaceComposer(int interfaceId, int id)
            : base(71)
        {
            WriteBigShort(id);
            WriteByteAdd(interfaceId);
        }
    }
}