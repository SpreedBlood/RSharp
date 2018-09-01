using DotNetty.Transport.Channels;
using RSharp.API.Handles;
using RSharp.API.Network;
using RSharp.API.Packets;
using RSharp.API.Sessions;

namespace RSharp.Network
{
    internal class NetworkHandler : SimpleChannelInboundHandler<IClientPacket>
    {
        private readonly IHandleProvider _handleProvider;
        private readonly ISessionController _sessionController;

        internal NetworkHandler(
            IHandleProvider provider,
            ISessionController sessionController)
        {
            _handleProvider = provider;
            _sessionController = sessionController;
        }

        public override void ChannelRegistered(IChannelHandlerContext context) =>
            _sessionController.CacheSession(context);

        public override void ChannelUnregistered(IChannelHandlerContext context) =>
            _sessionController.RemoveFromCache(context.Channel.Id);

        protected override async void ChannelRead0(IChannelHandlerContext ctx, IClientPacket msg)
        {
            if (_sessionController.TryGetSession(ctx.Channel.Id, out ISession session))
            {
                await _handleProvider.Handle(session, msg);
            }
        }
    }
}