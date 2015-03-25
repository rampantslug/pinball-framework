using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class SwitchViewModel : DeviceItemViewModel
    {
        readonly Switch _switch;

        public SwitchViewModel(Switch switchDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _switch = switchDevice;
            _device = switchDevice;
        }

        public string SwitchName
        {
            get { return _switch.Name; }
        }
    }
}
