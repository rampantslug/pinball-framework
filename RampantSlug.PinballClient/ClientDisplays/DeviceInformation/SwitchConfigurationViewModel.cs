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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using RampantSlug.Common.Commands;
using RampantSlug.PinballClient.CommonViewModels;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using RampantSlug.Common;
using RampantSlug.Common.DeviceAddress;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class SwitchConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private SwitchViewModel _switch;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<IAddress> _supportedHardwareSwitches;
        private IAddress _selectedSupportedHardwareSwitch;
        private ushort _directSwitchId;
        private ushort _matrixColumn;
        private ushort _matrixRow;
        private ObservableCollection<string> _switchTypes;
        private string _selectedSwitchType;


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


        public SwitchViewModel Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                NotifyOfPropertyChange(() => Switch);
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
                return _switch.PreviousStates;
            }

        }

        public ObservableCollection<string> SwitchTypes
        {
            get
            {
                return _switchTypes;
            }
            set
            {
                _switchTypes = value;
                NotifyOfPropertyChange(() => SwitchTypes);
            }
        }

        public string SelectedSwitchType
        {
            get
            {
                return _selectedSwitchType;
            }
            set
            {
                _selectedSwitchType = value;
                NotifyOfPropertyChange(() => SelectedSwitchType);
                if (string.Equals(SelectedSwitchType, SwitchTypes[0]))
                {
                    _switch.Type = SwitchType.NO;
                }
                else
                {
                    _switch.Type = SwitchType.NC;
                }
            }
        }

        public ObservableCollection<IAddress> SupportedHardwareSwitches
        {
            get
            {
                return _supportedHardwareSwitches;
            }
            set
            {
                _supportedHardwareSwitches = value;
                NotifyOfPropertyChange(() => SupportedHardwareSwitches);
            }
        }

        public IAddress SelectedSupportedHardwareSwitch
        {
            get { return _selectedSupportedHardwareSwitch; }
            set
            {
                _selectedSupportedHardwareSwitch = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareSwitch);
                NotifyOfPropertyChange(() => IsMatrixHardware);
            }
        }

        public bool IsMatrixHardware
        {
            get { return SelectedSupportedHardwareSwitch is PsmAddress; }
        }

        public ushort MatrixColumn
        {
            get { return _matrixColumn; }
            set
            {
                _matrixColumn = value;
                NotifyOfPropertyChange(() => MatrixColumn);

                if (IsMatrixHardware)
                {
                    var address = new PsmAddress();
                    address.UpdateColumn(MatrixColumn);
                    address.UpdateRow(MatrixRow);
                    _switch.Address = address;
                    NotifyOfPropertyChange(() => Switch);
                }   
            }
        }

        public ushort MatrixRow
        {
            get { return _matrixRow; }
            set
            {
                _matrixRow = value;
                NotifyOfPropertyChange(() => MatrixRow);

                if (IsMatrixHardware)
                {
                    var address = new PsmAddress();
                    address.UpdateColumn(MatrixColumn);
                    address.UpdateRow(MatrixRow);
                    _switch.Address = address;
                    NotifyOfPropertyChange(() => Switch);
                }   
            }
        }

        public ushort DirectSwitchId
        {
            get { return _directSwitchId; }
            set
            {
                _directSwitchId = value;
                NotifyOfPropertyChange(() => DirectSwitchId);

                if (!IsMatrixHardware)
                {
                    var address = new PdsAddress();
                    address.UpdateAddressId(DirectSwitchId);
                    _switch.Address = address;
                    NotifyOfPropertyChange(() => Switch);
                }             
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
                Switch.InputWirePrimaryBrush = InputWire.PrimaryWireColor;
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
                Switch.InputWireSecondaryBrush = InputWire.SecondaryWireColor;
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
                Switch.OutputWirePrimaryBrush = OutputWire.PrimaryWireColor;
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
                Switch.OutputWireSecondaryBrush = OutputWire.SecondaryWireColor;
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
        /// <param name="switchvm"></param>
        public SwitchConfigurationViewModel(SwitchViewModel switchvm) 
        {
            Switch = switchvm;

            LoadRefinedImage(); 
            
            SwitchTypes = new ObservableCollection<string>(){"Normally Open", "Normally Closed"};
            SelectedSwitchType = _switch.Type == SwitchType.NO ? SwitchTypes[0] : SwitchTypes[1];

            // Initialise Address
            _supportedHardwareSwitches = new ObservableCollection<IAddress> {new PsmAddress(), new PdsAddress()};

            var procSwitchMatrix = _switch.Address as PsmAddress;
            if (procSwitchMatrix != null)
            {
                SelectedSupportedHardwareSwitch = SupportedHardwareSwitches[0];
                MatrixColumn = procSwitchMatrix.MatrixColumn;
                MatrixRow = procSwitchMatrix.MatrixRow;
            }
            else
            {
                var procDirectSwitch = _switch.Address as PdsAddress;
                if (procDirectSwitch != null)
                {
                    SelectedSupportedHardwareSwitch = SupportedHardwareSwitches[1];
                    DirectSwitchId = procDirectSwitch.AddressId;
                }
            }


            // Initialise Wire Colours 
            InputWire = new DynamicWireIconViewModel();
            OutputWire = new DynamicWireIconViewModel();

            InputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            InputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();

            SelectedInputWirePrimaryColor = ColorBrushesHelper.ConvertBrushToString(Switch.InputWirePrimaryBrush);
            SelectedInputWireSecondaryColor = ColorBrushesHelper.ConvertBrushToString(Switch.InputWireSecondaryBrush);
            SelectedOutputWirePrimaryColor = ColorBrushesHelper.ConvertBrushToString(Switch.OutputWirePrimaryBrush);
            SelectedOutputWireSecondaryColor = ColorBrushesHelper.ConvertBrushToString(Switch.OutputWireSecondaryBrush);            
        }

        #endregion


        public void SaveDevice()
        {
           var busController = IoC.Get<IClientBusController>();
           busController.SendConfigureDeviceMessage(_switch.Device as Switch); 
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_switch.Device as Switch, true);
        }

        public void PressState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.PressActive);
        }

        public void HoldState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.HoldActive);
        }

        private void LoadRefinedImage()
        {
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Switches\" + _switch.RefinedType + ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
        }

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var path = Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Switches\";

            var dialog = new GallerySelectorDialog(additionalpath, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                _switch.RefinedType = result;
                LoadRefinedImage();
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }

    }

    
}
