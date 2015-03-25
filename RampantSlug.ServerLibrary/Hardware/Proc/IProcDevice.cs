using System;

namespace RampantSlug.ServerLibrary.Hardware.Proc
{
    /// <summary>
    /// Wrapper for libpinpproc
    /// </summary>
    public interface IProcDevice
    {

        /// <summary>
        /// 
        /// </summary>
        bool SwCoindoor { get; set; }


        ///
        /// General Board Functions
        /// 
        #region General Board Functions

        /// <summary>
        /// Destroys an existing P-ROC device handle.
        /// </summary>
        void Close();

        /// <summary>
        /// Resets the P-ROC interface to its defaults. resetFlags has two possible values:
        /// 0	Resets the software only.
        /// 1	Resets the software to its defaults and applies the changes to the hardware.
        /// </summary>
        /// <param name="flags"></param>
        void reset(uint flags);







        #endregion

        /// <summary>
        /// Writes aux_commands (a list of commands) to the P-ROC’s auxiliary bus instruction memory, 
        /// starting at address, which is an offset into the instruction memory.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="aux_commands"></param>
        void aux_send_commands(ushort address, ushort aux_commands);

        

        /// <summary>
        /// Disables (de-energizes) the specified driver number.
        /// </summary>
        /// <param name="number"></param>
        void driver_disable(ushort number);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds"></param>
        /// <param name="futureTime"></param>
        /// <returns></returns>
        Result driver_future_pulse(ushort number, byte milliseconds, UInt16 futureTime);
        
        /// <summary>
        /// Returns a dictionary containing the state information for the specified driver. 
        /// See Driver State Dictionary for a description of the dictionary.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        DriverState driver_get_state(ushort number);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        void driver_group_disable(byte number);

        /// <summary>
        /// Drives the specified driver number with an indefinite pitter-patter sequence, 
        /// where the driver is repeatedly turned on for milliseconds_on and the off for milliseconds_off, each with a max of 127.
        /// If original_on_time is non-zero, the driver is first pulsed for that number of milliseconds before the pitter-patter sequence begins, with a max 255.
        /// Pitter-patter sequences are commonly used for duty cycle control of driver circuits. A case where original_on_time might be non-zero would be for a single coil flipper circuit that needs to be driven to activate the flipper 
        /// before the pitter-patter sequence is used to hold the flipper up.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="original_on_time"></param>
        void driver_patter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time);

        /// <summary>
        /// Pulses driver number for the specified number of milliseconds. 0 indicates forever; the maximum is 255.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        Result driver_pulse(ushort number, byte milliseconds);

        /// <summary>
        /// Drives the specified driver number with a timed pitter-patter sequence, 
        /// where the driver is repeatedly turned on for milliseconds_on and then off for milliseconds_off, each with a max of 127.
        /// The driver is disabled after milliseconds_overall_patter_time, max 255.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="milliseconds_overall_patter_time"></param>
        void driver_pulsed_patter(ushort number, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time);

        /// <summary>
        /// Turns on/off the specified driver number according to the schedule.
        /// schedule is a 32-bit mask where each bit corresponds to a 1/32 of a second timeslot. 
        /// Active bits identify timeslots during which the driver number should be on. 
        /// The least significant bit corresponds to the first timeslot.
        /// The schedule is driven for the specified number of cycle_seconds, 0 = forever, max 255.
        /// now determines whether the schedule is activated immediately (True) or if it is synchronized to a 1 second timer internal to the P-ROC (False). 
        /// When now = False is used with multiple drivers, the schedules of all of the drivers will be synchronized.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="schedule"></param>
        /// <param name="cycle_seconds"></param>
        /// <param name="now"></param>
        void driver_schedule(ushort number, uint schedule, ushort cycle_seconds, bool now);

        /// <summary>
        /// Corresponds to PinPROC.driver_disable().
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        DriverState driver_state_disable(DriverState state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds"></param>
        /// <param name="futureTime"></param>
        /// <returns></returns>
        DriverState driver_state_future_pulse(DriverState state, byte milliseconds, UInt16 futureTime);

        /// <summary>
        /// Corresponds to PinPROC.driver_patter().
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="original_on_time"></param>
        /// <returns></returns>
        DriverState driver_state_patter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort original_on_time);

        /// <summary>
        /// Corresponds to PinPROC.driver_pulse().
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        DriverState driver_state_pulse(DriverState state, byte milliseconds);

        /// <summary>
        /// Corresponds to PinPROC.driver_pulsed_patter()
        /// </summary>
        /// <param name="state"></param>
        /// <param name="milliseconds_on"></param>
        /// <param name="milliseconds_off"></param>
        /// <param name="milliseconds_overall_patter_time"></param>
        /// <returns></returns>
        DriverState driver_state_pulsed_patter(DriverState state, ushort milliseconds_on, ushort milliseconds_off, ushort milliseconds_overall_patter_time);

        /// <summary>
        /// Corresponds to PinPROC.driver_schedule().
        /// </summary>
        /// <param name="state"></param>
        /// <param name="schedule"></param>
        /// <param name="seconds"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        DriverState driver_state_schedule(DriverState state, uint schedule, byte seconds, bool now);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="polarity"></param>
        /// <param name="use_clear"></param>
        /// <param name="strobe_start_select"></param>
        /// <param name="start_strobe_time"></param>
        /// <param name="matrix_row_enable_index0"></param>
        /// <param name="matrix_row_enable_index1"></param>
        /// <param name="active_low_matrix_rows"></param>
        /// <param name="tickle_stern_watchdog"></param>
        /// <param name="encode_enables"></param>
        /// <param name="watchdog_expired"></param>
        /// <param name="watchdog_enable"></param>
        /// <param name="watchdog_reset_time"></param>
        void driver_update_global_config(bool enable, bool polarity, bool use_clear, bool strobe_start_select,
            byte start_strobe_time, byte matrix_row_enable_index0, byte matrix_row_enable_index1,
            bool active_low_matrix_rows, bool tickle_stern_watchdog, bool encode_enables, bool watchdog_expired,
            bool watchdog_enable, ushort watchdog_reset_time);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group_num"></param>
        /// <param name="slow_time"></param>
        /// <param name="enable_index"></param>
        /// <param name="row_activate_index"></param>
        /// <param name="row_enable_select"></param>
        /// <param name="matrixed"></param>
        /// <param name="polarity"></param>
        /// <param name="active"></param>
        /// <param name="disable_strobe_after"></param>
        void driver_update_group_config(byte group_num, ushort slow_time, byte enable_index, byte row_activate_index,
            byte row_enable_select, bool matrixed, bool polarity, bool active, bool disable_strobe_after);

        /// <summary>
        /// Updates a driver configuration using the passed dictionary. 
        /// The driver number is contained within the dictionary. 
        /// See Driver State Dictionary for a description of the dictionary.
        /// </summary>
        /// <param name="driver"></param>
        void driver_update_state(ref DriverState driver);

        /// <summary>
        /// Writes all buffered commands to the P-ROC hardware. 
        /// This method is necessary because the internal command buffer is written to hardware only when it is full.
        /// </summary>
        void flush();

        /// <summary>
        /// Returns a list of dictionaries representing P-ROC events. 
        /// Each dictionary contains a type key and a value key. Event types include:
        /// 1	The switch has changed from open to closed and the signal has been debounced.
        /// 2	The switch has changed from closed to open and the signal has been debounced.
        /// 3	The switch has changed from open to closed and the signal has not been debounced.
        /// 4	The switch has changed from closed to open and the signal has not been debounced.
        /// 5	A new frame has been displayed on the DMD and there is room in the buffer for another.
        /// </summary>
        /// <returns></returns>
        Event[] get_events();

        

        /// <summary>
        /// Returns a list of integers representing the last known state of each switch. 
        /// See the table in get_events() for a list of state values.
        /// </summary>
        /// <returns></returns>
        EventType[] switch_get_states();

        /// <summary>
        /// Configures the rule for the given switch number when its state changes to event_type. 
        /// Rules are used to configure automatic hardware actions in response to switch events. 
        /// Actions include notifying the host and changing driver states.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="event_type"></param>
        /// <param name="rule"></param>
        /// <param name="linked_drivers"></param>
        /// <param name="drive_outputs_now"></param>
        void switch_update_rule(ushort number, EventType event_type, SwitchRule rule, DriverState[] linked_drivers, bool drive_outputs_now);

        /// <summary>
        /// This method resets the hardware watchdog timer. 
        /// The timer should be tickled regularly, as the drivers are disabled when the watchdog timer expires. 
        /// The default watchdog timer period is 1 second.
        /// </summary>
        void watchdog_tickle();

        

    }
}
