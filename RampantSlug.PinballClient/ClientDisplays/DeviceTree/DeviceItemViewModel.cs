using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceItemViewModel : Screen
    {
        readonly ObservableCollection<DeviceItemViewModel> _children;
        readonly DeviceItemViewModel _parent;
        protected IDevice _device;

        bool _isExpanded;
        bool _isSelected;

        private bool _isVisible;

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

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                NotifyOfPropertyChange(() => IsExpanded);
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

       

        #region Constructors

        //  public DeviceItemViewModel(IDevice device)
        //      : this(device, null)
        //  {
        //  }

        public DeviceItemViewModel(DeviceItemViewModel parent)
        {
            // _device = device;
            _parent = parent;

            _children = new ObservableCollection<DeviceItemViewModel>();


            /*     _children = new ReadOnlyCollection<DeviceItemViewModel>(
                         (from child in _device.Children
                          select new DeviceItemViewModel(child, this))
                          .ToList<DeviceItemViewModel>());          
             */

            _isVisible = true;
        }

        #endregion


        public ObservableCollection<DeviceItemViewModel> Children
        {
            get { return _children; }
        }

        #region Parent

        public new DeviceItemViewModel Parent
        {
            get { return _parent; }
        }

        public ushort Number { get { return _device.Number; } }

        #endregion // Parent


        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
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

