using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using RampantSlug.Common;
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

        public override string DeviceType
        {
            get { return "Switch"; }
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

        public SolidColorBrush InputWirePrimaryBrush { get; set; }
        public SolidColorBrush InputWireSecondaryBrush { get; set; }
        public SolidColorBrush OutputWirePrimaryBrush { get; set; }
        public SolidColorBrush OutputWireSecondaryBrush { get; set; }
       


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchDevice"></param>
        public SwitchViewModel(Switch switchDevice)
        {
            Device = switchDevice;

            InputWirePrimaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.InputWirePrimaryColor);
            InputWireSecondaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.InputWireSecondaryColor);
            OutputWirePrimaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.OutputWirePrimaryColor);
            OutputWireSecondaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.OutputWireSecondaryColor);
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
            //_device = switchDevice;

            //SwitchState = switchDevice.State;

            //NotifyOfPropertyChange(()=> IsActive);

        }
    }
}
