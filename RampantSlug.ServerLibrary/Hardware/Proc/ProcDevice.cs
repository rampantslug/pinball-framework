using System;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary.Hardware.Proc
{
    public class ProcDevice: IProcDevice
    {
        public IntPtr ProcHandle;
        //public MachineType g_machineType;
        private static bool firstTime = true;


        private bool swCoindoor = false;

        private object procSyncObject = new object();

        public RsLogManager Logger { get; set; }

        public bool SwCoindoor
        {
            get { return swCoindoor; }
            set { swCoindoor = value; }
        }

        public ProcDevice(RsLogManager logger)
        {
            Logger = logger;
            Logger.LogTestMessage("Initializing P-ROC device...");

            // Only support Custom Machines. If we want to allow WPC etc... then need to move MachineType out to variable.
            ProcHandle = PinProc.PRCreate(MachineType.PDB);
            if (ProcHandle == IntPtr.Zero)
            {
                //Logger.LogTestMessage("InvalidOperationException");

                //  var error = PinProc.PRGetLastErrorText();
                //  _logger.LogTestMessage(error);


                throw new InvalidOperationException("Failed to connect to PROC");

            } // TODO: Who is catching this error??
           
        }

        public void Close()
        {
            if (ProcHandle != IntPtr.Zero)
                PinProc.PRDelete(ProcHandle);

            ProcHandle = IntPtr.Zero;
        }

        public void flush()
        {
            lock (procSyncObject)
            {
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// DRIVER FUNCTIONS
        ///////////////////////////////////////////////////////////////////////////////

        public Result driver_pulse(ushort number, byte milliseconds)
        {
            DriverState state = this.driver_get_state(number);
            Result res;
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulse(ref state, milliseconds);
                res = PinProc.PRDriverUpdateState(ProcHandle, ref state);
            }

            if (res == Result.Success)
            {
                lock (procSyncObject)
                {
                    res = PinProc.PRDriverWatchdogTickle(ProcHandle);
                    res = PinProc.PRFlushWriteData(ProcHandle);
                }
            }
            return res;
        }

        public Result driver_future_pulse(ushort number, byte milliseconds, UInt16 futureTime)
        {
            DriverState state = this.driver_get_state(number);
            Result res;

            lock (procSyncObject)
            {
                PinProc.PRDriverStateFuturePulse(ref state, milliseconds, futureTime);
                res = PinProc.PRDriverUpdateState(ProcHandle, ref state);
            }

            if (res == Result.Success)
            {
                lock (procSyncObject)
                {
                    res = PinProc.PRDriverWatchdogTickle(ProcHandle);
                    res = PinProc.PRFlushWriteData(ProcHandle);
                }
            }
            return res;
        }

        public void driver_schedule(ushort number, uint schedule, ushort cycle_seconds, bool now)
        {
            DriverState state = this.driver_get_state(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStateSchedule(ref state, schedule, (byte)cycle_seconds, now);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        public void driver_patter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time)
        {
            DriverState state = this.driver_get_state(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePatter(ref state, milliseconds_on, milliseconds_off, original_on_time);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        public void driver_pulsed_patter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time)
        {
            DriverState state = this.driver_get_state(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulsedPatter(ref state, milliseconds_on, milliseconds_off, milliseconds_overall_patter_time);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        public void driver_group_disable(byte number)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverGroupDisable(ProcHandle, number);
            }
        }

        public void driver_disable(ushort number)
        {
            DriverState state = this.driver_get_state(number);
            lock (procSyncObject)
            {
                PinProc.PRDriverStateDisable(ref state);
                PinProc.PRDriverUpdateState(ProcHandle, ref state);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        public DriverState driver_get_state(ushort number)
        {
            DriverState ds = new DriverState();
            lock (procSyncObject)
            {
                PinProc.PRDriverGetState(ProcHandle, (byte)number, ref ds);
            }
            return ds;
        }

        public void driver_update_state(ref DriverState driver)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverUpdateState(ProcHandle, ref driver);
            }
        }

        public void driver_update_global_config(bool enable, bool polarity, bool use_clear, bool strobe_start_select,
            byte start_strobe_time, byte matrix_row_enable_index0, byte matrix_row_enable_index1,
            bool active_low_matrix_rows, bool tickle_stern_watchdog, bool encode_enables, bool watchdog_expired,
            bool watchdog_enable, ushort watchdog_reset_time)
        {
            lock (procSyncObject)
            {
                DriverGlobalConfig globals = new DriverGlobalConfig();
                globals.EnableOutputs = enable;
                globals.GlobalPolarity = polarity;
                globals.UseClear = use_clear;
                globals.StrobeStartSelect = strobe_start_select;
                globals.StartStrobeTime = start_strobe_time;
                globals.MatrixRowEnableIndex0 = matrix_row_enable_index0;
                globals.MatrixRowEnableIndex1 = matrix_row_enable_index1;
                globals.ActiveLowMatrixRows = active_low_matrix_rows;
                globals.TickleSternWatchdog = tickle_stern_watchdog;
                globals.EncodeEnables = encode_enables;
                globals.WatchdogExpired = watchdog_expired;
                globals.WatchdogEnable = watchdog_enable;
                globals.WatchdogResetTime = watchdog_reset_time;

                PinProc.PRDriverUpdateGlobalConfig(ProcHandle, ref globals);
            }
        }

        public void driver_update_group_config(byte group_num, ushort slow_time, byte enable_index, byte row_activate_index,
            byte row_enable_select, bool matrixed, bool polarity, bool active, bool disable_strobe_after)
        {
            lock (procSyncObject)
            {
                DriverGroupConfig group = new DriverGroupConfig();
                group.GroupNum = group_num;
                group.SlowTime = slow_time;
                group.EnableIndex = enable_index;
                group.RowActivateIndex = row_activate_index;
                group.RowEnableSelect = row_enable_select;
                group.Matrixed = matrixed;
                group.Polarity = polarity;
                group.Active = active;
                group.DisableStrobeAfter = disable_strobe_after;

                PinProc.PRDriverUpdateGroupConfig(ProcHandle, ref group);
            }
        }

        public DriverState driver_state_pulse(DriverState state, byte milliseconds)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulse(ref state, milliseconds);
            }
            return state;
        }

        public DriverState driver_state_future_pulse(DriverState state, byte milliseconds, UInt16 futureTime)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateFuturePulse(ref state, milliseconds, futureTime);
            }
            return state;
        }

        public DriverState driver_state_disable(DriverState state)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateDisable(ref state);
            }
            return state;
        }

        public DriverState driver_state_schedule(DriverState state, uint schedule, byte seconds, bool now)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStateSchedule(ref state, schedule, seconds, now);
            }
            return state;
        }

        public DriverState driver_state_patter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePatter(ref state, milliseconds_on, milliseconds_off, original_on_time);
            }
            return state;
        }

        public DriverState driver_state_pulsed_patter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time)
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverStatePulsedPatter(ref state, milliseconds_on, milliseconds_off, milliseconds_overall_patter_time);
            }
            return state;
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// SWITCH FUNCTIONS
        ///////////////////////////////////////////////////////////////////////////////

        public EventType[] switch_get_states()
        {
            ushort numSwitches = PinProc.kPRSwitchPhysicalLast + 1;
            EventType[] procSwitchStates = new EventType[numSwitches];
            lock (procSyncObject)
            {
                PinProc.PRSwitchGetStates(ProcHandle, procSwitchStates, numSwitches);
            }
            return procSwitchStates;
        }

        public void switch_update_rule(ushort number, EventType event_type, SwitchRule rule, DriverState[] linked_drivers, bool drive_outputs_now)
        {
            int numDrivers = 0;
            if (linked_drivers != null)
                numDrivers = linked_drivers.Length;

            //bool use_column_8 = g_machineType == MachineType.WPC;

            if (firstTime)
            {
                firstTime = false;
                SwitchConfig switchConfig = new SwitchConfig();
                switchConfig.Clear = false;
                switchConfig.UseColumn8 = false;
                switchConfig.UseColumn9 = false; // No WPC machines actually use this
                switchConfig.HostEventsEnable = true;
                switchConfig.DirectMatrixScanLoopTime = 2; // Milliseconds
                switchConfig.PulsesBeforeCheckingRX = 10;
                switchConfig.InactivePulsesAfterBurst = 12;
                switchConfig.PulsesPerBurst = 6;
                switchConfig.PulseHalfPeriodTime = 13; // Milliseconds
                lock (procSyncObject)
                {
                    PinProc.PRSwitchUpdateConfig(ProcHandle, ref switchConfig);
                }
            }
            Result r;
            lock (procSyncObject)
            {
                r = PinProc.PRSwitchUpdateRule(ProcHandle, (byte)number, event_type, ref rule, linked_drivers, numDrivers, drive_outputs_now);
            }
            if (r == Result.Success)
            {
                // Possibly we should flush the write data here
            }
            else
            {
        //        _logger.LogTestMessage(String.Format("SwitchUpdateRule FAILED for #{0} event_type={1} numDrivers={2} drive_outputs_now={3}",
        //            number, event_type.ToString(), numDrivers, drive_outputs_now));
            }
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="address"></param>
        /// <param name="aux_commands"></param>
        public void aux_send_commands(ushort address, ushort aux_commands)
        {
            throw new NotImplementedException();
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// PROC BOARD INTERACTIONS
        ///////////////////////////////////////////////////////////////////////////////

        public void watchdog_tickle()
        {
            lock (procSyncObject)
            {
                PinProc.PRDriverWatchdogTickle(ProcHandle);
                PinProc.PRFlushWriteData(ProcHandle);
            }
        }

        public Event[] get_events()
        {
            const int batchSize = 16; // Pyprocgame uses 16
            Event[] events = new Event[batchSize];

            int numEvents;

            lock (procSyncObject)
            {
                numEvents = PinProc.PRGetEvents(ProcHandle, events, batchSize);
            }

            if (numEvents <= 0) return null;

            return events;
        }

        public void reset(uint flags)
        {
            lock (procSyncObject)
            {
                PinProc.PRReset(ProcHandle, flags);
            }
        }


    }
}

