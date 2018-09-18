using RSharp.API.Packets;

namespace RSharp.Interface.Handles.Composers
{
    public class ResetScreenStateComposer : ServerPacket
    {
        public ResetScreenStateComposer()
            : base(107)
        {
        }
    }
}
