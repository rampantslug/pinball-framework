namespace RampantSlug.Common.DeviceAddress
{
    public class PlbAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym
        {
            get { return "PLB"; }
        }

        public override string HardwareDescription
        {
            get { return "Proc Led Board"; }
        }

        public AddressFactory.HardwareType HardwareType
        {
            get { return AddressFactory.HardwareType.ProcLedBoard; }
        }

    }
}