using DotNetty.Buffers;
using System.Text;

namespace RSharp.API.Packets
{
    public class ServerPacket
    {
        public IByteBuffer Buffer { get; }
        public int OpCode { get; }

        public ServerPacket()
        {
            OpCode = -1;
            Buffer = Unpooled.Buffer();
        }

        public ServerPacket(int opCode)
        {
            OpCode = opCode;
            Buffer = Unpooled.Buffer();

            Buffer.WriteByte(opCode);
        }

        protected void WriteLong(long value) => Buffer.WriteLong(value);

        protected void WriteByte(int value) => Buffer.WriteByte(value);

        protected void WriteByteAdd(int value) => Buffer.WriteByte(value + 128);

        protected void WriteByteSubtract(int value) => Buffer.WriteByte(128 - value);

        protected void WriteByteNegate(int value) => Buffer.WriteByte(-value);

        protected void WriteBigShort(int value)
        {
            Buffer.WriteByte(value >> 8);
            Buffer.WriteByte(value);
        }

        protected void WriteBigShortAdd(int value)
        {
            Buffer.WriteByte(value >> 8);
            Buffer.WriteByte(value + 128);
        }

        protected void WriteLittleShortAdd(byte value)
        {
            Buffer.WriteByte(value + 128);
            Buffer.WriteByte(value >> 8);
        }

        protected void WriteMiddleInt(int value)
        {
            Buffer.WriteByte(value >> 8);
            Buffer.WriteByte(value);
            Buffer.WriteByte(value >> 28);
            Buffer.WriteByte(value >> 16);
        }

        protected void WriteString(string value)
        {
            Buffer.WriteBytes(Encoding.Default.GetBytes(value));
            Buffer.WriteByte(10);
        }
    }
}