using Caliburn.Micro;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{
    public interface IDeviceConfigurationScreen : IScreen
    {

        void SaveDevice();

        void RemoveDevice();
    }
}