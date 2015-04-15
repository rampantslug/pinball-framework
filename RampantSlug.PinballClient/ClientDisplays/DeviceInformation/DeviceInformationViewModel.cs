using Caliburn.Micro;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Magnum.Extensions;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{
    public sealed class DeviceInformationViewModel : Conductor<IScreen>.Collection.OneActive, IDeviceInformation, 
        IHandle<ShowDeviceConfig>,
        IHandle<HighlightDevice>
    {

        private IEventAggregator _eventAggregator;
        private IShell _shell;

        public ushort DeviceId
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Number : (ushort) 0;
            }
        }

        public string DeviceType
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.GetType().ToShortTypeName() : string.Empty;
            }
        }

        public string DeviceName
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Name : "Device name";
            }
        }

        public string DeviceAddress
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Address : string.Empty;
            }
        }


        public ImageSource PlayfieldImage
        {
            get
            {
                return _shell != null ? _shell.PlayfieldImage : null;
            }
        }
        
        private IDevice _selectedDevice;

        public IDevice SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                

                
                _selectedDevice = value;
                NotifyOfPropertyChange(() => SelectedDevice);

                var switchDevice = _selectedDevice as Switch;
                if (switchDevice != null)
                {
                    // Update displayed switch 
                    ActivateItem(new SwitchConfigurationViewModel(switchDevice));
                }
                else
                {
                    var coil = _selectedDevice as Coil;
                    if (coil != null)
                    {
                        // Update displayed switch 
                        ActivateItem(new CoilConfigurationViewModel(coil));
                    }
                }
                NotifyOfPropertyChange(() => DeviceId);
                NotifyOfPropertyChange(() => DeviceType);
                NotifyOfPropertyChange(() => DeviceName);
                NotifyOfPropertyChange(() => DeviceAddress);

                if (PlayfieldImage == null)
                {
                    NotifyOfPropertyChange(() => PlayfieldImage);
                }
            }
        }


        public DeviceInformationViewModel() 
        {
            DisplayName = "Device Info";

        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

            _shell = IoC.Get<IShell>();
        }

        public void Handle(ShowDeviceConfig deviceMessage)
        {
            SelectedDevice = deviceMessage.Device;
        }

        public void Handle(HighlightDevice deviceMessage)
        {
            SelectedDevice = deviceMessage.Device;
        }

    }
}
