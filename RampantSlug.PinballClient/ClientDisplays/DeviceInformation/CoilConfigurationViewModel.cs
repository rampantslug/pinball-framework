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
using RampantSlug.Common;
using RampantSlug.Common.Commands;
using RampantSlug.Common.DeviceAddress;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class CoilConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private CoilViewModel _coil;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareCoils;
        private IAddress _selectedSupportedHardwareCoil;
        private ushort _coilId;

        // Wire Coloring...
        private ObservableCollection<string> _inputWirePrimaryColors;
        private string _selectedInputWirePrimaryColor;
        private ObservableCollection<string> _inputWireSecondaryColors;
        private string _selectedInputWireSecondaryColor;

        private ObservableCollection<string> _outputWirePrimaryColors;
        private string _selectedOutputWirePrimaryColor;
        private ObservableCollection<string> _outputWireSecondaryColors;
        private string _selectedOutputWireSecondaryColor;

        #endregion

        #region Properties

        public CoilViewModel Coil
        {
            get { return _coil; }
            set
            {
                _coil = value;
                NotifyOfPropertyChange(() => Coil);
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
                return _coil.PreviousStates;
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

        public ushort CoilId
        {
            get { return _coilId; }
            set
            {
                _coilId = value;
                NotifyOfPropertyChange(() => CoilId);
            }
        }

        #region Wire Coloring

        public ObservableCollection<string> InputWirePrimaryColors
        {
            get
            {
                return _inputWirePrimaryColors;
            }
            set
            {
                _inputWirePrimaryColors = value;
                NotifyOfPropertyChange(() => InputWirePrimaryColors);
            }
        }

        public string SelectedInputWirePrimaryColor
        {
            get { return _selectedInputWirePrimaryColor; }
            set
            {
                _selectedInputWirePrimaryColor = value;
                NotifyOfPropertyChange(() => SelectedInputWirePrimaryColor);
                InputWire.PrimaryWireColor = ColorBrushesHelper.ConvertStringToBrush(SelectedInputWirePrimaryColor);
                Coil.InputWirePrimaryBrush = InputWire.PrimaryWireColor;
            }
        }

        public ObservableCollection<string> InputWireSecondaryColors
        {
            get
            {
                return _inputWireSecondaryColors;
            }
            set
            {
                _inputWireSecondaryColors = value;
                NotifyOfPropertyChange(() => InputWireSecondaryColors);
            }
        }

        public string SelectedInputWireSecondaryColor
        {
            get { return _selectedInputWireSecondaryColor; }
            set
            {
                _selectedInputWireSecondaryColor = value;
                NotifyOfPropertyChange(() => SelectedInputWireSecondaryColor);
                InputWire.SecondaryWireColor = ColorBrushesHelper.ConvertStringToBrush(SelectedInputWireSecondaryColor);
                Coil.InputWireSecondaryBrush = InputWire.SecondaryWireColor;
            }
        }


        public ObservableCollection<string> OutputWirePrimaryColors
        {
            get
            {
                return _outputWirePrimaryColors;
            }
            set
            {
                _outputWirePrimaryColors = value;
                NotifyOfPropertyChange(() => OutputWirePrimaryColors);
            }
        }

        public string SelectedOutputWirePrimaryColor
        {
            get { return _selectedOutputWirePrimaryColor; }
            set
            {
                _selectedOutputWirePrimaryColor = value;
                NotifyOfPropertyChange(() => SelectedOutputWirePrimaryColor);
                OutputWire.PrimaryWireColor = ColorBrushesHelper.ConvertStringToBrush(SelectedOutputWirePrimaryColor);
                Coil.OutputWirePrimaryBrush = OutputWire.PrimaryWireColor;
            }
        }

        public ObservableCollection<string> OutputWireSecondaryColors
        {
            get
            {
                return _outputWireSecondaryColors;
            }
            set
            {
                _outputWireSecondaryColors = value;
                NotifyOfPropertyChange(() => OutputWireSecondaryColors);
            }
        }

        public string SelectedOutputWireSecondaryColor
        {
            get { return _selectedOutputWireSecondaryColor; }
            set
            {
                _selectedOutputWireSecondaryColor = value;
                NotifyOfPropertyChange(() => SelectedOutputWireSecondaryColor);
                OutputWire.SecondaryWireColor = ColorBrushesHelper.ConvertStringToBrush(SelectedOutputWireSecondaryColor);
                Coil.OutputWireSecondaryBrush = OutputWire.SecondaryWireColor;
            }
        }

        public DynamicWireIconViewModel InputWire { get; private set; }
        public DynamicWireIconViewModel OutputWire { get; private set; }

        #endregion


        #endregion

        #region Constructor
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilDevice"></param>
        public CoilConfigurationViewModel(CoilViewModel coilDevice)
        {
            _coil = coilDevice;

            LoadRefinedImage();

            // Initialise Address
            _supportedHardwareCoils = new ObservableCollection<IAddress> { new PdbAddress() };
            SelectedSupportedHardwareCoil = SupportedHardwareCoils[0];
            var procDriverBoard = Coil.Address as PdbAddress;
            if (procDriverBoard != null)
            {
                CoilId = procDriverBoard.AddressId;
            }

            // Initialise Wire Colours 
            InputWire = new DynamicWireIconViewModel();
            OutputWire = new DynamicWireIconViewModel();

            InputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            InputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();

            SelectedInputWirePrimaryColor = ColorBrushesHelper.ConvertBrushToString(Coil.InputWirePrimaryBrush);
            SelectedInputWireSecondaryColor = ColorBrushesHelper.ConvertBrushToString(Coil.InputWireSecondaryBrush);
            SelectedOutputWirePrimaryColor = ColorBrushesHelper.ConvertBrushToString(Coil.OutputWirePrimaryBrush);
            SelectedOutputWireSecondaryColor = ColorBrushesHelper.ConvertBrushToString(Coil.OutputWireSecondaryBrush);            
        }

        #endregion

        public void PulseState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_coil.Device as Coil, CoilCommand.PulseActive);
        }

        public void HoldState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_coil.Device as Coil, CoilCommand.HoldActive);
        }

        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_coil.Device as Coil);
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_coil.Device as Coil, true);
        }

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Coils\" + Coil.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Coils\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                Coil.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }
 
    }
}
