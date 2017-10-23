using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;
using ServerLibrary;
using ServerLibrary.Events;
using ServerLibrary.ServerDisplays;


namespace PinballServerDemo
{
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell, IHandle<UpdateDisplayEvent>, IHandle<ServerRestartedEvent>
    {
        private IGameController _gameController;
        private IEventAggregator _eventAggregator;
     
        public IDisplayBackgroundVideo BackgroundVideo { get; private set; }
        public IDisplayMainScore MainScore { get; private set; }
        public IDisplayOverrideDisplay OverrideDisplay { get; private set; }

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, IGameController gameController)
        {
            _eventAggregator = eventAggregator;
            _gameController = gameController;
        }

        public void Exit() 
        { 
            _gameController.ServerBusController.Stop(); 
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            //_gameController = IoC.Get<IGameController>();
            if (_gameController.Configure())
            {
                BackgroundVideo = _gameController.Display.BackgroundVideo;
                MainScore = _gameController.Display.MainScore;
                OverrideDisplay = _gameController.Display.OverrideDisplay;

                NotifyOfPropertyChange(()=>BackgroundVideo);
                NotifyOfPropertyChange(() => MainScore);
                NotifyOfPropertyChange(() => OverrideDisplay);

                _gameController.ConnectToHardware();
            }

            //_eventAggregator = IoC.Get<IEventAggregator>();
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
          //  PlayerScore += message.PlayerScore;
        }

        public void Handle(ServerRestartedEvent message)
        {
            BackgroundVideo = _gameController.Display.BackgroundVideo;
            MainScore = _gameController.Display.MainScore;
            OverrideDisplay = _gameController.Display.OverrideDisplay;

            NotifyOfPropertyChange(()=>BackgroundVideo);
            NotifyOfPropertyChange(() => MainScore);
            NotifyOfPropertyChange(() => OverrideDisplay);
        }
    }
}