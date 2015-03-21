using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
namespace RampantSlug.PinballServerDemo
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {
        //private IBusController _busController;
        private GameController _gameController;
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
            //_busController = IoC.Get<IBusController>();
            //_busController.Start();

            _switches = new BindableCollection<Switch>();
            _gameController = new GameController(); // TODO: Use DI for this?
        }

        public void Exit() 
        { 
            _gameController.ServerBusController.Stop(); 
        }

        public void UpdateUI()   // TODO: Manual update of the UI for initial testing only
        {
            Switches.Clear();
            _gameController._switches.ForEach(device => Switches.Add(device));
        }

        public void SaveConfig() 
        {
            _gameController.SaveConfigurationToFile();
        }
    }
}