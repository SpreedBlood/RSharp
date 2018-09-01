﻿using DotNetty.Transport.Channels;
using RSharp.API.Sessions;
using RSharp.Session.Models;
using System.Collections.Generic;

namespace RSharp.Session
{
    internal class SessionRepository
    {
        private readonly IDictionary<IChannelId, ISession> _cachedSessions;

        public SessionRepository()
        {
            _cachedSessions = new Dictionary<IChannelId, ISession>();
        }

        internal ISession GetSession(IChannelId channelId) =>
            _cachedSessions[channelId];

        internal bool TryGetSession(IChannelId channelId, out ISession session) =>
            _cachedSessions.TryGetValue(channelId, out session);

        internal void CacheSession(IChannelHandlerContext channel) =>
            _cachedSessions.Add(channel.Channel.Id, new SessionClient(channel));

        internal void RemoveFromCache(IChannelId channelId) =>
            _cachedSessions.Remove(channelId);
    }
}