namespace RampantSlug.Common.DeviceAddress
{
    public class PdsAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym
        {
            get { return "PDS"; }
        }

        public override string HardwareDescription
        {
            get { return "Proc Direct Switch"; }
        }

        public AddressFactory.HardwareType HardwareType
        {
            get { return AddressFactory.HardwareType.ProcDirectSwitch; }
        }

    }
}