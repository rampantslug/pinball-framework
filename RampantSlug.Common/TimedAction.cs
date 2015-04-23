using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RampantSlug.Common
{
    /// <summary>
    /// Helper class to execute an action with a specified delay
    /// </summary>
    public static class TimedAction
    {
        /// <summary>
        /// Execute an action after a set amount of time
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <param name="delay">Time to wait before executing</param>
        public static void ExecuteWithDelay(Action action, TimeSpan delay)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = delay;
            timer.Tag = action;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        private static void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer) sender;
            Action action = (Action) timer.Tag;

            action.Invoke();
            timer.Stop();
        }
    }
}