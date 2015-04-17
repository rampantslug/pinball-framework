using System.Collections.ObjectModel;
using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class DeviceViewModel : Screen, IDeviceViewModel
    {

        protected IDevice _device;
        protected bool _isSelected;
        protected bool _isVisible;
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

        public int VirtualLocationX
        {
            get
            {
                return Device.VirtualLocationX;
            }
            set
            {
                Device.VirtualLocationX = value;
                NotifyOfPropertyChange(() => VirtualLocationX);
            }
        }

        public int VirtualLocationY
        {
            get
            {
                return Device.VirtualLocationY;
            }
            set
            {
                Device.VirtualLocationY = value;
                NotifyOfPropertyChange(() => VirtualLocationY);
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

        public string Name
        {
            get { return Device.Name; }
            set { Device.Name = value; }
        }

        public string Address
        {
            get { return Device.Address; }
            set { Device.Address = value; }
        }

        public void ChangeVisibility()
        {
            IsVisible = !_isVisible;
        }


        public virtual void ConfigureDevice()
        {
        }


        public virtual void HighlightSelected()
        {
            
        }

    }
}

