using System.Collections.ObjectModel;
using Hardware.DeviceAddress;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient.ClientDisplays.DeviceInformation
{
    /// <summary>
    /// 
    /// </summary>
    public class StepperMotorConfigurationViewModel : DeviceConfigurationViewModel
    {
        #region Properties

        protected override string RefinedTypeLocationPrefix => "StepperMotors";

        public StepperMotorViewModel StepperMotor
        {
            get { return _stepperMotor; }
            set
            {
                _stepperMotor = value;
                NotifyOfPropertyChange(() => StepperMotor);
            }
        }


        public new ObservableCollection<HistoryRowViewModel> PreviousStates => StepperMotor.PreviousStates;

        public ObservableCollection<IAddress> SupportedHardwareStepperMotors
        {
            get
            {
                return _supportedHardwareStepperMotors;
            }
            set
            {
                _supportedHardwareStepperMotors = value;
                NotifyOfPropertyChange(() => SupportedHardwareStepperMotors);
            }
        }

        public IAddress SelectedSupportedHardwareStepperMotor
        {
            get { return _selectedSupportedHardwareStepperMotor; }
            set
            {
                _selectedSupportedHardwareStepperMotor = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareStepperMotor);
            }
        }

        public ushort StepperMotorId
        {
            get { return _stepperMotorId; }
            set
            {
                _stepperMotorId = value;
                NotifyOfPropertyChange(() => StepperMotorId);

                var address = StepperMotor.Address as AmsAddress;
                if (address != null)
                {
                    address.UpdateAddressId(_stepperMotorId);
                }
            }
        }


        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepperMotor"></param>
        public StepperMotorConfigurationViewModel(StepperMotorViewModel stepperMotor)
            : base(stepperMotor)
        {
            _stepperMotor = stepperMotor;

            // Initialise Address
            _supportedHardwareStepperMotors = new ObservableCollection<IAddress> { new AmsAddress() };
            SelectedSupportedHardwareStepperMotor = SupportedHardwareStepperMotors[0];
            var arduinoMotorShield = StepperMotor.Address as AmsAddress;
            if (arduinoMotorShield != null)
            {
                StepperMotorId = arduinoMotorShield.AddressId;
            }

        }

        public void RotateClockwise()
        {
            _stepperMotor.RotateClockwise();
        }

        public void RotateCounterClockwise()
        {
            _stepperMotor.RotateCounterClockwise();
        }

        private StepperMotorViewModel _stepperMotor;
        private ObservableCollection<IAddress> _supportedHardwareStepperMotors;
        private IAddress _selectedSupportedHardwareStepperMotor;
        private ushort _stepperMotorId;
    }
}
