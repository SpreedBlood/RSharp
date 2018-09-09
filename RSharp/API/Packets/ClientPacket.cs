using DotNetty.Buffers;

namespace RSharp.API.Packets
{
    public class ClientPacket : IClientPacket
    {
        public int OpCode { get; }
        public int Size { get; }

        private readonly IByteBuffer _content;

        public ClientPacket(int opCode, int size, IByteBuffer content)
        {
            OpCode = opCode;
            Size = size;
            _content = content;
        }

        public int ReadUByteS() =>
            128 - _content.ReadByte() & 0xff;
    }
}
