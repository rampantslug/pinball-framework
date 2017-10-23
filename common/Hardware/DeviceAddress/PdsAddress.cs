namespace Hardware.DeviceAddress
{
    public class PdsAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "PDS";

        public override string HardwareDescription => "Proc Direct Switch";

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ProcDirectSwitch;
    }
}