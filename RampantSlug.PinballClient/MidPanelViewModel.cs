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
    public class MidPanelViewModel : Conductor<IScreen>.Collection.AllActive, IHandle<ShowSwitchConfig>

    {
        private IClientBusController _busController;
        private IEventAggregator _eventAggregator;

        // Client Displays
        public IDeviceInformation DeviceInformation { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IGameStatus GameStatus { get; private set; }


        [ImportingConstructor]
        public MidPanelViewModel(
            IEventAggregator eventAggregator,
            IDeviceInformation deviceInformation, 
            ISwitchMatrix switchMatrix,
            IGameStatus gameStatus
            ) 
        {
            _eventAggregator = eventAggregator;
            DeviceInformation = deviceInformation;
            SwitchMatrix = switchMatrix;
            GameStatus = gameStatus;
        }


    
        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            ActivateItem(GameStatus);
            ActivateItem(SwitchMatrix);
            ActivateItem(DeviceInformation);
            

            _eventAggregator.Subscribe(this);
        }

     
        /// <summary>
        /// Request to update config of a device. Make DeviceInformation active if not already
        /// </summary>
        /// <param name="deviceMessage"></param>
        public void Handle(ShowSwitchConfig deviceMessage)
        {
            ActivateItem(DeviceInformation);
        }
       
    }
}