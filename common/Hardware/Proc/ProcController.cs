using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Hardware.DeviceAddress;
using Logging;
using Common;
using Hardware.Proc;

namespace Hardware.Proc
{
    [Export(typeof(IProcController))]
    public class ProcController : IProcController
    {

        private BackgroundWorker worker;

        /// <summary>
        /// Run loop exit condition. This continues until true
        /// </summary>
        protected bool _done = false;

        /// <summary>
        /// A pinproc class instance, created in the constructor with the Machine_Type attribute
        /// </summary>
        private IProcDevice _proc;

        private IRsLogger _logger;
        private IEventAggregator _eventAggregator;
        private IDevices _devices;

        //public IGameController GameController { get; private set; }

        [ImportingConstructor]
        public ProcController(IEventAggregator eventAggregator, IRsLogger logger, IDevices devices)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _devices = devices;
        }

   
        private void ProcWorkerThread(object sender, DoWorkEventArgs e)
        {

            System.Threading.Thread.CurrentThread.Name = "p-roc thread";

            try
            {
                _proc = new ProcDevice(_logger);
                _proc.reset(1);
                ProcessConfig();

                Start();

            }
            catch (Exception ex)
            {
                RsLog.LogMessage(LogEventType.Error, OriginatorType.Proc, "Proc", "Disconnected", "Failed to connect to P-ROC: " + ex.Message);
            }
        }

        public bool Setup()
        {
            
            //Put this onto a different thread...
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += new DoWorkEventHandler(ProcWorkerThread);
            worker.RunWorkerAsync();

            return true;
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


        public void ProcessConfig()
        {

            _devices.SwitchIdAddressLookup.Clear();

            // Add switches to PROC
            foreach (var gameSwitch in _devices.AllSwitches())
            {
                // TODO: Clean up how we determine the switch number. Need to create an Address Object contained in the switch
                // Lookup needs to be done at a different location to allow for other switch handlers other than PROC...

                var address = AddressFactory.CreateAddress(gameSwitch.Address);

                // Move this to internal device stuff. So that number and addressId are linked within device and external classes can use whatever to retrieve the switch
                _devices.SwitchIdAddressLookup.Add(address.AddressId, gameSwitch.Number);              

                _proc.switch_update_rule(address.AddressId,
                    EventType.SwitchClosedDebounced,
                    new SwitchRule { NotifyHost = true, ReloadActive = false },
                    null,
                    false
                    );
                _proc.switch_update_rule(address.AddressId,
                    EventType.SwitchOpenDebounced,
                    new SwitchRule { NotifyHost = true, ReloadActive = false },
                    null,
                    false
                    );
            } 
   
            // Add coils to PROC

            // TODO: Complete configuration stuffs...
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
                var switchNumber = _devices.SwitchIdAddressLookup[(ushort)evt.Value];
                var sw = _devices.Switches[switchNumber];
               
                // Need to update the state of the switch before publishing...

            //TODO: Should hardware have access to EventAggregator?!?!
//                _eventAggregator.PublishOnUIThread(new UpdateSwitchEvent
//                {
//                    Device = sw,
//                    SwitchEvent = evt
//
//                });

                // TODO: Trigger modes n stuff based on switch...
                // NOTE: However that this will push up to a higher level where modes will be dealt with elsewhere
                // To allow for mode triggering to occur from switch events sent via Client

                /*bool recvd_state = evt.Type == EventType.SwitchClosedDebounced;
                if (!sw.IsState(recvd_state))
                {
                    sw.SetState(recvd_state);
                    _modes.handle_event(evt);
                }*/


                
            }
        }

    }
}
