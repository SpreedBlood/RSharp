using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Packets;

namespace RSharp.Network.Codec
{
    internal class Encoder : MessageToMessageEncoder<ServerPacket>
    {
        protected override void Encode(IChannelHandlerContext context, ServerPacket message, List<object> output)
        {
            output.Add(message.Buffer);
        }
    }
}
