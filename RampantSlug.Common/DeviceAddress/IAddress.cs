namespace RampantSlug.Common.DeviceAddress
{
    public interface IAddress
    {
        string AddressString { get; }

        string HardwareAcronym { get; }

        string HardwareDescription { get; }

        AddressFactory.HardwareType HardwareType { get; }

        bool HardwareMatchesThisAddress(string addressString);

        void UpdateAddressString(string newAddress);
    }
}