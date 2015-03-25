using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
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
