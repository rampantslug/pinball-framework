using RampantSlug.Common;
using RampantSlug.PinballClient.ContractImplementations;
using MassTransit;
using RampantSlug.Contracts;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using RampantSlug.PinballClient;
using System.Collections.Generic;
using RampantSlug.PinballClient.ClientDisplays.LogMessages;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree;
using RampantSlug.PinballClient.ClientDisplays.GameStatus;
using RampantSlug.PinballClient.ClientDisplays.Playfield;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient {
    public class MidPanelViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<ShowDeviceConfig>

    {
        private IClientBusController _busController;
        private IEventAggregator _eventAggregator;

        // Client Displays
        public IDeviceInformation DeviceInformation { get; private set; }
        public IPlayfield Playfield { get; private set; }


        [ImportingConstructor]
        public MidPanelViewModel(
            IEventAggregator eventAggregator,
            IDeviceInformation deviceInformation, 
             IPlayfield playfield       
            ) 
        {
            _eventAggregator = eventAggregator;
            DeviceInformation = deviceInformation;
            Playfield = playfield;
        }


    
        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            ActivateItem(DeviceInformation);
            ActivateItem(Playfield);

            _eventAggregator.Subscribe(this);
        }

     
        /// <summary>
        /// Request to update config of a device. Make DeviceInformation active if not already
        /// </summary>
        /// <param name="deviceMessage"></param>
        public void Handle(ShowDeviceConfig deviceMessage)
        {
            ActivateItem(DeviceInformation);
        }
       
    }
}