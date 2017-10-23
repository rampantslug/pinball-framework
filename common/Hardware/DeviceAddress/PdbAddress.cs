namespace Hardware.DeviceAddress
{
    public class PdbAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "PDB";

        public override string HardwareDescription => "Proc Driver Board";

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ProcDriverBoard;
    }
}