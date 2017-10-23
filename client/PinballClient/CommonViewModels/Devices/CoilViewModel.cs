using System;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Hardware.DeviceAddress;
using Common.Commands;
using PinballClient.ClientComms;
using PinballClient.Events;

namespace PinballClient.CommonViewModels.Devices
{
    /// <summary>
    /// 
    /// </summary>
    public class CoilViewModel : DeviceViewModel
    {
 
        public override string DeviceType => "Coil";

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
                    EventAggregator.PublishOnUIThread(new HighlightCoilEvent() { CoilVm = this });
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilDevice"></param>
        public CoilViewModel(Coil coilDevice, IClientCommsController clientCommsController, IEventAggregator eventAggregator)
            : base(clientCommsController, eventAggregator)
        {
            Device = coilDevice;

            // New device hasn't had its address set yet
            if (string.IsNullOrEmpty(Device.Address))
            {
                Address = new PdbAddress();
            }
        }

        #endregion


        #region Device Command Methods

        public void ActivateDeviceState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Coil, CoilCommand.PulseActive);
        }

        public void PulseState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Coil, CoilCommand.PulseActive);
        }

        public void HoldState()
        {
            ClientCommsController.SendCommandDeviceMessage(Device as Coil, CoilCommand.HoldActive);
        } 

        #endregion

        public override void ConfigureDevice()
        {
            EventAggregator.PublishOnUIThread(new ShowCoilConfigEvent() { CoilVm = this });
        }

        public void UpdateDeviceInfo(Coil coilDevice, DateTime timestamp)
        {
            base.UpdateDeviceInfo(coilDevice, timestamp);
        }

    }
}
