namespace RampantSlug.Common.DeviceAddress
{
    public class PdbAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym
        {
            get { return "PDB"; }
        }

        public override string HardwareDescription
        {
            get { return "Proc Driver Board"; }
        }

        public AddressFactory.HardwareType HardwareType
        {
            get { return AddressFactory.HardwareType.ProcDriverBoard; }
        }

    }
}