﻿using DotNetty.Transport.Channels;
using RSharp.API.Network;
using RSharp.API.Sessions;

namespace RSharp.Session
{
    internal class SessionController : ISessionController
    {
        private readonly SessionRepository _sessionRepository;

        public SessionController(SessionRepository repository)
        {
            _sessionRepository = repository;
        }

        public ISession GetSession(IChannelId channelId) =>
            _sessionRepository.GetSession(channelId);

        public bool TryGetSession(IChannelId channelId, out ISession session) =>
            _sessionRepository.TryGetSession(channelId, out session);

        public void CacheSession(IChannelHandlerContext channel) =>
            _sessionRepository.CacheSession(channel);

        public void RemoveFromCache(IChannelId channelId) =>
            _sessionRepository.RemoveFromCache(channelId);
    }
}
