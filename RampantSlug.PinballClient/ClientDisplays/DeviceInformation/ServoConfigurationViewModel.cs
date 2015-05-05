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

    public class ServoConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private ServoViewModel _servo;
        private ImageSource _refinedTypeImage;

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

        public string Address
        {
            get { return _servo.Address; }
            set
            {
                _servo.Address = value;
                NotifyOfPropertyChange(() => Address);
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
            }
        }


        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _servo.PreviousStates;
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

            var path = System.IO.Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Servos\Micro.png";

            RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
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
 
    }

}
