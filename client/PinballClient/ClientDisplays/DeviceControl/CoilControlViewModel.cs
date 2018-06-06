using System.Collections.ObjectModel;
using Hardware.DeviceAddress;
using Common;
using PinballClient.ClientDisplays.DeviceConfig;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient.ClientDisplays.DeviceControl
{

    public class CoilControlViewModel : DeviceControlViewModel
    {
        #region Fields

        private CoilViewModel _coil;
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

        protected override string RefinedTypeLocationPrefix => "Coils";

        public CoilViewModel Coil
        {
            get { return _coil; }
            set
            {
                _coil = value;
                NotifyOfPropertyChange(() => Coil);
            }
        }

        public new ObservableCollection<HistoryRowViewModel> PreviousStates => _coil.PreviousStates;

        public new ObservableCollection<IAddress> SupportedHardwareCoils
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

        public new IAddress SelectedSupportedHardwareCoil
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
                var address = Coil.Address as PdbAddress;
                if (address != null)
                {
                    address.UpdateAddressId(_coilId);
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
        public CoilControlViewModel(CoilViewModel coilDevice)
            : base(coilDevice)
        {
            _coil = coilDevice;

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
            Coil.PulseState();
        }

        public void HoldState()
        {
            Coil.HoldState();
        } 
    }
}
