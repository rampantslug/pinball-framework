namespace BusinessObjects.Devices
{
    /// <summary>
    /// Represents a coil device in a pinball machine.
    /// </summary>
    public class Coil: Device, IDevice
    {

        public Coil()
        {
             
        }

        public override string StateString
        {
            get { return IsActive() ? "pulsing" : "inactive"; }
        }

        public void Pulse()
        {
            // TODO: Need to pulse the coil at this point!!
        }
    }
}
