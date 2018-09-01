using RSharp.API.Handles;
using RSharp.API.Packets;
using RSharp.API.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSharp
{
    internal class HandleProvider : IHandleProvider
    {
        private readonly IDictionary<int, IAsyncHandle> _asyncHandles;

        public HandleProvider(IEnumerable<IAsyncHandle> handles)
        {
            _asyncHandles = handles.ToDictionary(handle => handle.OpCode, handle => handle);
        }

        public async Task Handle(ISession session, IClientPacket clientPacket)
        {
            if (_asyncHandles.TryGetValue(clientPacket.OpCode, out IAsyncHandle handle))
            {
                await handle.Handle(session, clientPacket);
            }
            else
            {
                Console.WriteLine($"Unknown op code: {clientPacket.OpCode}");
            }
        }
    }
}