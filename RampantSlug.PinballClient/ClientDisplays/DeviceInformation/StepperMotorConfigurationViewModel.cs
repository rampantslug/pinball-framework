using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RampantSlug.Common.Commands;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class StepperMotorConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private StepperMotorViewModel _stepperMotor;
        private ImageSource _deviceTypeImage;

        #endregion

        #region Properties

        public StepperMotorViewModel StepperMotor
        {
            get { return _stepperMotor; }
            set
            {
                _stepperMotor = value;
                NotifyOfPropertyChange(() => StepperMotor);
            }
        }

        public string Address
        {
            get { return _stepperMotor.Address; }
            set
            {
                _stepperMotor.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public ImageSource DeviceTypeImage
        {
            get
            {
                return _deviceTypeImage;
            }
            set
            {
                _deviceTypeImage = value;
                NotifyOfPropertyChange(() => DeviceTypeImage);
            }
        }


        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _stepperMotor.PreviousStates;
            }

        }
        

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepperMotor"></param>
        public StepperMotorConfigurationViewModel(StepperMotorViewModel stepperMotor) 
        {
            _stepperMotor = stepperMotor;

            var path = System.IO.Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\StepperMotors\bipolar.png";

            DeviceTypeImage = new BitmapImage(new Uri(additionalpath));
        }

        #endregion

        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_stepperMotor.Device as StepperMotor);
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_stepperMotor.Device as StepperMotor, true);
        }

        public void RotateClockwise()
        {
            _stepperMotor.RotateClockwise();
        }

        public void RotateCounterClockwise()
        {
            _stepperMotor.RotateCounterClockwise();
        }

 
    }
}
