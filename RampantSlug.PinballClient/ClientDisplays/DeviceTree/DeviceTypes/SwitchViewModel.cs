using Caliburn.Micro;
using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class SwitchViewModel : DeviceItemViewModel
    {

        public SwitchViewModel(Switch switchDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _device = switchDevice;            
        }

        public string SwitchName
        {
            get { return Device.Name; }
        }

        public string SwitchState
        {
            get
            {
                var sw = Device as Switch;
                return sw != null ? sw.State : null;
            }
        }

        public void ActivateDeviceState()
        {
            var busController = IoC.Get<IClientBusController>();
            var sw = Device as Switch;
            busController.SendCommandDeviceMessage(sw, "ToggleOpenClosed");
        }

      
    }
}
