using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class LedViewModel : DeviceViewModel
    {
        readonly Led _led;

        public LedViewModel(Led ledDevice)
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
