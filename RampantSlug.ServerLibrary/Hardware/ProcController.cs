using RampantSlug.ServerLibrary.Hardware.Proc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Logging;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary.Hardware
{
    public class ProcController : IHardwareController
    {

        /// <summary>
        /// Run loop exit condition. This continues until true
        /// </summary>
        protected bool _done = false;

        /// <summary>
        /// A pinproc class instance, created in the constructor with the Machine_Type attribute
        /// </summary>
        protected IProcDevice _proc;

        private RsLogManager _logManager;


        public ProcController()
        {
        }

        public bool Setup()
        {
            // TODO: Push configuration data to PROC board
            _logManager = RsLogManager.GetCurrent;

            try
            {
                _proc = new ProcDevice(_logManager);
                _proc.reset(1);
                return true;
            }
            catch (Exception ex)
            {
                RsLogManager.GetCurrent.LogTestMessage("FATAL ERROR: Could not load P-ROC device.");
                RsLogManager.GetCurrent.LogTestMessage(ex.Message);
                return false;
            }
        }

        public void Start()
        {
            // Start run loop
            run_loop();
        }


        public void Close()
        {
            _done = true;
        }






        /// <summary>
        /// Retrieve all events from the PROC interface board
        /// </summary>
        /// <returns>A list of events from the PROC</returns>
        public virtual Event[] get_events()
        {
            return _proc.get_events();
        }


        /// <summary>
        /// Main run loop of the program. Performs the following logic until the loop ends:
        ///     - Get events from PROC
        ///     - Process this list of events across all modes
        ///     - 'Tick' modes
        ///     - Tickle watchdog
        /// </summary>
        private void run_loop()
        {
            long loops = 0;
            _done = false;
           // dmd_event();
            Event[] events;
            //try
            //{
            while (!_done)
            {
                loops++;
                events = get_events();
                if (events != null)
                {
                    foreach (Event evt in events)
                    {
                        process_event(evt);

                    }
                }

               // this.tick();
              //  tick_virtual_drivers();

                // TODO: Process modes ... These should exist above the ProcController level so events get passed up higher than here
                //this.modes.tick();

                // Do we have any events waiting such as pulses from the UI
            /*    lock (_coil_lock_object)
                {
                    SafeCoilDrive c;
                    for (int i = 0; i < _safe_coil_drive_queue.Count; i++)
                    {
                        c = _safe_coil_drive_queue[i];
                        if (c.pulse)
                            Coils[c.coil_name].Pulse(c.pulse_time);
                        if (c.disable)
                            Coils[c.coil_name].Disable();
                    }
                    _safe_coil_drive_queue.Clear();
                }*/

                if (_proc != null)
                {
                    _proc.watchdog_tickle();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log("RUN LOOP EXCEPTION: " + ex.ToString());
            //}
            //finally
            //{
          //  Logger.Log("Run loop ended");
         //   if (loops != 0)
          //  {
         //       double dt = Time.GetTime() - _bootTime;
          //  }
            if (_proc != null)
            {
                _proc.Close();
            }

        }



        /// <summary>
        /// Process the retrieved event from the PROC interface board (switch/dmd events)
        /// </summary>
        /// <param name="evt">The event to process</param>
        public void process_event(Event evt)
        {
            if (evt.Type == EventType.None)
            {
            }
            else if (evt.Type == EventType.Invalid)
            {
                // Invalid event type, end run loop perhaps
            }
            // DMD events
            else if (evt.Type == EventType.DMDFrameDisplayed)
            {
             //   this.dmd_event();
            }
            else
            {
               // Generate Switch event or update switch state
           /*     var sw = _switches[(ushort)evt.Value];
                bool recvd_state = evt.Type == EventType.SwitchClosedDebounced;

                if (!sw.IsState(recvd_state))
                {
                    sw.SetState(recvd_state);
                    _modes.handle_event(evt);
                }*/

                _logManager.LogTestMessage("Proc detected some event! Woohoo!");
            }
        }

    }
}
