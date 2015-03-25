using System;
using System.ComponentModel;
using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
using RampantSlug.ServerLibrary.Hardware;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.PinballServerDemo
{
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell 
    {
        private IGameController _gameController;
        private BindableCollection<Switch> _switches; 

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
            _gameController.ConnectToHardware();
        }

        protected override void OnDeactivate(bool close)
        {
            _gameController.CloseHardware();
            Exit();
            base.OnDeactivate(close);
        }
    }
}