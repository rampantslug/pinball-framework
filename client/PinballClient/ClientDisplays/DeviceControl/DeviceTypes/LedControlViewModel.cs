using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BusinessObjects.Shapes;
using Hardware.DeviceAddress;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;


namespace PinballClient.ClientDisplays.DeviceControl
{
    /// <summary>
    /// 
    /// </summary>
    public class LedControlViewModel : BaseDeviceControlViewModel
    {

        #region Properties

        protected override string RefinedTypeLocationPrefix => "Leds";

        public LedViewModel Led
        {
            get { return _led; }
            set
            {
                _led = value;
                NotifyOfPropertyChange(() => Led);
            }
        }

        public new ObservableCollection<HistoryRowViewModel> PreviousStates => Led.PreviousStates;

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

                var address = Led.Address as PlbAddress;
                if (address != null)
                {
                    address.UpdateAddressId(_ledId);
                }
            }
        }

        public IEnumerable<LedShape> AllShapes
        {
            get { return Enum.GetValues(typeof(LedShape)).Cast<LedShape>(); }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledvm"></param>
        public LedControlViewModel(LedViewModel ledvm) 
            : base(ledvm)
        {
            _led = ledvm;

            // Initialise Address
            _supportedHardwareLeds = new ObservableCollection<IAddress> { new PlbAddress() };
            SelectedSupportedHardwareLed = SupportedHardwareLeds[0];
            var procLedBoard = Led.Address as PlbAddress;
            if (procLedBoard != null)
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
