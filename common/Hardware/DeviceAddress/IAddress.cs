namespace Hardware.DeviceAddress
{
    public interface IAddress
    {
        string AddressString { get; }

        ushort AddressId { get; }

        string HardwareAcronym { get; }

        string HardwareDescription { get; }

        AddressFactory.HardwareType HardwareType { get; }

        bool HardwareMatchesThisAddress(string addressString);

        void UpdateAddressString(string newAddress);
    }
}