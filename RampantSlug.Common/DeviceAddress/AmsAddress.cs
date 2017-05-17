namespace RampantSlug.Common.DeviceAddress
{
    public class AmsAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym
        {
            get { return "AMS"; }
        }

        public override string HardwareDescription
        {
            get { return "Arduino Motor Shield"; }
        }

        public AddressFactory.HardwareType HardwareType
        {
            get { return AddressFactory.HardwareType.ArduinoMotorShield; }
        }
    }
}