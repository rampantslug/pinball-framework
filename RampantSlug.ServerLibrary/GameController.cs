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
using RampantSlug.Common.Commands;
using RampantSlug.Common.Logging;
using RampantSlug.ServerLibrary.Events;
using RampantSlug.ServerLibrary.Hardware;
using RampantSlug.ServerLibrary.Hardware.Arduino;
using RampantSlug.ServerLibrary.Hardware.Proc;
using RampantSlug.ServerLibrary.Logging;
using RampantSlug.ServerLibrary.Modes;
using RampantSlug.ServerLibrary.ServerDisplays;

namespace RampantSlug.ServerLibrary
{
    public class GameController : IGameController,          
        IHandle<RequestConfigEvent>,
        IHandle<RestartServerEvent>,
        IHandle<ConfigureMachineEvent>,
        IHandle<StartupCompleteEvent>,
 
        // Configure devices
        IHandle<ConfigureSwitchEvent>,
        IHandle<ConfigureCoilEvent>,
        IHandle<ConfigureStepperMotorEvent>,
        IHandle<ConfigureServoEvent>,
        IHandle<ConfigureLedEvent>,

        // Client command requests
        IHandle<SwitchCommandResult>,
        IHandle<CoilCommandResult>,
        IHandle<StepperMotorCommandResult>,
        IHandle<ServoCommandResult>,
        IHandle<LedCommandResult>,

        // Device State Changes
        IHandle<UpdateSwitchEvent>,
        IHandle<UpdateCoilEvent>,
        IHandle<UpdateStepperMotorEvent>,
        IHandle<UpdateServoEvent>,
        IHandle<UpdateLedEvent>
    {
        // Services used by GameController  
        private IEventAggregator _eventAggregator;
        public IServerBusController ServerBusController { get; private set; }

        // Hardware Controllers
        private IProcController _procController;
        private IArduinoController _arduinoController;


        // Devices used by Pinball Hardware
        public IDevices Devices { get; set; }

        // Display Elements
        public IDisplay Display { get; set; }

        // GamePlay logic
        public IGamePlay GamePlay { get; set; }

        


        



        public string ServerName { get; set; }

        public string ServerIcon { get; set; }

        public bool UseHardware { get; set; }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public GameController() 
        {
            
        }

        #endregion




        #region Setup / Teardown

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Configure(bool isRestart = false)
        {
            if (!isRestart)
            {
                ServerBusController = IoC.Get<IServerBusController>();
                ServerBusController.Start();
            }
            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

            _procController = IoC.Get<IProcController>();
            _arduinoController = IoC.Get<IArduinoController>();



            // Are we better off moving these to IOC??
            Devices = new Devices();
            Display = new Display();
            GamePlay = new GamePlay(this);

            try
            {
                // Retrieve saved configuration information
                var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
                var gameConfiguration = Configuration.FromFile(filePath + @"\Configuration\machine.json");

                // Update local information from configuration
                Devices.LoadSwitches(gameConfiguration.Switches);
                Devices.LoadCoils(gameConfiguration.Coils);
                Devices.LoadStepperMotors(gameConfiguration.StepperMotors);
                Devices.LoadServos(gameConfiguration.Servos);
                Devices.LoadLeds(gameConfiguration.Leds);

                //_switches = DeviceCollection<Switch>.CreateCollection(gameConfiguration.Switches, RsLogManager.GetCurrent);
                //_coils = DeviceCollection<Coil>.CreateCollection(gameConfiguration.Coils, RsLogManager.GetCurrent);
                //_stepperMotors = DeviceCollection<StepperMotor>.CreateCollection(gameConfiguration.StepperMotors, RsLogManager.GetCurrent);
                //_servos = DeviceCollection<Servo>.CreateCollection(gameConfiguration.Servos, RsLogManager.GetCurrent);
                //_leds = DeviceCollection<Led>.CreateCollection(gameConfiguration.Leds, RsLogManager.GetCurrent);

                ServerName = gameConfiguration.ServerName;
                ServerIcon = gameConfiguration.ServerIcon;
                UseHardware = gameConfiguration.UseHardware;

                return true;
            }
            catch (Exception ex)
            {
                RsLogManager.GetCurrent.LogMessage(LogEventType.Error, OriginatorType.System, "Server", "Configuration", "Error processing configuration: " + ex.Message);
                return false;
            }
        }

        public void ConnectToHardware()
        {
            if (UseHardware)
            {
                _procController.Setup();
                _arduinoController.Setup();
            }
            else
            {
                RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, "Server", "Simulation", "Server not using hardware. Simulation only.");
            }
        }

        public void DisconnectFromHardware()
        {
            if(_procController != null)
                _procController.Close();

            if (_arduinoController != null)
                _arduinoController.Close();
        }

        public void Handle(StartupCompleteEvent message)
        {
            GamePlay.Initialise();
        }

        #endregion

        

        #region Respond to Commands sent from Client to control/fake hardware

        // TODO: Clean this up a bit and implement a SwitchType to check against for NO / NC states
        public void Handle(SwitchCommandResult message)
        {
            var updatedSwitch = message.Device;

            if (message.Command == SwitchCommand.PressActive)
                {
                    updatedSwitch.State = string.Equals(updatedSwitch.State, "Open") ? "Closed" : "Open";
                    _eventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
                    {
                        Device = updatedSwitch,
                        SwitchEvent = new Event()
                        {
                            Time = 5,
                            Type = EventType.SwitchClosedDebounced,
                            Value = updatedSwitch.Number
                        }
                    });

                    updatedSwitch.State = string.Equals(updatedSwitch.State, "Open") ? "Closed" : "Open";

                    TimedAction.ExecuteWithDelay(new System.Action(delegate
                    {
                        _eventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
                        {
                            Device = updatedSwitch,
                            SwitchEvent = new Event()
                            {
                                Time = 5,
                                Type = EventType.SwitchOpenDebounced,
                                Value = updatedSwitch.Number
                            }
                        });
                    }), TimeSpan.FromSeconds(0.5));
                    
                }
                else if (message.Command == SwitchCommand.HoldActive)
                {
                    updatedSwitch.State = string.Equals(updatedSwitch.State, "Open") ? "Closed" : "Open";
                    _eventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
                    {
                        Device = updatedSwitch,
                        SwitchEvent = new Event()
                        {
                            Time = 5,
                            Type = EventType.SwitchClosedDebounced,
                            Value = updatedSwitch.Number
                        }
                    });

                }
        }

        public void Handle(CoilCommandResult message)
        {
            var updatedCoil = message.Device;

            if (message.Command == CoilCommand.PulseActive)
            {
                RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.Coil, message.Device.Name, message.Command.ToString(), "Client command to pulse Coil.");
                
                updatedCoil.State = "Pulsing";

                _eventAggregator.PublishOnUIThread(new UpdateCoilEvent() { Device = updatedCoil });

                updatedCoil.State = "Inactive";

                TimedAction.ExecuteWithDelay(new System.Action(delegate
                {
                    _eventAggregator.PublishOnUIThread(new UpdateCoilEvent()
                    {
                        Device = updatedCoil
                    });
                }), TimeSpan.FromSeconds(0.5));
            }  
        }

        public void Handle(StepperMotorCommandResult message)
        {
            var updatedStepperMotor = message.Device;

            if (message.Command == StepperMotorCommand.ToClockwiseLimit)
            {
                // TODO: Look up name of position for clockwise limit for this device
                updatedStepperMotor.State = "ClockwiseLimit";

                _eventAggregator.PublishOnUIThread(new UpdateStepperMotorEvent() { Device = updatedStepperMotor });
            }
            else if (message.Command == StepperMotorCommand.ToCounterClockwiseLimit)
            {
                updatedStepperMotor.State = "CounterClockwiseLimit";
                _eventAggregator.PublishOnUIThread(new UpdateStepperMotorEvent() { Device = updatedStepperMotor });
            }

            RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.StepperMotor, message.Device.Name, message.Command.ToString(), "Client command to rotate Stepper Motor.");

           

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

        public void Handle(ServoCommandResult message)
        {
            var updatedServo = message.Device;

            if (message.Command == ServoCommand.ToClockwiseLimit)
            {
                // TODO: Look up name of position for clockwise limit for this device
                updatedServo.State = "ClockwiseLimit";

                _eventAggregator.PublishOnUIThread(new UpdateServoEvent() { Device = updatedServo });
            }
            else if (message.Command == ServoCommand.ToCounterClockwiseLimit)
            {
                updatedServo.State = "CounterClockwiseLimit";
                _eventAggregator.PublishOnUIThread(new UpdateServoEvent() { Device = updatedServo });
            }

            RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.Servo, message.Device.Name, message.Command.ToString(), "Client command to rotate Servo.");

        }

        public void Handle(LedCommandResult message)
        {
            var updatedLed = message.Device;

            if (message.Command == LedCommand.MidIntesityOn)
            {
                updatedLed.State = "MidIntesityOn";

                _eventAggregator.PublishOnUIThread(new UpdateLedEvent() { Device = updatedLed });
            }
            else if (message.Command == LedCommand.FullOff)
            {
                updatedLed.State = "Off";
                _eventAggregator.PublishOnUIThread(new UpdateLedEvent() { Device = updatedLed });
            }
            RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.Led, message.Device.Name, message.Command.ToString(), "Client command to change Led.");
        }

        #endregion

        #region Respond to Events from Hardware Devices

        public void Handle(UpdateSwitchEvent message)
        {
            // Update local state of switch...
            var sw = message.Device;
            if (sw != null)
            {
                // TODO: Check for normally closed / open etc...
                if (message.SwitchEvent.Type == EventType.SwitchClosedDebounced)
                {
                    // Update the switch and push change to local cache before sending to client.
                }

                Devices.UpdateSwitch(sw.Number, sw);

                // Update score
                // TODO: Clean this up and come up with a better solution
                //_eventAggregator.PublishOnUIThread(new UpdateDisplayEvent{PlayerScore = 10});

                GamePlay.ProcessSwitchEvent(message.SwitchEvent);

                ServerBusController.SendUpdateDeviceMessage(sw);
            }
        }

        public void Handle(UpdateCoilEvent message)
        {
            // Update local state of coil...
            var coil = message.Device;
            if (coil != null)
            {
                Devices.UpdateCoil(coil.Number, coil);

                // Notify Client if listening
                ServerBusController.SendUpdateDeviceMessage(coil);
            }
        }

        public void Handle(UpdateStepperMotorEvent message)
        {
            // Update local state of stepper motor...
            var stepperMotor = message.Device;
            if (stepperMotor != null)
            {
                Devices.UpdateStepperMotor(stepperMotor.Number, stepperMotor);

                // Notify Client if listening
                ServerBusController.SendUpdateDeviceMessage(stepperMotor);
            }
        }

        public void Handle(UpdateServoEvent message)
        {
            // Update local state of servo...
            var servo = message.Device;
            if (servo != null)
            {
                Devices.UpdateServo(servo.Number, servo);

                // Notify Client if listening
                ServerBusController.SendUpdateDeviceMessage(servo);
            }
        }

        public void Handle(UpdateLedEvent message)
        {
            // Update local state of led...
            var led = message.Device;
            if (led != null)
            {
                Devices.UpdateLed(led.Number, led);

                // Notify Client if listening
                ServerBusController.SendUpdateDeviceMessage(led);
            }
        }


        #endregion



        public void Handle(RestartServerEvent message)
        {
            DisconnectFromHardware();
            _eventAggregator = null;
            _procController = null;
            _arduinoController = null;
         //   BackgroundVideo = null;
         //   MainScore = null;

            if (Configure(true))
            {
                ConnectToHardware();
                _eventAggregator.PublishOnUIThread(new ServerRestartedEvent());
            }
        }

        #region Configuration Related Methods

        public void Handle(RequestConfigEvent message)
        {
            var config = PopulateConfiguration();
            ServerBusController.SendConfigurationMessage(config);
        }

        public void Handle(ConfigureMachineEvent message)
        {
            UseHardware = message.UseHardware;
        }

        public void Handle(ConfigureSwitchEvent message)
        {
            if (UpdateConfig(message.Device, message.RemoveDevice))
            {
                SaveConfigurationToFile();

                // Update Client with changes
                var config = PopulateConfiguration();
                ServerBusController.SendConfigurationMessage(config);
            }          
        }

        public void Handle(ConfigureCoilEvent message)
        {
            if (UpdateConfig(message.Device, message.RemoveDevice))
            {
                SaveConfigurationToFile();

                // Update Client with changes
                var config = PopulateConfiguration();
                ServerBusController.SendConfigurationMessage(config);
            }
        }

        public void Handle(ConfigureStepperMotorEvent message)
        {
            if (UpdateConfig(message.Device, message.RemoveDevice))
            {
                SaveConfigurationToFile();

                // Update Client with changes
                var config = PopulateConfiguration();
                ServerBusController.SendConfigurationMessage(config);
            }
        }

        public void Handle(ConfigureServoEvent message)
        {
            if (UpdateConfig(message.Device, message.RemoveDevice))
            {
                SaveConfigurationToFile();

                // Update Client with changes
                var config = PopulateConfiguration();
                ServerBusController.SendConfigurationMessage(config);
            }
        }

        public void Handle(ConfigureLedEvent message)
        {
            if (UpdateConfig(message.Device, message.RemoveDevice))
            {
                SaveConfigurationToFile();

                // Update Client with changes
                var config = PopulateConfiguration();
                ServerBusController.SendConfigurationMessage(config);
            }
        }

      

        private bool UpdateConfig(Switch updatedSwitch, bool removeDevice)
        {
            // Adding a new switch
            if (updatedSwitch.Number == 0)
            {
                if (Devices.AddSwitch(updatedSwitch))
                {
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Add to config", "Added switch to config.");
                    return true;
                }
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedSwitch.Name, "Add to config", "Invalid switch settings. Not saving to config.");
                return false;
            }

            // Update or remove existing Switch
            if (Devices.Switches.ContainsKey(updatedSwitch.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveSwitch(updatedSwitch.Number);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Remove from config", "Removed switch from config.");
                }
                else
                {
                    Devices.UpdateSwitch(updatedSwitch.Number, updatedSwitch);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Update config", "Updated switch in config.");
                }
                return true;
            }
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedSwitch.Name, "Configuration", "Invalid switch settings. Not saving to config.");
                return false;
        }

        private bool UpdateConfig(Coil updatedCoil, bool removeDevice)
        {
            // Adding a new coil
            if (updatedCoil.Number == 0)
            {
                if (Devices.AddCoil(updatedCoil))
                {
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Add to config", "Added coil to config.");
                    return true;
                }
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedCoil.Name, "Add to config", "Invalid coil settings. Not saving to config.");
                return false;
            }

            // Update or remove existing coil
            if (Devices.Coils.ContainsKey(updatedCoil.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveCoil(updatedCoil.Number);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Remove from config", "Removed coil from config.");
                }
                else
                {
                    Devices.UpdateCoil(updatedCoil.Number, updatedCoil);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Update config", "Updated coil in config.");
                }
                return true;
            }
            else 
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedCoil.Name, "Configuration", "Invalid coil settings. Not saving to config.");
                return false;
            }
        }

        private bool UpdateConfig(StepperMotor updatedStepperMotor, bool removeDevice)
        {
            // Adding a new stepperMotor
            if (updatedStepperMotor.Number == 0)
            {
                if (Devices.AddStepperMotor(updatedStepperMotor))
                {
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Add to config", "Added stepper motor to config.");
                    return true;
                }
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedStepperMotor.Name, "Add to config", "Invalid stepper motor settings. Not saving to config.");
                return false;
            }

            // Update or remove existing stepperMotor
            if (Devices.StepperMotors.ContainsKey(updatedStepperMotor.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveStepperMotor(updatedStepperMotor.Number);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Remove from config", "Removed stepper motor from config.");
                }
                else
                {
                    Devices.UpdateStepperMotor(updatedStepperMotor.Number, updatedStepperMotor);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Update config", "Updated stepper motor in config.");
                }
                return true;
            }
            else 
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedStepperMotor.Name, "Configuration", "Invalid stepper motor settings. Not saving to config.");
                return false;
            }
        }

        private bool UpdateConfig(Servo updatedServo, bool removeDevice)
        {
            // Adding a new servo
            if (updatedServo.Number == 0)
            {
                if (Devices.AddServo(updatedServo))
                {
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Add to config", "Added servo to config.");
                    return true;
                }
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedServo.Name, "Add to config", "Invalid servo settings. Not saving to config.");
                return false;
            }

            // Update or remove existing servo
            if (Devices.Servos.ContainsKey(updatedServo.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveServo(updatedServo.Number);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Remove from config", "Removed servo from config.");
                }
                else
                {
                    Devices.UpdateServo(updatedServo.Number, updatedServo);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Update config", "Updated servo in config.");
                }
                return true;
            }
            else 
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedServo.Name, "Configuration", "Invalid servo settings. Not saving to config.");
                return false;
            }
        }

        private bool UpdateConfig(Led updatedLed, bool removeDevice)
        {
            // Adding a new led
            if (updatedLed.Number == 0)
            {
                if (Devices.AddLed(updatedLed))
                {
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Add to config", "Added led to config.");
                    return true;
                }
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedLed.Name, "Add to config", "Invalid led settings. Not saving to config.");
                return false;
            }

            // Update or remove existing led
            if (Devices.Leds.ContainsKey(updatedLed.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveLed(updatedLed.Number);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Remove from config", "Removed led from config.");
                }
                else
                {
                    Devices.UpdateLed(updatedLed.Number, updatedLed);
                    RsLogManager.GetCurrent.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Update config", "Updated led in config.");
                }
                return true;
            }
            else 
            {                
                // Something has gone wrong. Should have generated a Number based on Address
                RsLogManager.GetCurrent.LogMessage(LogEventType.Warn, OriginatorType.System, updatedLed.Name, "Configuration", "Invalid led settings. Not saving to config.");
                return false;
            }
        }


        public void SaveConfigurationToFile()
        {
            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            var config = PopulateConfiguration();
            config.ToFile(filePath + @"\Configuration\machine.json");
        }


        private Configuration PopulateConfiguration()
        {
            var gameConfiguration = new Configuration();
            gameConfiguration.ImageSerialize();

            gameConfiguration.ServerName = ServerName;
            gameConfiguration.ServerIcon = ServerIcon;
            gameConfiguration.UseHardware = UseHardware;

            gameConfiguration.Switches = Devices.AllSwitches();
            gameConfiguration.Coils = Devices.AllCoils();
            gameConfiguration.StepperMotors = Devices.AllStepperMotors();
            gameConfiguration.Servos = Devices.AllServos();
            gameConfiguration.Leds = Devices.AllLeds();

            return gameConfiguration;
        }

        #endregion


      

    }
}
