namespace Hardware.DeviceAddress
{
    public class PlbAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "PLB";

        public override string HardwareDescription => "Proc Led Board";

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ProcLedBoard;
    }
}