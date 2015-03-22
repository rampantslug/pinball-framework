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
using RampantSlug.PinballClient.ClientDisplays.GameStatus;
using RampantSlug.PinballClient.ClientDisplays.Playfield;

namespace RampantSlug.PinballClient {
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell//, IPartImportsSatisfiedNotification

    {
        private IClientBusController _busController;
        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;


        // Client Displays
        public IDeviceConfiguration DeviceConfiguration { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }
        public IGameStatus GameStatus { get; private set; }
        public IPlayfield Playfield { get; private set; }


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
            ILogMessages logMessages, 
            IDeviceConfiguration deviceConfiguration, 
            ISwitchMatrix switchMatrix, 
            IDeviceTree deviceTree,
            IGameStatus gameStatus,
            IPlayfield playfield       
            ) 
        {
            LogMessages = logMessages;
            DeviceConfiguration = deviceConfiguration;
            SwitchMatrix = switchMatrix;
            DeviceTree = deviceTree;
            GameStatus = gameStatus;
            Playfield = playfield;

            _busController = IoC.Get<IClientBusController>();
            _busController.Start();

            MidTabs = new BindableCollection<IScreen>();
            RightTabs = new BindableCollection<IScreen>();
        }


        public void Exit()
        {
            _busController.Stop();
        }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            MidTabs.Add(Playfield);
            MidTabs.Add(DeviceConfiguration);

            RightTabs.Add(SwitchMatrix);
            RightTabs.Add(GameStatus);
            
        }

        public void GetSettings()
        {
            _busController.RequestSettings();
        }
       
    }
}