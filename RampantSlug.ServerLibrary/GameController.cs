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
using RampantSlug.ServerLibrary.ServerDisplays;

namespace RampantSlug.ServerLibrary
{
    public class GameController : IGameController, IHandle<DeviceConfigMessageResult>, IHandle<RequestConfigResult>, IHandle<DeviceCommandResult>, IHandle<SwitchUpdateEvent>
    {
        // Services used by GameController  
        private IEventAggregator _eventAggregator;
        public IServerBusController ServerBusController { get; private set; }

        


        // Hardware Controllers
        private IProcController _procController;
        private IArduinoController _arduinoController;


        // Devices used by Pinball Hardware
        private AttrCollection<ushort, string, Switch> _switches;
        private AttrCollection<ushort, string, Coil> _coils;
        private AttrCollection<ushort, string, StepperMotor> _stepperMotors;


        // Display Elements
        public IDisplayBackgroundVideo BackgroundVideo { get; private set; }

        public IDisplayMainScore MainScore { get; private set; }


        public AttrCollection<ushort, string, Switch> Switches
        {
            get { return _switches; }
            set { _switches = value; }
        }

        public AttrCollection<ushort, string, Coil> Coils
        {
            get { return _coils; }
            set { _coils = value; }
        }

        public AttrCollection<ushort, string, StepperMotor> StepperMotors
        {
            get { return _stepperMotors; }
            set { _stepperMotors = value; }
        }


        public GameController() 
        {

        }


  




      

        public bool Configure()
        {
            ServerBusController = IoC.Get<IServerBusController>();
            ServerBusController.Start();

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

            _procController = IoC.Get<IProcController>();
            _arduinoController = IoC.Get<IArduinoController>();

            BackgroundVideo = IoC.Get<IDisplayBackgroundVideo>();
            MainScore = IoC.Get<IDisplayMainScore>();

            try
            {
                // Retrieve saved configuration information
                var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
                var gameConfiguration = Configuration.FromFile(filePath + @"\Configuration\machine.json");

                // Update local information
                _switches = DeviceCollection<Switch>.CreateCollection(gameConfiguration.Switches, RsLogManager.GetCurrent);
                _coils = DeviceCollection<Coil>.CreateCollection(gameConfiguration.Coils, RsLogManager.GetCurrent);
                _stepperMotors = DeviceCollection<StepperMotor>.CreateCollection(gameConfiguration.StepperMotors, RsLogManager.GetCurrent);

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
            _procController.Setup();
            _arduinoController.Setup();

        }

        public void DisconnectFromHardware()
        {
            if(_procController != null)
                _procController.Close();

            if (_arduinoController != null)
                _arduinoController.Close();
        }



       

        #region Respond to EventAggregator Events

        public void Handle(DeviceConfigMessageResult message)
        {
            var device = message.Device;

            var updatedSwitch = device as Switch;
            if (updatedSwitch != null)
            {
               UpdateConfig(updatedSwitch);
               return;
            }

                var updatedCoil = device as Coil;
                if (updatedCoil != null)
                {
                    UpdateConfig(updatedCoil);
                    return;
                }

                var updatedStepperMotor = device as StepperMotor;
                if (updatedStepperMotor != null)
                {
                    UpdateConfig(updatedStepperMotor);
                    return;
                }
            

            
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

            //MainScore.PlayerScore += 10;

            var device = message.Device;

            var commandSwitch = device as Switch;
            if (commandSwitch != null)
            {
                if (string.Equals(message.TempControllerMessage, "ToggleOpenClosed"))
                {
                    if (string.Equals(commandSwitch.State, "Open"))
                    {
                        commandSwitch.State = "Closed";
                    }
                    else
                    {
                        commandSwitch.State = "Open";
                    }
                    _eventAggregator.PublishOnUIThread(new SwitchUpdateEvent(){UpdatedSwitch = commandSwitch});
                }
                
            }
        }


        public void Handle(RequestConfigResult message)
        {
            var gameConfiguration = new Configuration();
            gameConfiguration.ImageSerialize();
            gameConfiguration.Switches = _switches.Values;
            gameConfiguration.Coils = _coils.Values;
            gameConfiguration.StepperMotors = _stepperMotors.Values;

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
                //_eventAggregator.PublishOnUIThread(new UpdateDisplayEvent{PlayerScore = 10});
                MainScore.PlayerScore += 10;

                ServerBusController.SendDeviceMessage(sw);
            }
        }


        #endregion



        #region Configuration Related Methods

        private void UpdateConfig(Switch updatedSwitch)
        {
            if (updatedSwitch.Number == 0)
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogTestMessage("Invalid switch settings. Not saving to config.");
                return;
            }

            Switch sw = null;
            Switches.TryGetValue(updatedSwitch.Number, out sw);

            // Update existing Switch
            if (sw != null)
            {
                Switches.Update(sw.Number, sw);
                RsLogManager.GetCurrent.LogTestMessage("Updated switch " + sw.Name + "in config.");
            }
            else // Adding a new switch
            {
                Switches.Add(updatedSwitch.Number, updatedSwitch.Name, updatedSwitch);
                RsLogManager.GetCurrent.LogTestMessage("Added switch " + updatedSwitch.Name + "to config.");
            }
        }

        private void UpdateConfig(Coil updatedCoil)
        {
            if (updatedCoil.Number == 0)
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogTestMessage("Invalid coil settings. Not saving to config.");
                return;
            }

            Coil coil = null;
            Coils.TryGetValue(updatedCoil.Number, out coil);

            // Update existing coil
            if (coil != null)
            {
                Coils.Update(coil.Number, coil);
                RsLogManager.GetCurrent.LogTestMessage("Updated coil " + coil.Name + "in config.");
            }
            else // Adding a new coil
            {
                Coils.Add(updatedCoil.Number, updatedCoil.Name, updatedCoil);
                RsLogManager.GetCurrent.LogTestMessage("Added coil " + updatedCoil.Name + "to config.");
            }
        }

        private void UpdateConfig(StepperMotor updatedStepperMotor)
        {
            if (updatedStepperMotor.Number == 0)
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogTestMessage("Invalid stepper motor settings. Not saving to config.");
                return;
            }

            StepperMotor stepperMotor = null;
            StepperMotors.TryGetValue(updatedStepperMotor.Number, out stepperMotor);

            // Update existing stepperMotor
            if (stepperMotor != null)
            {
                StepperMotors.Update(stepperMotor.Number, stepperMotor);
                RsLogManager.GetCurrent.LogTestMessage("Updated stepper motor " + stepperMotor.Name + "in config.");
            }
            else // Adding a new stepperMotor
            {
                StepperMotors.Add(updatedStepperMotor.Number, updatedStepperMotor.Name, updatedStepperMotor);
                RsLogManager.GetCurrent.LogTestMessage("Added stepper motor " + updatedStepperMotor.Name + "to config.");
            }
        }


        public void SaveConfigurationToFile()
        {
            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            // _gameConfiguration.ToFile(filePath);
        }

        #endregion

        

       
    }
}
