using RSharp.API.Entity;
using RSharp.API.Packets;

namespace RSharp.World.Handles.Composers
{
    public class MapRegionComposer : ServerPacket
    {
        public MapRegionComposer(IPosition position)
            : base(73)
        {
            WriteBigShortAdd(position.MapRegionX + 6);
            WriteBigShort(position.MapRegionY);
        }
    }
}
