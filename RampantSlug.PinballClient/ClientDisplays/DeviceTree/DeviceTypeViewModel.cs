using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceTypeViewModel : DeviceItemViewModel
    {
        private readonly DeviceType _deviceType;

        private List<IDevice> _devices;

        public DeviceTypeViewModel(DeviceType deviceType, List<IDevice> devices)
            : base(null)
        {
            _deviceType = deviceType;
            _devices = devices;
            LoadChildren();
        }

        public string DeviceTypeName
        {
            get { return _deviceType.DeviceTypeName; }
        }

        protected override void LoadChildren()
        {
            foreach (Device device in _devices)
            {
                if (device is Driver)
                {
                    base.Children.Add(new DriverViewModel(device as Driver, this));
                }
                else
                {
                    base.Children.Add(new SwitchViewModel(device as Switch, this));
                }

            }

        }
    }
}

