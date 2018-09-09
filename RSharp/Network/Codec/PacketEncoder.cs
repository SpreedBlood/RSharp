using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Packets;

namespace RSharp.Network.Codec
{
    internal class PacketEncoder : MessageToByteEncoder<ServerPacket>
    {
        private readonly ISAACCipher _cipher;

        internal PacketEncoder(ISAACCipher cipher)
        {
            _cipher = cipher;
        }
        
        protected override void Encode(IChannelHandlerContext context, ServerPacket message, IByteBuffer output)
        {
            int encodedOpCode = message.OpCode + _cipher.GetNextValue(); //Encode the opcode.
            message.Buffer.SetByte(0, encodedOpCode); //Write the opcode to the first position.
            output.WriteBytes(message.Buffer); //Write the buffer from the message to the output.
            message.Buffer.Release(); //Release the old packet buffer.
        }
    }
}
