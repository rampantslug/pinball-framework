namespace BusinessObjects.Devices
{
    public interface IDevice
    {
        /// <summary>
        /// Number used internally to reference the device.
        /// </summary>
        ushort Number { get; set; }

        /// <summary>
        /// Address to locate the device. Type of Hardware and Location.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Integer mapping of location for Hardware reference.
        /// </summary>
        ushort DeviceId { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Is device currently active.
        /// </summary>
        bool IsActive(double seconds = -1);

        /// <summary>
        /// String representation of the state the device is in.
        /// </summary>
        string StateString { get; }

        /// <summary>
        /// Sets the device state to the given state
        /// </summary>
        /// <param name="state">true if active, false otherwise</param>
        void SetState(bool state);

        string InputWirePrimaryColor { get; set; }
        string InputWireSecondaryColor { get; set; }
        string OutputWirePrimaryColor { get; set; }
        string OutputWireSecondaryColor { get; set; }

        /// <summary>
        /// Horizontal position of the device on the playfield. 
        /// 0 = left most side of the playfield image, 100 = right most .
        /// </summary>
        double VirtualLocationX { get; set; }

        /// <summary>
        /// Vertical position of the device on the playfield. 
        /// 0 = top most side of the playfield image, 100 = bottom most .
        /// </summary>
        double VirtualLocationY { get; set; }

        double Angle { get; set; }

        double Scale { get; set; }

        /// <summary>
        /// More refined type definition of the device.
        /// Matches a suitable image for display in Client software.
        /// </summary>
        string RefinedType { get; set; }

        
    }
}
