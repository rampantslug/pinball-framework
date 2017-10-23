using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using ServerLibrary.Events;

namespace ServerLibrary
{
    public delegate bool SwitchAcceptedHandler(Switch sw);
    public delegate bool DelayedHandler(object param);
    public delegate void AnonDelayedHandler();
    public abstract class ServerMode : IMode
    {
        
        /// <summary>
        /// Reference to the hosting GameController object / descendant
        /// </summary>
        public IGameController Game { get; set; }
        
        /// <summary>
        /// The priority of the mode in the queue
        /// </summary>
        public int Priority { get; set; }


        public virtual string Title{ get; set; }

        public List<ModeEvent> ModeEvents { get; set; }

        public List<RequiredDevice> RequiredDevices { get; set; }

        public List<RequiredMedia> RequiredMedia { get; set; }


        //public abstract void InitialiseModeEvents(object obj);

        // Constants
        public const bool SWITCH_STOP = true;
        public const bool SWITCH_CONTINUE = false;

        /// <summary>
        /// Creates a new Mode
        /// </summary>
        /// <param name="game">The parent GameController object</param>
        /// <param name="priority">The priority of this mode in the queue</param>
        public ServerMode(IGameController game, int priority)
        {
            this.Game = game;
            this.Priority = priority;
            
            ModeEvents = new List<ModeEvent>();
            RequiredDevices = new List<RequiredDevice>();
            RequiredMedia = new List<RequiredMedia>();
        }


        public bool HandleEvent(UpdateSwitchEvent switchEvent)
        {
            var handled = false;

            var modeEvents = new List<ModeEvent>();
            foreach (var modeEvent in ModeEvents)
            {
                if (modeEvent.AssociatedSwitch != null) // Some ModeEvents may not have a switch assigned to them
                {
                    if (modeEvent.AssociatedSwitch.Number == switchEvent.Device.Number)
                    {
                        if (modeEvent.Trigger == switchEvent.SwitchEvent.Type)
                        {
                            modeEvent.MethodToExecute(switchEvent.Device);
                        }
                    }
                }
            }

            return handled;
        }

        /// <summary>
        /// Notifies the mode that it is now active on the mode queue.
        /// </summary>
        public virtual void ModeStarted()
        {
            // This method should not be invoked directly; it is called by the GameController run loop
        }

        /// <summary>
        /// Notifies the mode that it has been removed from the mode queue
        /// </summary>
        public virtual void mode_stopped()
        {
            // This method should not be invoked directly. It is called by the GameController run loop
        }

        /// <summary>
        /// Notifies the mode that it is now the topmost mode on the mode queue
        /// </summary>
        public virtual void mode_topmost()
        {
            // This method should not be invoked directly, it is called by the GameController run loop
        }

        //public virtual void mode_tick()
        //{
        // Called by the game controller run loop during each loop when the mode is running
        //}

        /// <summary>
        /// Called by the GameController to dispatch any delayed events
        /// </summary>
        //public void dispatch_delayed()
        //{
        /* double t = tools.Time.GetTime();
         int cnt = _delayed.Count;
         for (int i = 0; i < cnt; i++)
         {
             if (_delayed[i].Time <= t)
             {
                 Game.Logger.Log("dispatch_delayed() " + _delayed[i].Name + " " + _delayed[i].Time.ToString() + " <= " + t.ToString());
                 if (_delayed[i].Param != null)
                     _delayed[i].Handler.DynamicInvoke(_delayed[i].Param);
                 else
                     _delayed[i].Handler.DynamicInvoke(null);
             }
         }
         _delayed = _delayed.Where<Delayed>(x => x.Time > t).ToList<Delayed>();*/
        //}


    }
}

