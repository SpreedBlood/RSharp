using RSharp.API.Sessions;
using RSharp.API.World;
using RSharp.World.Tasks;
using System.Collections.Generic;

namespace RSharp.World
{
    internal class World : IWorld
    {
        /// <summary>
        /// The sessions that the world holds.
        /// </summary>
        private readonly ICollection<ISession> _playerList;

        /// <summary>
        /// The periodic scheduler that's resposible for cycling the world.
        /// </summary>
        private readonly PeriodicScheduler _scheduler;

        internal World()
        {
            _playerList = new List<ISession>();

            _scheduler = new PeriodicScheduler(Cycle, 0, 500);
            _scheduler.Start();
        }

        public void AddSession(ISession session) => _playerList.Add(session);

        public bool RemoveSession(ISession session) => _playerList.Remove(session);

        internal void Cycle()
        {
            foreach (ISession session in _playerList)
            {

            }
        }
    }
}