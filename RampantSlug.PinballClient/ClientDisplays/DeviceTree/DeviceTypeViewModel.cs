using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes;

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
            foreach (IDevice device in _devices)
            {
                if (device is Driver)
                {
                    base.Children.Add(new DriverViewModel(device as Driver, this));
                }
                else if (device is Switch)
                {
                    base.Children.Add(new SwitchViewModel(device as Switch, this));
                }
                else if (device is Coil)
                {
                    base.Children.Add(new CoilViewModel(device as Coil, this));
                }
                else if (device is Servo)
                {
                    base.Children.Add(new ServoViewModel(device as Servo, this));
                }
                else if (device is StepperMotor)
                {
                    base.Children.Add(new StepperMotorViewModel(device as StepperMotor, this));
                }
                else if (device is DCMotor)
                {
                    base.Children.Add(new DCMotorViewModel(device as DCMotor, this));
                }
                else if (device is Led)
                {
                    base.Children.Add(new LedViewModel(device as Led, this));
                }

            }

        }
    }
}

