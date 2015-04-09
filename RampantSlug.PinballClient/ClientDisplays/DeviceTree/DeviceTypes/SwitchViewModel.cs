using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
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
