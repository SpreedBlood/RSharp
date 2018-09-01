﻿using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using RSharp.API.Packets;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Network.Codec;

namespace RSharp.Session.Models
{
    internal class SessionClient : ISession
    {
        public IPlayer Player { get; set; }
        public string[] LoginDetails { get; set; }
        public ISAACCipher ISAAC { get; set; }

        private readonly IChannelHandlerContext _channel;

        internal SessionClient(IChannelHandlerContext context)
        {
            _channel = context;
        }

        public Task WriteAsync(ServerPacket serverPacket) => _channel.WriteAsync(serverPacket);

        public Task WriteAndFlushAsync(ServerPacket serverPacket) => _channel.WriteAndFlushAsync(serverPacket);

        public void Flush() => _channel.Flush();
    }
}