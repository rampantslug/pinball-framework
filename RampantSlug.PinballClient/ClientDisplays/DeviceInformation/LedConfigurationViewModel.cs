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

    public class LedConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private LedViewModel _led;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareLeds;
        private IAddress _selectedSupportedHardwareLed;
        private ushort _ledId;

        #region Properties


        public LedViewModel Led
        {
            get { return _led; }
            set
            {
                _led = value;
                NotifyOfPropertyChange(() => Led);
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
                return Led.PreviousStates;
            }

        }

        public ObservableCollection<IAddress> SupportedHardwareLeds
        {
            get
            {
                return _supportedHardwareLeds;
            }
            set
            {
                _supportedHardwareLeds = value;
                NotifyOfPropertyChange(() => SupportedHardwareLeds);
            }
        }

        public IAddress SelectedSupportedHardwareLed
        {
            get { return _selectedSupportedHardwareLed; }
            set
            {
                _selectedSupportedHardwareLed = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareLed);
            }
        }

        public ushort LedId
        {
            get { return _ledId; }
            set
            {
                _ledId = value;
                NotifyOfPropertyChange(() => LedId);
            }
        }
        

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledvm"></param>
        public LedConfigurationViewModel(LedViewModel ledvm) 
        {
            _led = ledvm;

            LoadRefinedImage();

            // Initialise Address
            _supportedHardwareLeds = new ObservableCollection<IAddress> { new PlbAddress() };
            SelectedSupportedHardwareLed = SupportedHardwareLeds[0];
            var procLedBoard = Led.Address as PlbAddress;
            if (procLedBoard != null)
            {
                LedId = procLedBoard.AddressId;
            }
        }



        public void SaveDevice()
        {
           var busController = IoC.Get<IClientBusController>();
           busController.SendConfigureDeviceMessage(_led.Device as Led); 
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_led.Device as Led, true);
        }

        public void ActivateLed()
        {
            _led.ActivateLed();
        }

        public void DeactivateLed()
        {
            _led.DeactivateLed();
        }

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Leds\" + Led.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Leds\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                Led.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }

    }
}
