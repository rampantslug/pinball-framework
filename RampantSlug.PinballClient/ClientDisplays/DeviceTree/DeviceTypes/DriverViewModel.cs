using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class DriverViewModel : DeviceItemViewModel
    {
        readonly Driver _driver;

        public DriverViewModel(Driver driver, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _driver = driver;
        }

        public string DriverName
        {
            get { return _driver.Name; }

        }
    }
}
