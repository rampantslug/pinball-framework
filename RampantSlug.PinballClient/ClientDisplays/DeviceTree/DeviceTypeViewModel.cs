using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceTypeViewModel : DeviceViewModel
    {
        private readonly DeviceType _deviceType;


        public DeviceTypeViewModel(DeviceType deviceType, ObservableCollection<DeviceViewModel> devices)
        {
            _deviceType = deviceType;

            foreach (var device in devices)
            {
                Children.Add(device);
            }
        }

        public string DeviceTypeName
        {
            get { return _deviceType.DeviceTypeName; }
        }

        public void AddDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            //eventAggregator.PublishOnUIThread(new ShowDeviceConfig() { Device = _device });

            // Determine type of new device based on device type that this corresponds too...

            if (_deviceType is SwitchType)
            {
                eventAggregator.PublishOnUIThread(new ShowSwitchConfig() { SwitchVm = new SwitchViewModel(new Switch()) });
            }

        }

        public new void ChangeVisibility()
        {
            IsVisible = !_isVisible;

            var eventAggregator = IoC.Get<IEventAggregator>();

            if (_deviceType is SwitchType)
            {
                eventAggregator.PublishOnUIThread(new AllSwitchesVisibility() { IsVisible = IsVisible});
            }
            else if (_deviceType is CoilType)
            {
                eventAggregator.PublishOnUIThread(new AllCoilsVisibility() { IsVisible = IsVisible });
            }
            else if (_deviceType is StepperMotorType)
            {
                eventAggregator.PublishOnUIThread(new AllStepperMotorsVisibility() { IsVisible = IsVisible });
            }
            else if (_deviceType is ServoType)
            {
                eventAggregator.PublishOnUIThread(new AllServosVisibility() { IsVisible = IsVisible });
            }
        }
    }
}

