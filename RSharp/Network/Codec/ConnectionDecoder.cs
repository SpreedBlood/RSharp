using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Network.Codec.Packet;

namespace RSharp.Network.Codec
{
    internal class ConnectionDecoder : ByteToMessageDecoder
    {
        private readonly IPlayerController _playerController;
        private readonly ISessionController _sessionController;

        internal ConnectionDecoder(
            IPlayerController playerController,
            ISessionController sessionController)
        {
            _playerController = playerController;
            _sessionController = sessionController;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            if (message.ReadableBytes < 2) return;

            int request = message.ReadUByte();
            if (request != 14)
            {
                Console.WriteLine($"Invalid login request: {request}");
                context.CloseAsync();
                return;
            }

            //Name hash
            message.ReadUByte();

            context.WriteAndFlushAsync(new ConnectionSuccessComposer());
            context.Channel.Pipeline.Remove(this);
            context.Channel.Pipeline.AddAfter("encoder", "loginDecoder", new LoginDecoder(_playerController, _sessionController));
        }
    }
}