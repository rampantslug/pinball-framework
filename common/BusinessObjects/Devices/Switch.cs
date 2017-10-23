using System;

namespace BusinessObjects.Devices
{
    /// <summary>
    /// Represents a switch device in a pinball machine.
    /// </summary>
    public class Switch: Device, IDevice
    {



        public SwitchType Type
        {
            get; set;
        }

        public Switch()
        {
            Number = 0;
            Name = "";
            Type = SwitchType.NO;
        }

        /// <summary>
        /// Sets the switch state to the given state
        /// </summary>
        /// <param name="state">true for closed, false for open</param>
        public override void SetState(bool state)
        {
            _state = state;
            ResetTimer();
        }

        /// <summary>
        /// Check if the switch is the given state (or has been in the given state for the specified number of seconds)
        /// </summary>
        /// <param name="state">The state to check</param>
        /// <param name="seconds">The time the switch has been in the specified state (secs)</param>
        /// <returns>True if the switch has been in the specified state for the given number of seconds (or is currently in the state if seconds is unspecified)</returns>
        public bool IsState(bool state, double seconds = -1)
        {
            if (_state == state)
            {
                if (seconds != -1)
                {
                    return TimeSinceChange() > seconds;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the switch is currently active or has been active for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds the switch has been active</param>
        /// <returns>True if the switch is active or has been for the specified number of seconds, false otherwise</returns>
        public override bool IsActive(double seconds = -1)
        {
            return IsState(Type == SwitchType.NO, seconds);
        }

        /// <summary>
        /// Checks to see if the switch is currently inactive or has been inactive for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds the switch has been inactive</param>
        /// <returns>True if the switch is inactive or has been for the specified number of seconds, false otherwise</returns>
        public bool IsInactive(double seconds = -1)
        {
            return IsState(Type == SwitchType.NC, seconds);
        }

        /// <summary>
        /// Checks to see if the switch is currently open or has been for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds this switch has been open for</param>
        /// <returns>True if the switch is currently open or has been open for the specified number of seconds, false otherwise.</returns>
        public bool IsOpen(double seconds = -1)
        {
            return IsState(false, seconds);
        }

        /// <summary>
        /// Checks to see if the switch is currently closed or has been for the specified number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds this switch has been closed for</param>
        /// <returns>True if the switch is currently closed or has been closed for the specified number of seconds, false otherwise.</returns>
        public bool IsClosed(double seconds = -1)
        {
            return IsState(true, seconds);
        }



 

        public override string StateString
        {
            get
            {
                return IsClosed() ? "closed" : "open  ";
            }
        }

        public override string ToString()
        {
            return String.Format("<Switch name={0}, address={1}, internal number={2}>", Name, Address, Number);
        }

    }

    public enum SwitchType
    {
        NO = 0, // Normally Open 
        NC = 1 // Normally Closed (Optos)
    };
}
