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
    public class StepperMotorViewModel : DeviceViewModel
    {

        public override string DeviceType => "Stepper Motor";

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
                    EventAggregator.PublishOnUIThread(new HighlightStepperMotorEvent() { StepperMotorVm = this });
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepperMotorDevice"></param>
        public StepperMotorViewModel(StepperMotor stepperMotorDevice, IClientCommsController clientCommsController, IEventAggregator eventAggregator)
            : base(clientCommsController, eventAggregator)
        {
            Device = stepperMotorDevice;

            // New device hasn't had its address set yet
            if (string.IsNullOrEmpty(Device.Address))
            {
                Address = new AmsAddress();
            }
        }

        #endregion

        #region Device Command Methods

        public void RotateToOtherLimit()
        {
            // TODO: Check where we are and rotate to other limit
            RotateClockwise();
        }

        public void RotateClockwise()
        {
            var stepperMotor = Device as StepperMotor;
            ClientCommsController.SendCommandDeviceMessage(stepperMotor, StepperMotorCommand.ToClockwiseLimit);
        }

        public void RotateCounterClockwise()
        {
            var stepperMotor = Device as StepperMotor;
            ClientCommsController.SendCommandDeviceMessage(stepperMotor, StepperMotorCommand.ToCounterClockwiseLimit);
        }

        #endregion

        public override void ConfigureDevice()
        {
            EventAggregator.PublishOnUIThread(new ShowStepperMotorConfigEvent() { StepperMotorVm = this });
        }

        public void UpdateDeviceInfo(StepperMotor stepperMotor, DateTime timestamp)
        {
            base.UpdateDeviceInfo(stepperMotor, timestamp);
        }
    }
}
