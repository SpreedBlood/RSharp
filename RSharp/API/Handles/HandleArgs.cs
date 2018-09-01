using RSharp.API.Packets;

namespace RSharp.API.Handles
{
    public abstract class HandleArgs
    {
        public abstract void ParseArgs(IClientPacket clientPacket);
    }

    public class EmptyArgs : HandleArgs
    {
        public override void ParseArgs(IClientPacket clientPacket)
        {
        }
    }
}
