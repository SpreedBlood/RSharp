using DotNetty.Buffers;
using RSharp.Network.Codec;

namespace RSharp.API.Packets
{
    public class ServerPacket
    {
        public IByteBuffer Buffer { get; }
        private readonly int _opCode;

        public ServerPacket()
            : this(-1)
        {

        }

        public ServerPacket(int opCode)
            : this(opCode, Unpooled.Buffer())
        {
        }

        public ServerPacket(int opCode, IByteBuffer buffer)
        {
            _opCode = opCode;
            Buffer = buffer;
        }

        protected void WriteLong(long value) => Buffer.WriteLong(value);

        protected void WriteByte(byte value) => Buffer.WriteByte(value);

        protected void WriteByteA(byte value) => Buffer.WriteByte(value + 128);

        protected void WriteEncByte(byte value, ISAACCipher cipher) => Buffer.WriteByte(value + cipher.GetNextValue());
    }
}