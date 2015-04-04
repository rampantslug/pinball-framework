using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Logging;
using RampantSlug.ServerLibrary.Events;
using RampantSlug.ServerLibrary.Hardware;
using RampantSlug.ServerLibrary.Hardware.Arduino;
using RampantSlug.ServerLibrary.Hardware.Proc;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary
{
    public class GameController : IGameController, IHandle<DeviceMessageResult>, IHandle<RequestConfigResult>, IHandle<DeviceCommandResult>, IHandle<SwitchUpdateEvent>
    {
        // Services used by GameController  
        private IEventAggregator _eventAggregator;
        public IServerBusController ServerBusController { get; private set; }

        public AttrCollection<ushort, string, Switch> Switches
        {
            get { return _switches; }
            set { _switches = value; }
        }


        // Hardware Controllers
        private IProcController _procController;
        private IArduinoController _arduinoController;


        // Devices used by Pinball Hardware
        private AttrCollection<ushort, string, Switch> _switches;
        private AttrCollection<ushort, string, Coil> _coils;
        private DeviceCollection<Motor> _motors;

        public GameController() 
        {
          //  _bootstrapper = new AppBootstrapper();
        }


  




      

        public bool Configure()
        {
            ServerBusController = IoC.Get<IServerBusController>();
            ServerBusController.Start();

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

            _procController = IoC.Get<IProcController>();
            _arduinoController = IoC.Get<IArduinoController>();

            try
            {
                // Retrieve saved configuration information
                var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
                var gameConfiguration = Configuration.FromFile(filePath + @"\Configuration\machine.json");

                // Update local information
                _switches = DeviceCollection<Switch>.CreateCollection(gameConfiguration.Switches, RsLogManager.GetCurrent);
                _coils = DeviceCollection<Coil>.CreateCollection(gameConfiguration.Coils, RsLogManager.GetCurrent);

                return true;
            }
            catch (Exception ex)
            {
                RsLogManager.GetCurrent.LogTestMessage("Error Processing Configuration " + ex.Message);
                return false;
            }
        }

        public void ConnectToHardware()
        {

            //TODO: Need to rename this to a Connect method??
            // And below to a disconnect instead of Close()?
            _procController.Setup();

        }

        public void DisconnectFromHardware()
        {
            if(_procController != null)
                _procController.Close();

            if (_arduinoController != null)
                _arduinoController.Close();
        }



       

        #region Respond to EventAggregator Events

        public void Handle(DeviceMessageResult message)
        {
            var device = message.Device;
            var switchDevice = device as Switch;
            if (switchDevice != null)
            {
                //_switches.Add(switchDevice);
                // TODO: This maybe needs to be done in the Device Message Consumer?
                //_gameConfiguration.Switches.Add(switchDevice);
            }

            RsLogManager.GetCurrent.LogTestMessage("Received device settings message from client");
        }

        public void Handle(DeviceCommandResult message)
        {
            // Set the device into the desired state
           // message.Device


            // If appropriate hardware is connected then also drive the hardware to that state

           // if (_tempArduino == null)
          //  {
           //     _tempArduino = new ArduinoDevice();
          //  }
           // RsLogManager.GetCurrent.LogTestMessage("Received device command request from client: " + message.TempControllerMessage);
           // _tempArduino.SendRequestToArduinoBoard(message.TempControllerMessage);
        }


        public void Handle(RequestConfigResult message)
        {
            var gameConfiguration = new Configuration();
            gameConfiguration.ImageSerialize();
            gameConfiguration.Switches = _switches.Values;
            gameConfiguration.Coils = _coils.Values;
            //gameConfiguration.StepperMotors = _motors.Values;

            ServerBusController.SendConfigurationMessage(gameConfiguration);
        }

        public void Handle(SwitchUpdateEvent message)
        {
            // Update local state of switch...
            var sw = message.UpdatedSwitch;
            if (sw != null)
            {
                _switches.Update(sw.Number, sw);
                RsLogManager.GetCurrent.LogTestMessage("Switch Event for: " + sw.Name);

                // Update score
                // TODO: Clean this up and come up with a better solution
                _eventAggregator.PublishOnUIThread(new UpdateDisplayEvent{PlayerScore = 10});
            }
        }


        #endregion


        public void SaveConfigurationToFile()
        {
            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            // _gameConfiguration.ToFile(filePath);
        }

       
    }
}
