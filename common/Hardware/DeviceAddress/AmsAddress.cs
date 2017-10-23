namespace Hardware.DeviceAddress
{
    public class AmsAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "AMS";

        public override string HardwareDescription => "Arduino Motor Shield";

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ArduinoMotorShield;
    }
}