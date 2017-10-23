using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLibrary.Events;

namespace ServerLibrary
{
    public class ModeQueue
    {
       // protected IGameController _game;
        protected List<ServerMode> _modes;
        protected object _mode_lock_obj = new object();
        public ModeQueue(IGameController game)
        {
       //     _game = game;
            _modes = new List<ServerMode>();
        }

        public void Add(ServerMode mode)
        {
            if (_modes.Contains(mode))
                throw new Exception("Attempted to add mode " + mode.ToString() + ", already in mode queue.");

            lock (_mode_lock_obj)
            {
                _modes.Add(mode);
            }
            //self.modes.sort(lambda x, y: y.priority - x.priority)
            _modes.Sort();
            mode.ModeStarted();

            if (mode == _modes[0])
                mode.mode_topmost();
        }

        public void Remove(ServerMode mode)
        {
            mode.mode_stopped();
            lock (_mode_lock_obj)
            {
                _modes.Remove(mode);
            }

            if (_modes.Count > 0)
                _modes[0].mode_topmost();
        }

        public void HandleEvent(UpdateSwitchEvent switchEvent)
        {
            ServerMode[] modes = new ServerMode[_modes.Count()];
            _modes.CopyTo(modes);
            foreach (ServerMode mode in modes)
            {
                var handled = mode.HandleEvent(switchEvent);
                if (handled)
                    break;
            }
        }

        public void Clear()
        {
            _modes.Clear();
        }

       /* public void tick()
        {
            Mode[] modes;
            lock (_mode_lock_obj)
            {
                modes = new Mode[_modes.Count()];
                _modes.CopyTo(modes);
            }
            for (int i = 0; i < modes.Length; i++)
            {
                modes[i].dispatch_delayed();
                modes[i].mode_tick();
            }
        }*/

        public List<ServerMode> Modes
        {
            get { return _modes; }
        }
    }
}

