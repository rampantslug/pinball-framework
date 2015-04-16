using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class SwitchViewModel : DeviceViewModel, IDeviceViewModel
    {
        private ObservableCollection<HistoryRowViewModel> _previousStates;
        private string _switchState;
        private string _switchName;

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

        public SwitchViewModel(Switch switchDevice)
        {
            _device = switchDevice;
            SwitchState = switchDevice.State;
            SwitchName = switchDevice.Name;

            _previousStates = new ObservableCollection<HistoryRowViewModel>();
        }

        public string SwitchName
        {
            get { return _switchName; }
            set
            {
                _switchName = value;
                NotifyOfPropertyChange(() => SwitchName);
            }
        }

        public string SwitchState
        {
            get { return _switchState; }
            private set
            {
                _switchState = value;
                NotifyOfPropertyChange(() => SwitchState);
            }
        }

        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _previousStates;
            }
            private set
            {
                _previousStates = value;
                NotifyOfPropertyChange(() => PreviousStates);
            }
        }

        public void ActivateDeviceState()
        {
            var busController = IoC.Get<IClientBusController>();
            var sw = Device as Switch;
            busController.SendCommandDeviceMessage(sw, "ToggleOpenClosed");
        }

        public override void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowSwitchConfig() { SwitchVm = this });
        }

        public override void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightSwitch() { SwitchVm = this });
        }

        public void UpdateDeviceInfo(Switch switchDevice, DateTime timestamp)
        {
            if (PreviousStates.Count > 10)
            {
                PreviousStates.Remove(PreviousStates.Last());
            }
            PreviousStates.Insert(0, new HistoryRowViewModel()
            {
                Timestamp = timestamp.ToString(),
                State = switchDevice.State
            });

            // Update stuff.
            _device = switchDevice;

            SwitchState = switchDevice.State;
            // TEst name change
            SwitchName = "Test name change";

            //this.Refresh();
        }
    }
}
