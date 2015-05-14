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

    public class ServoConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private ServoViewModel _servo;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareServos;
        private IAddress _selectedSupportedHardwareServo;
        private ushort _servoId;

        #endregion

        #region Properties

        public ServoViewModel Servo
        {
            get { return _servo; }
            set
            {
                _servo = value;
                NotifyOfPropertyChange(() => Servo);
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
                return Servo.PreviousStates;
            }

        }

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
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servo"></param>
        public ServoConfigurationViewModel(ServoViewModel servo) 
        {
            _servo = servo;

            LoadRefinedImage();

            // Initialise Address
            _supportedHardwareServos = new ObservableCollection<IAddress> { new AssAddress() };
            SelectedSupportedHardwareServo = SupportedHardwareServos[0];
            var arduinoServoShield = Servo.Address as AssAddress;
            if (arduinoServoShield != null)
            {
                ServoId = arduinoServoShield.AddressId;
            }
        }

        #endregion

        public void RotateClockwise()
        {
            _servo.RotateClockwise();
        }

        public void RotateCounterClockwise()
        {
            _servo.RotateCounterClockwise();
        }

        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_servo.Device as Servo); 
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_servo.Device as Servo, true);
        }

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Servos\" + Servo.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Servos\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                Servo.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }

    }

}
