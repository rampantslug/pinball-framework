using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class DriverViewModel : DeviceViewModel
    {
        readonly Driver _driver;

        public DriverViewModel(Driver driver)
        {
            _driver = driver;
        }

        public string DriverName
        {
            get { return _driver.Name; }

        }
    }
}
