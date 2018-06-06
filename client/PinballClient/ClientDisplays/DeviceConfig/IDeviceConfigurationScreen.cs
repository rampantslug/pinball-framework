using Caliburn.Micro;

namespace PinballClient.ClientDisplays.DeviceConfig
{
    public interface IDeviceConfigurationScreen : IScreen
    {

        void SaveDevice();

        void RemoveDevice();
    }
}