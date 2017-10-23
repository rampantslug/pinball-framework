namespace BusinessObjects.Devices
{
    /// <summary>
    /// Represents a general motor device in a pinball machine.
    /// </summary>
    public class Motor : Device, IDevice
    {


        public override string StateString
        {
            get { return IsActive() ? "rotating" : "inactive"; }
        }
    }
}
