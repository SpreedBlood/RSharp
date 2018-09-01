namespace RSharp.API.Packets
{
    public interface IClientPacket
    {
        /// <summary>
        /// Gets the op code of the incoming packet.
        /// </summary>
        int OpCode { get; }

        /// <summary>
        /// Gets the size of the incoming packet.
        /// </summary>
        int Size { get; }
    }
}
