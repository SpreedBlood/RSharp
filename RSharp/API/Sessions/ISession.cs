﻿using RSharp.API.Packets;
using RSharp.API.Players;
using RSharp.Network.Codec;
using System.Threading.Tasks;

namespace RSharp.API.Sessions
{
    public interface ISession
    {
        /// <summary>
        /// Gets the player instance associated with the session. This is null
        /// until logged in.
        /// </summary>
        IPlayer Player { get; set; }

        /// <summary>
        /// Gets the login details of the player.
        /// [0] = username,
        /// [1] = password.
        /// </summary>
        string[] LoginDetails { get; set; }

        /// <summary>
        /// Gets the encryption that's associated with the session.
        /// </summary>
        ISAACCipher ISAAC { get; set; }

        /// <summary>
        /// Writes the packet asynchronously.
        /// </summary>
        /// <param name="serverPacket">The packet to write.</param>
        /// <returns>Written the packet upon task completion.</returns>
        Task WriteAsync(ServerPacket serverPacket);

        /// <summary>
        /// Writes and flushes all the written packets asynchronously.
        /// </summary>
        /// <param name="serverPacket">The packet to write and flush.</param>
        /// <returns>Written and flushed all the packets upon task completion.</returns>
        Task WriteAndFlushAsync(ServerPacket serverPacket);

        /// <summary>
        /// Flushes the queue that's associated with the session.
        /// </summary>
        void Flush();
    }
}