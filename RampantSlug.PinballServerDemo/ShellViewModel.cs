using System;
using System.ComponentModel;
using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
using RampantSlug.ServerLibrary.Events;
using RampantSlug.ServerLibrary.Hardware;

namespace RampantSlug.PinballServerDemo
{
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell, IHandle<UpdateDisplayEvent>
    {
        private IGameController _gameController;
        private BindableCollection<Switch> _switches;
        private int _playerScore;
        // private GameLibraryBootstrapper _gameLibrary;
        private IEventAggregator _eventAggregator;

        public string TextToTransmit { get; set; }

        public BindableCollection<Switch> Switches
        {
            get
            {
                return _switches;
            }
            set
            {
                _switches = value;
                NotifyOfPropertyChange(() => Switches);
            }
        }


        public int PlayerScore
        {
            get
            {
                return _playerScore;
            }
            set
            {
                _playerScore = value;
                NotifyOfPropertyChange(() => PlayerScore);
            }
        }


        public void SimpleMessage() 
        {
            _gameController.ServerBusController.SendSimpleMessage(TextToTransmit);
        }

        public void EventMessage()
        {
            _gameController.ServerBusController.SendEventMessage(TextToTransmit);
        }

        public ShellViewModel() 
        {
            _switches = new BindableCollection<Switch>();
            
        }

        public void Exit() 
        { 
            _gameController.ServerBusController.Stop(); 
        }

        public void UpdateUI()   // TODO: Manual update of the UI for initial testing only
        {
            Switches.Clear();
           // _gameController._switches.ForEach(device => Switches.Add(device));
        }

        public void SaveConfig() 
        {
            _gameController.SaveConfigurationToFile();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);



            _gameController = IoC.Get<IGameController>();
            if (_gameController.Configure())
            {
                _gameController.ConnectToHardware();
            }

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            _gameController.DisconnectFromHardware();

            Exit();
            base.OnDeactivate(close);
        }

        public void Handle(UpdateDisplayEvent message)
        {
            PlayerScore += message.PlayerScore;
        }
    }
}