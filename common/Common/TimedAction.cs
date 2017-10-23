using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Common
{
    /// <summary>
    /// Helper class to execute an action with a specified delay.
    /// </summary>
    public static class TimedAction
    {
        /// <summary>
        /// Execute an action after a set amount of time.
        /// </summary>
        /// <param name="action">Action to perform.</param>
        /// <param name="delay">Time to wait before executing.</param>
        public static void ExecuteWithDelay(Action action, TimeSpan delay)
        {
            var timer = new DispatcherTimer {Interval = delay, Tag = action};
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        private static void TimerTick(object sender, EventArgs e)
        {
            var timer = (DispatcherTimer) sender;
            var action = (Action) timer.Tag;

            action.Invoke();
            timer.Stop();
        }
    }
}