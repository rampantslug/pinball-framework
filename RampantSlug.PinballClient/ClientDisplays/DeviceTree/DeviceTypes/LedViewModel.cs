using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class LedViewModel : DeviceItemViewModel
    {
        readonly Led _led;

        public LedViewModel(Led ledDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _led = ledDevice;
            _device = ledDevice;
        }

        public string LedName
        {
            get { return _led.Name; }
        }
    }
}
