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

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class SwitchConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private SwitchViewModel _switch;
        private ImageSource _refinedTypeImage;
        private ObservableCollection<string> _supportedHardwareSwitches;
        private string _selectedSupportedHardwareSwitch;
        private ushort _directSwitchId;
        private ushort _matrixColumn;
        private ushort _matrixRow;


        private ObservableCollection<string> _inputWirePrimaryColors;
        private string _selectedInputWirePrimaryColor;
        private ObservableCollection<string> _inputWireSecondaryColors;
        private string _selectedInputWireSecondaryColor;

        private ObservableCollection<string> _outputWirePrimaryColors;
        private string _selectedOutputWirePrimaryColor;
        private ObservableCollection<string> _outputWireSecondaryColors;
        private string _selectedOutputWireSecondaryColor;

        public DynamicWireIconViewModel InputWire { get; private set; }
        public DynamicWireIconViewModel OutputWire { get; private set; }

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

        public string Address
        {
            get { return _switch.Address; }
            set
            {
                _switch.Address = value;
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

   /*     public string Type
        {
            get { return _switch.Type.ToString(); }
            set
            {
                _switch.Type = (SwitchType) Enum.Parse(typeof (SwitchType),value, true);
                NotifyOfPropertyChange(() => Type);
            }
        }*/



    /*    public DateTime LastChangeTimeStamp
        {
            get { return _switch.LastChangeTimeStamp; }
            set
            {
                _switch.LastChangeTimeStamp = value;
                NotifyOfPropertyChange(() => LastChangeTimeStamp);
            }
        }

        // TODO: Should this be an enum of available colours based on what is set for the project        
        public string WiringColors
        {
            get { return _switch.WiringColors; }
            set
            {
                _switch.WiringColors = value;
                NotifyOfPropertyChange(() => WiringColors);
            }
        }*/

        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _switch.PreviousStates;
            }

        }

        public ObservableCollection<string> SupportedHardwareSwitches
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

        public string SelectedSupportedHardwareSwitch
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
            get
            {
                string psmName;
                if (SupportedHardware.SwitchHardware.TryGetValue(SupportedHardware.ProcSwitchMatrix, out psmName))
                {
                    if (string.Equals(psmName, SelectedSupportedHardwareSwitch))
                        return true;
                }
                return false;
            }
        }

        public ushort MatrixColumn
        {
            get { return _matrixColumn; }
            set
            {
                _matrixColumn = value;
                NotifyOfPropertyChange(() => MatrixColumn);
            }
        }

        public ushort MatrixRow
        {
            get { return _matrixRow; }
            set
            {
                _matrixRow = value;
                NotifyOfPropertyChange(() => MatrixRow);
            }
        }

        public ushort DirectSwitchId
        {
            get { return _directSwitchId; }
            set
            {
                _directSwitchId = value;
                NotifyOfPropertyChange(() => DirectSwitchId);
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
            }
        }

        #endregion


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchvm"></param>
        public SwitchConfigurationViewModel(SwitchViewModel switchvm) 
        {
            _switch = switchvm;

            LoadRefinedImage(); 
            _supportedHardwareSwitches = new ObservableCollection<string>();
            foreach (var value in SupportedHardware.SwitchHardware.Values)
            {
                _supportedHardwareSwitches.Add(value);
            }

            InputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            InputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWirePrimaryColors = ColorBrushesHelper.GetColorStrings();
            OutputWireSecondaryColors = ColorBrushesHelper.GetColorStrings();

            InputWire = new DynamicWireIconViewModel()
            {
                PrimaryWireColor = switchvm.InputWirePrimaryBrush,
                SecondaryWireColor = switchvm.InputWireSecondaryBrush
            };
            OutputWire = new DynamicWireIconViewModel()
            {
                PrimaryWireColor = switchvm.OutputWirePrimaryBrush,
                SecondaryWireColor = switchvm.OutputWireSecondaryBrush
            };
        }

        


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
