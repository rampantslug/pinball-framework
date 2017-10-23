using System;

namespace BusinessObjects.Devices
{
    /// <summary>
    /// Base class for each type of Device found in a Pinball Machine.
    /// </summary>
    public abstract class Device : IDevice
    {
        /// <summary>
        /// The current state of the switch.
        /// 'True' for closed, 'False' for open.
        /// 
        /// Generally, use IsActive() or IsInactive() instead of this
        /// </summary>
        /// 
        // TODO: This should not be public but need to investigate best method for serialising it for Mass Transit
        public bool _state = false;

        /// <summary>
        /// Number used internally to reference the device.
        /// </summary>
        public ushort Number { get; set; }

        /// <summary>
        /// Address to locate the device. Type of Hardware and Location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Integer mapping of location for Hardware reference.
        /// </summary>
        public ushort DeviceId { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// String representation of the state the device is in.
        /// </summary>
        public abstract string StateString { get; }


        public string InputWirePrimaryColor { get; set; }
        public string InputWireSecondaryColor { get; set; }
        public string OutputWirePrimaryColor { get; set; }
        public string OutputWireSecondaryColor { get; set; }


        /// <summary>
        /// Horizontal position of the device on the playfield. 
        /// 0 = left most side of the playfield image, 100 = right most .
        /// </summary>
        public double VirtualLocationX { get; set; }

        /// <summary>
        /// Vertical position of the device on the playfield. 
        /// 0 = top most side of the playfield image, 100 = bottom most .
        /// </summary>
        public double VirtualLocationY { get; set; }

        /// <summary>
        /// Time of the last state change of the device.
        /// </summary>
        public DateTime LastChangeTimeStamp { get; set; }

        /// <summary>
        /// More refined type definition of the device.
        /// Matches a suitable image for display in Client software.
        /// </summary>
        public string RefinedType { get; set; }

        public double Angle { get; set; }
        public double Scale { get; set; }












        public virtual bool IsActive(double seconds = -1)
        {
            return _state;
        }

        public virtual void SetState(bool state)
        {
            _state = state;
            ResetTimer();
        }

        /// <summary>
        /// Set the time of last state change to the current time
        /// </summary>
        public void ResetTimer()
        {
            LastChangeTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Get the number of seconds since the switch has last changed states
        /// </summary>
        /// <returns>The number of seconds since the switch has last changed states</returns>
        public double TimeSinceChange()
        {
            return DateTime.Now.Subtract(LastChangeTimeStamp).TotalSeconds;
        }

    }
}

