using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using RampantSlug.Common.Commands;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class SwitchViewModel : DeviceViewModel, IDeviceViewModel
    {
       
        private string _switchState;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                HighlightSelected();
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public string SwitchState
        {
            get { return _switchState; }
            private set
            {
                _switchState = value;
                NotifyOfPropertyChange(() => SwitchState);
            }
        }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchDevice"></param>
        public SwitchViewModel(Switch switchDevice)
        {
            _device = switchDevice;
            SwitchState = switchDevice.State;
 
        }

        #endregion





        #region Device Command Methods

        public void ActivateDeviceState()
        {
            var busController = IoC.Get<IClientBusController>();
            var sw = Device as Switch;
            busController.SendCommandDeviceMessage(sw, SwitchCommand.PressActive);
        }

        #endregion

        public override void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowSwitchConfig() { SwitchVm = this });
        }

        public override void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightSwitch() { SwitchVm = this });
        }

        public void UpdateDeviceInfo(Switch switchDevice, DateTime timestamp)
        {
            base.UpdateDeviceInfo(switchDevice, timestamp);

            // Update stuff.
            _device = switchDevice;

            SwitchState = switchDevice.State;

            NotifyOfPropertyChange(()=> IsDeviceActive);

        }
    }
}
