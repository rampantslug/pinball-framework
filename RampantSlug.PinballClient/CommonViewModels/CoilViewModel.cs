using System;
using System.Linq;
using Caliburn.Micro;
using RampantSlug.Common.Commands;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class CoilViewModel : DeviceViewModel
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
        /// <param name="coilDevice"></param>
        public CoilViewModel(Coil coilDevice)
        {
            _device = coilDevice;
        }

        #endregion


        #region Device Command Methods

        public void ActivateDeviceState()
        {
            var busController = IoC.Get<IClientBusController>();
            var coil = Device as Coil;
            busController.SendCommandDeviceMessage(coil, CoilCommand.PulseActive);
        }

        #endregion

        public override void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowCoilConfig() { CoilVm = this });
        }

        public override void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightCoil() { CoilVm = this });
        }

        public void UpdateDeviceInfo(Coil coilDevice, DateTime timestamp)
        {
            // TODO: Move this into DeviceViewModel section
            if (PreviousStates.Count > 10)
            {
                PreviousStates.Remove(PreviousStates.Last());
            }
            PreviousStates.Insert(0, new HistoryRowViewModel()
            {
                Timestamp = timestamp.ToString(),
                State = "No servo states exist yet."
            });

            // Update stuff.
            _device = coilDevice;
            NotifyOfPropertyChange(() => IsDeviceActive);

        }

    }
}
