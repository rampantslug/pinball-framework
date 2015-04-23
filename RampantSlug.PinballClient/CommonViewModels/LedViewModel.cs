using System;
using System.Linq;
using Caliburn.Micro;
using RampantSlug.Common.Commands;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class LedViewModel : DeviceViewModel
    {

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                HighlightSelected();
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledDevice"></param>
        public LedViewModel(Led ledDevice)
        {
            _device = ledDevice;
        }

        #endregion

        #region Device Command Methods

        public void ActivateLed()
        {
            var busController = IoC.Get<IClientBusController>();
            var led = Device as Led;
            busController.SendCommandDeviceMessage(led, LedCommand.MidIntesityOn);
        }

        public void DeactivateLed()
        {
            var busController = IoC.Get<IClientBusController>();
            var led = Device as Led;
            busController.SendCommandDeviceMessage(led, LedCommand.FullOff);
        }

        #endregion

        public override void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowLedConfig() { LedVm = this });
        }

        public override void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightLed() { LedVm = this });
        }

        public void UpdateDeviceInfo(Led ledDevice, DateTime timestamp)
        {
            base.UpdateDeviceInfo(ledDevice, timestamp);

            // Update stuff.
            _device = ledDevice;
            NotifyOfPropertyChange(() => IsDeviceActive);

        }
    }
}
