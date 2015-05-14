using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RampantSlug.Common.Commands;
using RampantSlug.Common.DeviceAddress;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class StepperMotorConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private StepperMotorViewModel _stepperMotor;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareStepperMotors;
        private IAddress _selectedSupportedHardwareStepperMotor;
        private ushort _stepperMotorId;



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

        public ImageSource RefinedTypeImage
        {
            get
            {
                return _refinedTypeImage;
            }
            set
            {
                _refinedTypeImage = value;
                NotifyOfPropertyChange(() => RefinedTypeImage);
                NotifyOfPropertyChange(() => RefinedTypeImageExists);
            }
        }

        public bool RefinedTypeImageExists
        {
            get
            {
                return RefinedTypeImage != null;
            }

        }

        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return StepperMotor.PreviousStates;
            }

        }

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

            LoadRefinedImage();

            // Initialise Address
            _supportedHardwareStepperMotors = new ObservableCollection<IAddress> { new AmsAddress() };
            SelectedSupportedHardwareStepperMotor = SupportedHardwareStepperMotors[0];
            var arduinoMotorShield = StepperMotor.Address as AmsAddress;
            if (arduinoMotorShield != null)
            {
                StepperMotorId = arduinoMotorShield.AddressId;
            }

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

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\StepperMotors\" + StepperMotor.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\StepperMotors\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                StepperMotor.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }
    }
}
