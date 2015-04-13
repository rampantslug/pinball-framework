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
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{
    public sealed class DeviceInformationViewModel : Conductor<IScreen>.Collection.OneActive, IDeviceInformation, 
        IHandle<ShowDeviceConfig>,
        IHandle<HighlightDevice>
    {
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
            }
        }


        public DeviceInformationViewModel() 
        {
            DisplayName = "Device Info";

        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);
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
