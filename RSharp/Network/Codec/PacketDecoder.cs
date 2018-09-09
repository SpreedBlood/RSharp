using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Packets;

namespace RSharp.Network.Codec
{
    internal class PacketDecoder : ByteToMessageDecoder
    {
        private readonly ISAACCipher _cipher;

        private int opCode;
        private int size;

        internal PacketDecoder(ISAACCipher cipher)
        {
            _cipher = cipher;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (opCode == -1)
            {
                if (input.ReadableBytes > 0)
                {
                    opCode = input.ReadByte();
                    opCode = opCode - _cipher.GetNextValue() & 0xFF;
                    size = _packetLengths[opCode];
                }
                else
                {
                    return;
                }
            }

            if (size == -1)
            {
                if (input.ReadableBytes > 0)
                {
                    size = input.ReadByte() & 0xFF;
                }
                else
                {
                    return;
                }
            }

            if (size == -2)
            {
                if (input.ReadableBytes > 1)
                {
                    size = input.ReadUnsignedShort();
                }
                else
                {
                    return;
                }
            }

            if (input.ReadableBytes >= size)
            {
                output.Add(new ClientPacket(opCode, size, input.ReadBytes(size)));
                Reset();
            }
        }

        private void Reset()
        {
            opCode = -1;
            size = -1;
        }

        private static readonly int[] _packetLengths =
        {
            0, 0, 0, 1, -1, 0, 0, 0, 0, 0, // 0
			0, 0, 0, 0, 8, 0, 6, 2, 2, 0, // 10
			0, 2, 0, 6, 0, 12, 0, 0, 0, 0, // 20
			0, 0, 0, 0, 0, 8, 4, 0, 0, 2, // 30
			2, 6, 0, 6, 0, -1, 0, 0, 0, 0, // 40
			0, 0, 0, 12, 0, 0, 0, 0, 8, 0, // 50
			0, 8, 0, 0, 0, 0, 0, 0, 0, 0, // 60
			6, 0, 2, 2, 8, 6, 0, -1, 0, 6, // 70
			0, 0, 0, 0, 0, 1, 4, 6, 0, 0, // 80
			0, 0, 0, 0, 0, 3, 0, 0, -1, 0, // 90
			0, 13, 0, -1, 0, 0, 0, 0, 0, 0,// 100
			0, 0, 0, 0, 0, 0, 0, 6, 0, 0, // 110
			1, 0, 6, 0, 0, 0, -1, 0, 2, 6, // 120
			0, 4, 6, 8, 0, 6, 0, 0, 0, 2, // 130
			0, 0, 0, 0, 0, 6, 0, 0, 0, 0, // 140
			0, 0, 1, 2, 0, 2, 6, 0, 0, 0, // 150
			0, 0, 0, 0, -1, -1, 0, 0, 0, 0,// 160
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, // 170
			0, 8, 0, 3, 0, 2, 0, 0, 8, 1, // 180
			0, 0, 12, 0, 0, 0, 0, 0, 0, 0, // 190
			2, 0, 0, 0, 0, 0, 0, 0, 4, 0, // 200
			4, 0, 0, 0, 7, 8, 0, 0, 10, 0, // 210
			0, 0, 0, 0, 0, 0, -1, 0, 6, 0, // 220
			1, 0, 0, 0, 6, 0, 6, 8, 1, 0, // 230
			0, 4, 0, 0, 0, 0, -1, 0, -1, 4,// 240
			0, 0, 6, 6, 0, 0, 0 // 250
        };
    }
}