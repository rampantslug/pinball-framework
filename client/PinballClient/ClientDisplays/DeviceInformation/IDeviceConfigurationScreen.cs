using Caliburn.Micro;

namespace PinballClient.ClientDisplays.DeviceInformation
{
    public interface IDeviceConfigurationScreen : IScreen
    {

        void SaveDevice();

        void RemoveDevice();
    }
}