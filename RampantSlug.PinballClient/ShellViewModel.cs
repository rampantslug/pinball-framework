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
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell, IHandle<ShowDeviceConfig>

    {
        private IClientBusController _busController;
        private IEventAggregator _eventAggregator;


        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;


        // Client Displays
        public IDeviceInformation DeviceInformation { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }
        public IGameStatus GameStatus { get; private set; }
        public IPlayfield Playfield { get; private set; }


        public MidPanelViewModel MidPanel { get; private set; }

        public BindableCollection<IScreen> MidTabs
        {
            get
            {
                return _midTabs;
            }
            set
            {
                _midTabs = value;
                NotifyOfPropertyChange(() => MidTabs);
            }
        }

        public BindableCollection<IScreen> RightTabs
        {
            get
            {
                return _rightTabs;
            }
            set
            {
                _rightTabs = value;
                NotifyOfPropertyChange(() => RightTabs);
            }
        }


        [ImportingConstructor]
        public ShellViewModel(
            IEventAggregator eventAggregator,
            ILogMessages logMessages,
            IDeviceInformation deviceInformation, 
            ISwitchMatrix switchMatrix, 
            IDeviceTree deviceTree,
            IGameStatus gameStatus,
            IPlayfield playfield       
            ) 
        {
            _eventAggregator = eventAggregator;
            LogMessages = logMessages;
            DeviceInformation = deviceInformation;
            SwitchMatrix = switchMatrix;
            DeviceTree = deviceTree;
            GameStatus = gameStatus;
            Playfield = playfield;

            _busController = IoC.Get<IClientBusController>();
            _busController.Start();

            MidTabs = new BindableCollection<IScreen>();
            RightTabs = new BindableCollection<IScreen>();
            
            DisplayName = "Client";

            MidPanel = new MidPanelViewModel(eventAggregator, deviceInformation, playfield);
        }


        public void Exit()
        {
            _busController.Stop();
        }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            //MidTabs.Add(Playfield);
            //MidTabs.Add(DeviceInformation);

            RightTabs.Add(SwitchMatrix);
            RightTabs.Add(GameStatus);

            _eventAggregator.Subscribe(this);

            _busController.RequestSettings();
        }

        public void GetSettings()
        {
            _busController.RequestSettings();
        }

        /// <summary>
        /// Request to update config of a device. Make DeviceInformation active if not already
        /// </summary>
        /// <param name="deviceMessage"></param>
        public void Handle(ShowDeviceConfig deviceMessage)
        {
            Playfield.Deactivate(false);
            DeviceInformation.Activate();
        }

        protected override void OnDeactivate(bool close)
        {
            Exit();
            base.OnDeactivate(close);
        }
       
    }
}