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
            busController.SendDeviceCommandMessage(Device, "ToggleOpenClosed");
        }

        public string SwitchState
        {
            get { return _switch.State; }
        }

        public void ActivateDeviceState()
        {
           // var eventAggregator = IoC.Get<IEventAggregator>();
            //eventAggregator.PublishOnUIThread(new ShowDeviceConfig() { Device = _device });
        }
    }
}
