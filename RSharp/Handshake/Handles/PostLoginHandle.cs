using System;
using System.Threading.Tasks;
using RSharp.API.Handles;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Handshake.Handles.Composers;

namespace RSharp.Handshake.Handles
{
    internal class PostLoginHandle : AbstractHandle<EmptyArgs>
    {
        public override int OpCode => 3;

        private readonly IPlayerController _playerController;

        public PostLoginHandle(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        public override async Task Invoke(ISession session, EmptyArgs handleArgs)
        {
            IPlayer player = await _playerController.GetPlayer(session.LoginDetails[0], session.LoginDetails[1]);
            if (player != null)
            {
                session.Player = player;
            }

            await session.WriteAsync(new InitializeFrameComposer(249, session.ISAAC));
            await session.WriteAsync(new IsMemberComposer(false)); //TODO: Members!
        }
    }
}