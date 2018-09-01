using DotNetty.Transport.Channels;
using RSharp.API.Network;

namespace RSharp.API.Sessions
{
    public interface ISessionController
    {
        /// <summary>
        /// Get a cached session without checking.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        /// <returns>The session if exists else null.</returns>
        ISession GetSession(IChannelId channelId);

        /// <summary>
        /// Tries to get session with checking. Please use within if statement else simply
        /// use <see cref="GetSession"/>
        /// </summary>
        /// <param name="channelId">The channel id</param>
        /// <param name="session">The usage entry</param>
        /// <returns>Wether or not the session exists.</returns>
        bool TryGetSession(IChannelId channelId, out ISession session);

        /// <summary>
        /// Caches the session.
        /// </summary>
        /// <param name="channel">The channel to assign to the session.</param>
        void CacheSession(IChannelHandlerContext channel);

        /// <summary>
        /// Removes the entry from the cache.
        /// </summary>
        /// <param name="channelId">The channel id to remoev.</param>
        void RemoveFromCache(IChannelId channelId);
    }
}