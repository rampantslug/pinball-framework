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
    public class ServoViewModel : DeviceViewModel
    {
        private int _minPulseLength;
        private int _maxPulseLength;
        private int _currentAngle;
        private int _maxAngle;
        private int _minAngle;

        public IObservableCollection<ServoDefinedPosition> ServoPositions { get; set; }

        public int MinAngle
        {
            get
            {
                return _minAngle; }
            set
            {
                _minAngle = value;
                NotifyOfPropertyChange(() => MinAngle);
            }
        }
        public int MaxAngle
        {
            get { return _maxAngle; }
            set
            {
                _maxAngle = value;
                NotifyOfPropertyChange(() => MaxAngle);
            }
        }

        public int CurrentAngle
        {
            get { return _currentAngle; }
            set
            {
                _currentAngle = value;
                NotifyOfPropertyChange(() => CurrentAngle);
            }
        }

        public int MaxPulseLength
        {
            get { return _maxPulseLength; }
            set
            {
                _maxPulseLength = value;
                NotifyOfPropertyChange(() => MaxPulseLength);
            }
        }
        public int MinPulseLength
        {
            get { return _minPulseLength; }
            set
            {
                _minPulseLength = value;
                NotifyOfPropertyChange(() => MinPulseLength);
            }
        }

        public override string DeviceType => "Servo";

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
                    EventAggregator.PublishOnUIThread(new HighlightServoEvent() { ServoVm = this });
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servoDevice"></param>
        public ServoViewModel(Servo servoDevice, IClientCommsController clientCommsController, IEventAggregator eventAggregator)
            : base(clientCommsController, eventAggregator)
        {
            Device = servoDevice;

            // New device hasn't had its address set yet
            if (string.IsNullOrEmpty(Device.Address))
            {
                Address = new AssAddress();
            }

            // Add some test data at the moment... 
            // TODO: This needs to be saved/loaded to servo config.

            ServoPositions = new BindableCollection<ServoDefinedPosition>()
            {
                new ServoDefinedPosition(){Name = "GateOpen", Angle = 160 },
                new ServoDefinedPosition(){Name = "GateClosed", Angle = 20 },
            };

            MinAngle = 5;
            MaxAngle = 175;

            MaxPulseLength = 560;
            MinPulseLength = 120;

            CurrentAngle = 80;
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
            var servo = Device as Servo;
            ClientCommsController.SendCommandDeviceMessage(servo, ServoCommand.ToClockwiseLimit);
        }

        public void RotateCounterClockwise()
        {
            var servo = Device as Servo;
            ClientCommsController.SendCommandDeviceMessage(servo, ServoCommand.ToCounterClockwiseLimit);
        }

        #endregion

        public override void ConfigureDevice()
        {
            EventAggregator.PublishOnUIThread(new ShowServoConfigEvent() { ServoVm = this });
        }

        public void UpdateDeviceInfo(Servo servo, DateTime timestamp)
        {
            base.UpdateDeviceInfo(servo, timestamp);
        }
    }

    public class ServoDefinedPosition: Screen
    {
        private int _angle;
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public int Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                NotifyOfPropertyChange(() => Angle);
            }
        }
    }
}
