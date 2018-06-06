using System.Collections.ObjectModel;
using BusinessObjects.Devices;
using Common;
using Hardware.DeviceAddress;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;


namespace PinballClient.ClientDisplays.DeviceConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class SwitchConfigViewModel : DeviceConfigViewModel
    {

        #region Properties

        protected override string RefinedTypeLocationPrefix => "Switches";

        public SwitchViewModel Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                NotifyOfPropertyChange(() => Switch);
            }
        }

        public new ObservableCollection<HistoryRowViewModel> PreviousStates => _switch.PreviousStates;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchvm"></param>
        public SwitchConfigViewModel(SwitchViewModel switchvm)
            : base(switchvm)
        {
            Switch = switchvm;
            
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

        public void PressState()
        {
            _switch.PressState();
        }

        public void HoldState()
        {
            _switch.HoldState();
        }


        private SwitchViewModel _switch;
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
    }

    
}
