using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BusinessObjects.Shapes;
using Hardware.DeviceAddress;
using PinballClient.ClientDisplays.DeviceConfig;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;


namespace PinballClient.ClientDisplays.DeviceConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class LedConfigViewModel : BaseDeviceConfigViewModel
    {

        #region Properties

        protected override string RefinedTypeLocationPrefix => "Leds";

        public LedViewModel Led
        {
            get => _led;
            set
            {
                _led = value;
                NotifyOfPropertyChange(() => Led);
            }
        }

        public new ObservableCollection<HistoryRowViewModel> PreviousStates => Led.PreviousStates;

        public ObservableCollection<IAddress> SupportedHardwareLeds
        {
            get => _supportedHardwareLeds;
            set
            {
                _supportedHardwareLeds = value;
                NotifyOfPropertyChange(() => SupportedHardwareLeds);
            }
        }

        public IAddress SelectedSupportedHardwareLed
        {
            get => _selectedSupportedHardwareLed;
            set
            {
                _selectedSupportedHardwareLed = value;
                NotifyOfPropertyChange(() => SelectedSupportedHardwareLed);
            }
        }

        public ushort LedId
        {
            get => _ledId;
            set
            {
                _ledId = value;
                NotifyOfPropertyChange(() => LedId);

                if (Led.Address is PlbAddress address)
                {
                    address.UpdateAddressId(_ledId);
                }
            }
        }

        public IEnumerable<LedShape> AllShapes => Enum.GetValues(typeof(LedShape)).Cast<LedShape>();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledvm"></param>
        public LedConfigViewModel(LedViewModel ledvm) 
            : base(ledvm)
        {
            _led = ledvm;

            // Initialise Address
            _supportedHardwareLeds = new ObservableCollection<IAddress> { new PlbAddress() };
            SelectedSupportedHardwareLed = SupportedHardwareLeds[0];
            if (Led.Address is PlbAddress procLedBoard)
            {
                LedId = procLedBoard.AddressId;
            }
        }

        public void ActivateLed()
        {
            _led.ActivateLed();
        }

        public void DeactivateLed()
        {
            _led.DeactivateLed();
        }


        private LedViewModel _led;
        private ObservableCollection<IAddress> _supportedHardwareLeds;
        private IAddress _selectedSupportedHardwareLed;
        private ushort _ledId;
    }
}
