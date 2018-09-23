using RSharp.Uv;
using System;

namespace RSharp.World.Tasks
{
    /// <summary>
    /// This class is used to create longrunning tasks with delays.
    /// </summary>
    internal class PeriodicScheduler : IDisposable
    {
        /// <summary>
        /// The timeout before the timer starts.
        /// </summary>
        private readonly int _timeout;

        /// <summary>
        /// The delay between executions.
        /// </summary>
        private readonly int _delay;

        /// <summary>
        /// The timer that's responsible for the task.
        /// </summary>
        private readonly Timer _timer;

        internal PeriodicScheduler(
            Action action,
            int timeout,
            int delay)
        {
            _timeout = timeout;
            _delay = delay;

            Loop loop = new Loop();
            _timer = loop.NewTimer(action);
        }

        /// <summary>
        /// Starts the periodic task.
        /// </summary>
        internal void Start() =>
            _timer.Start(_timeout, _delay);

        /// <summary>
        /// Cancels the periodic task.
        /// </summary>
        public void Dispose() => _timer.Stop();
    }
}
