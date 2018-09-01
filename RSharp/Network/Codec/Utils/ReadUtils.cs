using DotNetty.Buffers;
using System.Text;

namespace RSharp.Network.Codec
{
    internal static class ReadUtils
    {
        internal static int ReadUByte(this IByteBuffer buffer) =>
            buffer.ReadByte() & 0xff;

        internal static string GetRS2String(this IByteBuffer buffer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte b;
            while (buffer.IsReadable() && (b = buffer.ReadByte()) != 10)
            {
                stringBuilder.Append((char)b);
            }

            return stringBuilder.ToString();
        }
    }
}
