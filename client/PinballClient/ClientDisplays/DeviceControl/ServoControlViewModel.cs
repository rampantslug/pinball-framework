using System.Collections.ObjectModel;
using Hardware.DeviceAddress;
using PinballClient.ClientDisplays.DeviceInformation;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;


namespace PinballClient.ClientDisplays.DeviceControl
{
    /// <summary>
    /// Class for controlling a Servo.
    /// </summary>
    public class ServoControlViewModel : DeviceControlViewModel
    {

        #region Properties

        protected override string RefinedTypeLocationPrefix => "Servos";

        public ServoViewModel Servo
        {
            get { return _servo; }
            set
            {
                _servo = value;
                NotifyOfPropertyChange(() => Servo);
            }
        }

        public new ObservableCollection<HistoryRowViewModel> PreviousStates => Servo.PreviousStates;

        public ObservableCollection<IAddress> SupportedHardwareServos
        {
            get
            {
                return _supportedHardwareServos;
            }
            set
            {
                _supportedHardwareServos = value;
                NotifyOfPropertyChange(() => SupportedHardwareServos);
            }
        }

        public IAddress SelectedSupportedHardwareServo
        {
            get { return _selectedSupportedHardwareServo; }
            set
            {
                _selectedSupportedHardwareServo = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareServo);
            }
        }

        public ushort ServoId
        {
            get { return _servoId; }
            set
            {
                _servoId = value;
                NotifyOfPropertyChange(() => ServoId);

                var address = Servo.Address as AssAddress;
                if (address != null)
                {
                    address.UpdateAddressId(_servoId);
                }
            }
        }


        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="servo"></param>
        public ServoControlViewModel(ServoViewModel servo)
            : base(servo)
        {
            _servo = servo;

            // Initialise Address
            _supportedHardwareServos = new ObservableCollection<IAddress> { new AssAddress() };
            SelectedSupportedHardwareServo = SupportedHardwareServos[0];
            var arduinoServoShield = Servo.Address as AssAddress;
            if (arduinoServoShield != null)
            {
                ServoId = arduinoServoShield.AddressId;
            }
        }

        public void RotateClockwise()
        {
            _servo.RotateClockwise();
        }

        public void RotateCounterClockwise()
        {
            _servo.RotateCounterClockwise();
        }

        private ServoViewModel _servo;
        private ObservableCollection<IAddress> _supportedHardwareServos;
        private IAddress _selectedSupportedHardwareServo;
        private ushort _servoId;
    }

}
