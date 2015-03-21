using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public class GameController: IHandle<DeviceMessageResult>, IHandle<RequestSettingsResult>
    {
        private AppBootstrapper _bootstrapper;
        public List<Switch> _switches;
        private IEventAggregator _eventAggregator;
        private Configuration _gameConfiguration;

        private IBusController _busController;

        public IBusController ServerBusController 
        {
            get
            {
                return _busController;
            }
        }

        public GameController() 
        {
            _switches = new List<Switch>();
            _bootstrapper = new AppBootstrapper();

            _busController = IoC.Get<IBusController>();
            _busController.Start();

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

           // _gameConfiguration = new Configuration();
           
            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

            _gameConfiguration = Configuration.FromFile(filePath + @"\Configuration\machine.json");
            _switches = _gameConfiguration.Switches;
        }


        public void Handle(DeviceMessageResult message)
        {
            _switches.Add(message.Device);
            // TODO: This maybe needs to be done in the Device Message Consumer?
            _gameConfiguration.Switches.Add(message.Device);
        }

        public void Handle(RequestSettingsResult message)
        {
            _busController.SendSettingsMessage(_switches);
        }

        public void SaveConfigurationToFile()
        {
           var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

           _gameConfiguration.ToFile(filePath);
        }
    }
}
