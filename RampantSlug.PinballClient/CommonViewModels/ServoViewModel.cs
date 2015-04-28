using System;
using System.Linq;
using Caliburn.Micro;
using RampantSlug.Common.Commands;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class ServoViewModel : DeviceViewModel
    {

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
            get { return "Servo"; }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servoDevice"></param>
        public ServoViewModel(Servo servoDevice)
        {
            Device = servoDevice;
        }

        #endregion

        #region Device Command Methods

        public void RotateClockwise()
        {
            var busController = IoC.Get<IClientBusController>();
            var servo = Device as Servo;
            busController.SendCommandDeviceMessage(servo, ServoCommand.ToClockwiseLimit);
        }

        public void RotateCounterClockwise()
        {
            var busController = IoC.Get<IClientBusController>();
            var servo = Device as Servo;
            busController.SendCommandDeviceMessage(servo, ServoCommand.ToCounterClockwiseLimit);
        }

        #endregion

        public override void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowServoConfig() { ServoVm = this });
        }

        public override void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightServo() { ServoVm = this });
        }

        public void UpdateDeviceInfo(Servo servo, DateTime timestamp)
        {
            base.UpdateDeviceInfo(servo, timestamp);

            // Update stuff.
            //_device = servo;
            //NotifyOfPropertyChange(() => IsActive);

        }
    }
}
