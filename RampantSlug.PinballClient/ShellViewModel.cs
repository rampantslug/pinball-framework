using RampantSlug.Common;
using RampantSlug.PinballClient.ContractImplementations;
using MassTransit;
using RampantSlug.Contracts;
using RampantSlug.PinballClient.Model;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using RampantSlug.PinballClient;
using System.Collections.Generic;
using RampantSlug.PinballClient.ClientDisplays.LogMessages;
using RampantSlug.PinballClient.ClientDisplays.DeviceConfiguration;
using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree;

namespace RampantSlug.PinballClient {
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell, IPartImportsSatisfiedNotification

    {

        public IDeviceConfiguration DeviceConfiguration { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }

        [ImportMany(typeof(IClientDisplay))]
        private IEnumerable<IClientDisplay> _clientDisplays;

        private IClientBusController _busController;


        [ImportingConstructor]
        public ShellViewModel(ILogMessages logMessages, IDeviceConfiguration deviceConfiguration, ISwitchMatrix switchMatrix, IDeviceTree deviceTree) 
        {
            LogMessages = logMessages;
            DeviceConfiguration = deviceConfiguration;
            SwitchMatrix = switchMatrix;
            DeviceTree = deviceTree;


            _busController = IoC.Get<IClientBusController>();
            _busController.Start();

          /*  _bus = BusInitializer.CreateBus("TestSubscriber", x =>
            {
                x.Subscribe(subs =>
                {
                    subs.Consumer<SimpleMessageConsumer>().Permanent();
                    subs.Consumer<EventMessageConsumer>().Permanent();
                });
            });*/
        }

        // Add all instances of IClientDisplay as tabs to this shell
        public void OnImportsSatisfied() 
        {
            foreach (var clientDisplay in _clientDisplays)
            {
                ActivateItem((Screen)clientDisplay);
            }
        }

        public void Exit() { _busController.Stop(); }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);
          //  ActivateItem(new LogViewModel()); // Retrieve this from config file 
            // instead of hardcoded (then save it back to config if successful connection)


        }

        public void GetSettings()
        {
            _busController.RequestSettings();
        }
       
    }
}