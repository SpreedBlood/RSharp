using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using RSharp.API.Handshake;
using RSharp.API.Players;
using RSharp.API.Sessions;
using RSharp.Network.Codec.Packet;

namespace RSharp.Network.Codec
{
    internal class HandshakeDecoder : ByteToMessageDecoder
    {
        private readonly IPlayerController _playerController;
        private readonly ISessionController _sessionController;

        private HandshakeStatus status;

        internal HandshakeDecoder(
            IPlayerController playerController,
            ISessionController sessionController)
        {
            _playerController = playerController;
            _sessionController = sessionController;

            status = HandshakeStatus.Handshake;
        }

        protected override async void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            switch (status)
            {
                case HandshakeStatus.Handshake:
                    await DecodeHandshake(context, input, output);
                    break;

                case HandshakeStatus.Login:
                     await DecodeLogin(context, input, output);
                    break;
            }
        }

        private async Task DecodeHandshake(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 2) return;

            int request = input.ReadUByte();
            if (request != 14)
            {
                await context.CloseAsync();
                return;
            }

            int nameHash = input.ReadUByte();
            await context.WriteAndFlushAsync(new ConnectionSuccessComposer().Buffer);
            status = HandshakeStatus.Login;
        }

        private async Task DecodeLogin(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 2) return; //Invalid.

            int loginType = input.ReadByte(); //16 = connecting, 18 = reconnecting.
            if (loginType != 16 && loginType != 18)
            {
                Console.WriteLine($"Invalid login type: {loginType}");
                await context.CloseAsync();
                return;
            }
            
            int blockLength = input.ReadByte() - 40; //40 is the extra offset.
            if (blockLength <= 0)
            {
                await CloseChannel(context, $"Encrypted packet size zero or negative: {blockLength}");
                return;
            }

            byte magicNumber = input.ReadByte();

            short clientVersion = input.ReadShort();
            
            byte highOrLowMemory = input.ReadByte();

            // Skip the CRC keys.
            for (int i = 0; i < 9; i++) input.ReadInt();
            
            input.ReadByte(); //RSA is disabled, skip the block length.

            int rsaOpCode = input.ReadByte();
            if (rsaOpCode != 10)
            {
                await CloseChannel(context, "Unable to decode RSA block properly!");
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

            int uid = input.ReadInt();

            string username = input.GetRS2String();
            string password = input.GetRS2String();
            bool userExists = await _playerController.UserExists(username);
            if (userExists)
            {
                if (!await _playerController.TryLogin(username, password)) //Invalid credentials.
                {
                    await context.WriteAndFlushAsync(new LoginErrorComposer(LoginMessage.INVALID_CREDS).Buffer).ContinueWith(async (task, channel) =>
                    {
                        await ((IChannelHandlerContext)channel).CloseAsync();
                    }, context);
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

                await context.WriteAndFlushAsync(new LoginSuccessComposer().Buffer);

                context.Channel.Pipeline
                    .AddBefore("decoder", "packetEncoder", new PacketEncoder(outCipher))
                    .AddAfter("packetEncoder", "packetDecoder", new PacketDecoder(inCipher))
                    .Remove(this);
            }
        }

        private static async Task CloseChannel(IChannelHandlerContext context, string errorMsg)
        {
            Console.WriteLine(errorMsg);
            await context.CloseAsync();
        }

        private static ISAACCipher GetIsaac(int[] seed) => new ISAACCipher(seed);
    }

    internal enum HandshakeStatus
    {
        Handshake = 0,
        Login = 1
    }
}