using RSharp.API.Packets;
using RSharp.API.Sessions;
using System.Threading.Tasks;

namespace RSharp.API.Handles
{
    public abstract class AbstractHandle<TArgs> : IAsyncHandle
        where TArgs : HandleArgs, new()
    {
        public abstract int OpCode { get; }
        
        public abstract Task Invoke(ISession session, TArgs args);

        public Task Handle(ISession session, IClientPacket clientPacket)
        {
            if (typeof(TArgs) == typeof(EmptyArgs))
                return Invoke(session, null);

            TArgs args = new TArgs();
            args.ParseArgs(clientPacket);

            return Invoke(session, args);
        }
    }
}