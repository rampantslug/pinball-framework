namespace Hardware.DeviceAddress
{
    public class AssAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "ASS";

        public override string HardwareDescription => "Arduino Servo Shield";

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ArduinoServoShield;
    }
}