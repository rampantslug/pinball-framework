namespace RampantSlug.Common.DeviceAddress
{
    public class AssAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym
        {
            get { return "ASS"; }
        }

        public override string HardwareDescription
        {
            get { return "Arduino Servo Shield"; }
        }

        public AddressFactory.HardwareType HardwareType
        {
            get { return AddressFactory.HardwareType.ArduinoServoShield; }
        }

    }
}