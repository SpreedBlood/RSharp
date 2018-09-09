using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using RSharp.API.Handles;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Network.Codec;

namespace RSharp.Network
{
    internal class NetworkInitializer : ChannelInitializer<ISocketChannel>
    {
        private readonly IHandleProvider _handleProvider;
        private readonly IPlayerController _playerController;
        private readonly ISessionController _sessionController;

        public NetworkInitializer(
            IHandleProvider provider,
            IPlayerController playerController,
            ISessionController sessionController)
        {
            _handleProvider = provider;
            _playerController = playerController;
            _sessionController = sessionController;
        }

        protected override void InitChannel(ISocketChannel channel)
        {
            channel.Pipeline
                .AddLast("decoder", new HandshakeDecoder(_playerController, _sessionController))
                .AddLast("handler", new NetworkHandler(_handleProvider, _sessionController));
        }
    }
}
