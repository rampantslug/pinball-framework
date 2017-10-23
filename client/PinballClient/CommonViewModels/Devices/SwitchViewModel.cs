using System;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Hardware.DeviceAddress;
using Common;
using Common.Commands;
using PinballClient.ClientComms;
using PinballClient.Events;

namespace PinballClient.CommonViewModels.Devices
{
    public class SwitchViewModel : DeviceViewModel, IDeviceViewModel
    {
       
        //private string _switchState;

       

        public override string DeviceType => "Switch";


        public SwitchType Type
        {
            get
            {
                return (Device as Switch).Type;
            }
            set
            {
                var sw = Device as Switch;
                sw.Type = value;
                NotifyOfPropertyChange(()=> Type);
            }
        }


        public override bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    EventAggregator.PublishOnUIThread(new HighlightSwitchEvent() { SwitchVm = this });
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchDevice"></param>
        public SwitchViewModel(Switch switchDevice, IClientCommsController clientCommsController, IEventAggregator eventAggregator)
            : base(clientCommsController, eventAggregator)
        {
            Device = switchDevice;

            InputWirePrimaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.InputWirePrimaryColor);
            InputWireSecondaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.InputWireSecondaryColor);
            OutputWirePrimaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.OutputWirePrimaryColor);
            OutputWireSecondaryBrush = ColorBrushesHelper.ConvertStringToBrush(switchDevice.OutputWireSecondaryColor);

            // New device hasn't had its address set yet
            if (string.IsNullOrEmpty(Device.Address))
            {
                Address = new PsmAddress();
            }
        }

        #endregion


        #region Device Command Methods

        public void ActivateDeviceState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Switch, SwitchCommand.PressActive);
        }

        public void PressState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Switch, SwitchCommand.PressActive);
        }

        public void HoldState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Switch, SwitchCommand.HoldActive);
        }

        #endregion

        public override void ConfigureDevice()
        {
            EventAggregator.PublishOnUIThread(new ShowSwitchConfigEvent() { SwitchVm = this });
        }

        public void UpdateDeviceInfo(Switch switchDevice, DateTime timestamp)
        {
            base.UpdateDeviceInfo(switchDevice, timestamp);
        }
    }
}
