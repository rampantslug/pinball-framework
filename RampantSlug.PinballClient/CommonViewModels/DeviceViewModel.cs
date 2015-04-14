using System.Collections.ObjectModel;
using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class DeviceViewModel : Screen
    {

        protected IDevice _device;
        private bool _isSelected;
        private bool _isVisible;
        private ObservableCollection<DeviceViewModel> _children;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }

        public bool IsDeviceActive
        {
            get
            {
                return _device.IsDeviceActive;
            }
        }

        public IDevice Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                NotifyOfPropertyChange(() => Device);
                NotifyOfPropertyChange(() => IsDeviceActive);
            }
        }

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

        public ObservableCollection<DeviceViewModel> Children
        {
            get { return _children; }
        }

        #region Constructors


        public DeviceViewModel()
        {
            _children = new ObservableCollection<DeviceViewModel>();
            _isVisible = true;
        }

        #endregion



        public ushort Number {
            get
            {
                return _device.Number;
            } 
        }


        public void ConfigureDevice()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new ShowDeviceConfig() { Device = _device });
        }

        public void ChangeVisibility()
        {
            IsVisible = !_isVisible;
        }

        private void HighlightSelected()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new HighlightDevice() { Device = _device });
        }

    }
}

