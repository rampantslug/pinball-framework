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

namespace RampantSlug.PinballClient.ClientDisplays.DeviceConfiguration
{
    //[Export(typeof(IClientDisplay))]
    public class DeviceConfigurationViewModel : Conductor<IScreen>.Collection.OneActive,  IDeviceConfiguration, IHandle<ConfigureDevice>
    {
        private Switch _selectedSwitch;


        public string ClientDisplayName { get { return "Device Configuration"; } }


        public Switch SelectedSwitch
        {
            get
            {
                return _selectedSwitch;
            }
            set
            {
                _selectedSwitch = value;
                NotifyOfPropertyChange(() => SelectedSwitch);

                // Update displayed switch 
                ActivateItem(new SwitchConfigurationViewModel(_selectedSwitch));
            }
        }


        public DeviceConfigurationViewModel() 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

           
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
            busController.SendDeviceMessage(SelectedSwitch); 
        }

        public void Handle(ConfigureDevice deviceMessage)
        {
            SelectedSwitch = deviceMessage.Device;
        }

 
    }
}
