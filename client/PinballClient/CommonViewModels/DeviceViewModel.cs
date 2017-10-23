using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Hardware.DeviceAddress;
using Common;
using PinballClient.ClientComms;
using PinballClient.Events;

namespace PinballClient.CommonViewModels
{
    public class DeviceViewModel : Screen, IDeviceViewModel
    {



        #region Config persisted Properties

        public ushort Number
        {
            get
            {
                return Device.Number;
            }
        }

        public string Name
        {
            get
            {
                return Device.Name;
            }
            set
            {
                Device.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string State
        {
            get
            {
                return Device.StateString;
            }
        }

        public IAddress Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                Device.Address = _address.AddressString;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public SolidColorBrush InputWirePrimaryBrush
        {
            get
            {
                return ColorBrushesHelper.ConvertStringToBrush(Device.InputWirePrimaryColor);
            }
            set
            {
                var colorString = ColorBrushesHelper.ConvertBrushToString(value);
                Device.InputWirePrimaryColor = colorString;
            }
        }

        public SolidColorBrush InputWireSecondaryBrush
        {
            get
            {
                return ColorBrushesHelper.ConvertStringToBrush(Device.InputWireSecondaryColor);
            }
            set
            {
                var colorString = ColorBrushesHelper.ConvertBrushToString(value);
                Device.InputWireSecondaryColor = colorString;
            }
        }

        public SolidColorBrush OutputWirePrimaryBrush
        {
            get
            {
                return ColorBrushesHelper.ConvertStringToBrush(Device.OutputWirePrimaryColor);
            }
            set
            {
                var colorString = ColorBrushesHelper.ConvertBrushToString(value);
                Device.OutputWirePrimaryColor = colorString;
            }
        }

        public SolidColorBrush OutputWireSecondaryBrush
        {
            get
            {
                return ColorBrushesHelper.ConvertStringToBrush(Device.OutputWireSecondaryColor);
            }
            set
            {
                var colorString = ColorBrushesHelper.ConvertBrushToString(value);
                Device.OutputWireSecondaryColor = colorString;
            }
        }

        public double VirtualLocationX
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

        public double VirtualLocationY
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

        public string RefinedType
        {
            get
            {
                return Device.RefinedType;
            }
            set
            {
                Device.RefinedType = value;
                NotifyOfPropertyChange(() => RefinedType);
            }
        }

        public double Angle
        {
            get { return Device.Angle; }
            set
            {
                Device.Angle = value;
                NotifyOfPropertyChange(() => Scale);
            }
        }

        public double Scale
        {
            get { return Device.Scale; }
            set
            {
                Device.Scale = value;
                NotifyOfPropertyChange(() => Scale);
            }
        }


        #endregion

        #region View support properties

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

        public virtual bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                //HighlightSelected();
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public ObservableCollection<DeviceViewModel> Children
        {
            get { return _children; }
        }

        #endregion

        public Geometry PlayfieldGeometry
        {
            get { return _playfieldGeometry; }
            set
            {
                if (Equals(value, _playfieldGeometry)) return;
                _playfieldGeometry = value;
                NotifyOfPropertyChange(() => PlayfieldGeometry);
            }
        }

        public bool IsDeviceActive
        {
            get
            {
                return _device.IsActive();
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

                // If this has been updated then need to refresh all child properties 
                Address = AddressFactory.CreateAddress(_device.Address);

                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => State);
                NotifyOfPropertyChange(() => IsDeviceActive);
                NotifyOfPropertyChange(() => Address);
                NotifyOfPropertyChange(() => VirtualLocationX);
                NotifyOfPropertyChange(() => VirtualLocationY);

            }
        }

        public virtual string DeviceType {
            get { return "Generic Device"; }
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


       

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public DeviceViewModel(IClientCommsController clientCommsController, IEventAggregator eventAggregator)
        {
            ClientCommsController = clientCommsController;
            EventAggregator = eventAggregator;

            _children = new ObservableCollection<DeviceViewModel>();
            _isVisible = true;
            _previousStates = new ObservableCollection<HistoryRowViewModel>();

            
        }

        #endregion



       

        public void ChangeVisibility()
        {
            IsVisible = !_isVisible;
        }


        public virtual void ConfigureDevice()
        {
        }


   /*     public virtual void HighlightSelected()
        {
            IsSelected = true;
        }*/

        public void UpdateDeviceInfo(IDevice device, DateTime timestamp)
        {
            Device = device;

            if (PreviousStates.Count >= 10)
            {
                PreviousStates.Remove(PreviousStates.Last());
            }
            PreviousStates.Insert(0, new HistoryRowViewModel()
            {
                Timestamp = timestamp.ToString(),
                State = device.StateString
            });

        }

        public void Save()
        {
            ClientCommsController.SendConfigureDeviceMessage((dynamic) Device);
        }

        public void Remove()
        {
            // Note that is this is not static cast below there is no compile time checking to ensure
            // that method exists. Consider refactor? Create generic inside BusController and do dynamic cast there?
            ClientCommsController.SendConfigureDeviceMessage((dynamic) Device, true);
        }


        protected IDevice _device;
        private ObservableCollection<HistoryRowViewModel> _previousStates;
        protected bool _isSelected;
        private bool _isVisible;
        private readonly ObservableCollection<DeviceViewModel> _children;
        private IAddress _address;


        protected IClientCommsController ClientCommsController;
        protected IEventAggregator EventAggregator;
        private Geometry _playfieldGeometry;
    }
}

