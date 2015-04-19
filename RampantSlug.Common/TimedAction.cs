using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RampantSlug.Common
{
    public static class TimedAction
    {
        public static void ExecuteWithDelay(Action action, TimeSpan delay)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = delay;
            timer.Tag = action;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private static void timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer) sender;
            Action action = (Action) timer.Tag;

            action.Invoke();
            timer.Stop();
        }
    }
}