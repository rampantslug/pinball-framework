using Caliburn.Micro;
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
using Hardware.DeviceAddress;
using Common;
using Common.Commands;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PinballClient.ClientDisplays.DeviceConfig;
using PinballClient.ClientDisplays.Dialogs;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient.ClientDisplays.DeviceControl
{

    public class DeviceControlViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private IDeviceViewModel _deviceViewModel;

        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareCoils;
        private IAddress _selectedSupportedHardwareCoil;




        #endregion

        #region Properties

        protected virtual string RefinedTypeLocationPrefix { get; }

        public IDeviceViewModel DeviceVm
        {
            get { return _deviceViewModel; }
            set
            {
                _deviceViewModel = value;
                NotifyOfPropertyChange(() => DeviceVm);
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
                return _deviceViewModel.PreviousStates;
            }

        }

        public ObservableCollection<IAddress> SupportedHardwareCoils
        {
            get
            {
                return _supportedHardwareCoils;
            }
            set
            {
                _supportedHardwareCoils = value;
                NotifyOfPropertyChange(() => SupportedHardwareCoils);
            }
        }

        public IAddress SelectedSupportedHardwareCoil
        {
            get { return _selectedSupportedHardwareCoil; }
            set
            {
                _selectedSupportedHardwareCoil = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareCoil);
            }
        }
        /*
        public ushort CoilId
        {
            get { return _deId; }
            set
            {
                _coilId = value;
                NotifyOfPropertyChange(() => CoilId);
                var address = Coil.Address as PdbAddress;
                if (address != null)
                {
                    address.UpdateAddressId(_coilId);
                }
            }
        }*/



        #endregion

        #region Constructor


        public DeviceControlViewModel(IDeviceViewModel deviceViewModel)
        {
            _deviceViewModel = deviceViewModel;

            LoadRefinedImage();

            // Initialise Address
     /*       _supportedHardwareCoils = new ObservableCollection<IAddress> {new PdbAddress()};
            SelectedSupportedHardwareCoil = SupportedHardwareCoils[0];
            var procDriverBoard = Coil.Address as PdbAddress;
            if (procDriverBoard != null)
            {
                _deviceId = procDriverBoard.AddressId;
            }
            */
        }

        #endregion

        public void SaveDevice()
        {
            _deviceViewModel.Save();
        }

        public void RemoveDevice()
        {
           _deviceViewModel.Remove();
        }

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\" + RefinedTypeLocationPrefix + @"\" + DeviceVm.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\"+ RefinedTypeLocationPrefix + @"\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                DeviceVm.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }
 
    }
}
