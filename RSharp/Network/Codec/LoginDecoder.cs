using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Handshake;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Network.Codec.Packet;

namespace RSharp.Network.Codec
{
    internal class LoginDecoder : ByteToMessageDecoder
    {
        private readonly IPlayerController _playerController;
        private readonly ISessionController _sessionController;

        internal LoginDecoder(
            IPlayerController playerController,
            ISessionController sessionController)
        {
            _playerController = playerController;
            _sessionController = sessionController;
        }

        protected override async void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 2) return;

            int loginType = input.ReadUByte();
            if (loginType != 16 && loginType != 18)
            {
                Console.WriteLine($"Invalid login type: {loginType}");
                await context.CloseAsync();
                return;
            }

            // Make sure we have the complete login block
            int blockLength = input.ReadUByte();
            int loginEncryptSize = blockLength - (36 + 1 + 1 + 2);
            if (loginEncryptSize <= 0)
            {
                Console.WriteLine($"Encrypted packet size zero or negative: {loginEncryptSize}");
                await context.CloseAsync();
                return;
            }

            input.ReadUByte(); //Magic id.

            ushort clientVersion = input.ReadUnsignedShort();

            // High/low memory
            input.ReadByte();

            // Skip the CRC keys.
            for (int i = 0; i < 9; i++) input.ReadInt();

            // Skip RSA block length.
            input.ReadByte();

            int rsaOpCode = input.ReadByte();
            if (rsaOpCode != 10)
            {
                Console.WriteLine("Unable to decode RSA block properly!");
                await context.CloseAsync();
                return;
            }

            long clientHalf = input.ReadLong();
            long serverHalf = input.ReadLong();

            int[] isaacSeed = { (int)(clientHalf >> 32), (int)clientHalf, (int)(serverHalf >> 32), (int)serverHalf };

            ISAACCipher inCipher = new ISAACCipher(isaacSeed);
            for (int i = 0; i < isaacSeed.Length; i++)
            {
                isaacSeed[i] += 50;
            }

            ISAACCipher outCipher = new ISAACCipher(isaacSeed);
            
            string username = input.GetRS2String();
            string password = input.GetRS2String();

            bool userExists = await _playerController.UserExists(username);
            if (userExists)
            {
                if (!await _playerController.TryLogin(username, password)) //Invalid credentials.
                {
                    await context.WriteAndFlushAsync(new LoginErrorComposer(LoginMessage.INVALID_CREDS)).ContinueWith(async (task, channel) =>
                    {
                        await ((IChannelHandlerContext)channel).CloseAsync();
                    }, context); ;
                    return;
                }
            }
            else
            {
                await _playerController.TryRegister(username, password);
            }

            if (_sessionController.TryGetSession(context.Channel.Id, out ISession session))
            {
                string[] loginDetails = new string[2];
                loginDetails[0] = username;
                loginDetails[1] = password;
                session.LoginDetails = loginDetails;

                context.Channel.Pipeline.Remove(this)
                    .AddAfter("encoder", "packetDecoder", new PacketDecoder(inCipher));

                session.ISAAC = outCipher;

                await context.WriteAndFlushAsync(new LoginSuccessComposer());
            }
        }
    }
}