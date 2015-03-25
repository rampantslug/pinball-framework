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
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceConfiguration
{
    //[Export(typeof(IClientDisplay))]
    public sealed class DeviceConfigurationViewModel : Conductor<IScreen>.Collection.OneActive, IDeviceConfiguration, IHandle<ShowDeviceConfig>
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
            }
        }


        public DeviceConfigurationViewModel() 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);
            DisplayName = "Device Configuration";

        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);
        }

        public void SaveDevice()
        {
            // TODO: Change this to retrieve updated info from viewmodel before sending to server

            var busController = IoC.Get<IClientBusController>();
            busController.SendDeviceMessage(SelectedDevice); 
        }

        public void Handle(ShowDeviceConfig deviceMessage)
        {
            SelectedDevice = deviceMessage.Device;
        }

 
    }
}
