using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.ServerLibrary.Events;
using RampantSlug.ServerLibrary.Hardware;
using RampantSlug.ServerLibrary.Hardware.Proc;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary
{
    public class GameController: IGameController, IHandle<DeviceMessageResult>, IHandle<RequestSettingsResult>
    {
        private AppBootstrapper _bootstrapper;       
        private IEventAggregator _eventAggregator;
        private Configuration _gameConfiguration;
        private IServerBusController _busController;


        private static BackgroundWorker worker;

        private static IHardwareController _procController;


        public List<Switch> _switches;
        public List<Coil> _coils;

        public IServerBusController ServerBusController 
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

            _busController = IoC.Get<IServerBusController>();
            _busController.Start();

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

           // _gameConfiguration = new Configuration();
           
            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

            _gameConfiguration = Configuration.FromFile(filePath + @"\Configuration\machine.json");
            _switches = _gameConfiguration.Switches;
            _coils = _gameConfiguration.Coils;
        }


        public void Handle(DeviceMessageResult message)
        {
            var device = message.Device;
            var switchDevice = device as Switch;
            if(switchDevice != null)
            {
                _switches.Add(switchDevice);
                // TODO: This maybe needs to be done in the Device Message Consumer?
                _gameConfiguration.Switches.Add(switchDevice);
            }

            RsLogManager.GetCurrent.LogTestMessage("Received device settings message from client");
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

        public void ConnectToHardware()
        {
            // Put this onto a different thread...

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += new DoWorkEventHandler(ProcWorkerThread);
            worker.RunWorkerAsync();
        }

        public void CloseHardware()
        {
            _procController.Close();
        }



        private static void ProcWorkerThread(object sender, DoWorkEventArgs e)
        {

            System.Threading.Thread.CurrentThread.Name = "p-roc thread";

            _procController = IoC.Get<IHardwareController>();
            if (_procController.Setup())
            {
                _procController.Start();
            }
        }
    }
}
