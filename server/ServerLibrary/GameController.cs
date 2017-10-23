using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Commands;
using Configuration;
using Hardware.Arduino;
using Hardware.Proc;
using Logging;
using ServerLibrary.Events;


namespace ServerLibrary
{
    [Export(typeof(IGameController))]
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
        public IEventAggregator EventAggregator { get; private set; }
        public IServerBusController ServerBusController { get; private set; }
        public IRsLogger RsLogger { get; private set; }

        // Hardware Controllers
        private IProcController _procController;
        private IArduinoController _arduinoController;
        private RsConfiguration _configuration;


        // Devices used by Pinball Hardware
        public IDevices Devices { get; private set; }

        // Display Elements
        public IDisplay Display { get; set; }

        // GamePlay logic
        public IGamePlay GamePlay { get; set; }


        // TODO: Replace these temp constructs to something a bit nicer...
        public List<string> Images { get; set; }
        public List<string> Videos { get; set; } 
        public List<string> Sounds { get; set; }
 
        public string ServerName { get; set; }

        public string ServerIcon { get; set; }

        public bool UseHardware { get; set; }

        public MachineState MachineState { get; set; }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public GameController(
            IServerBusController serverBusController, 
            IEventAggregator eventAggregator, 
            IRsLogger rsLogger,
            IDevices devices,
           // IGamePlay gamePlay,
           // IDisplay display,
            IProcController procController,
            IArduinoController arduinoController   
            )
        {
            ServerBusController = serverBusController;
            ServerBusController.Start();

            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);

            RsLogger = rsLogger;

            Devices = devices;
          //  GamePlay = gamePlay;
          //  Display = display;

            _procController = procController;
            _arduinoController = arduinoController;

            MachineState = new MachineState(devices);
    }

        #endregion




        #region Setup / Teardown

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Configure()
        {
           // _procController = IoC.Get<IProcController>();
           // _arduinoController = IoC.Get<IArduinoController>();

            // These are created here rather than constructor to facilitate a restart scenario
         //   Devices = new Devices();
            Display = new Display();
            GamePlay = new GamePlay(this);

            Images = new List<string>();
            Videos = new List<string>();
            Sounds = new List<string>();

            try
            {
                // Retrieve saved configuration information
                var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
                _configuration = new RsConfiguration(filePath + @"\Configuration\machine.json");
                var gameConfiguration = _configuration.MachineConfiguration;

                // Update local information from configuration
                Devices.LoadSwitches(gameConfiguration.Switches);
                Devices.LoadCoils(gameConfiguration.Coils);
                Devices.LoadStepperMotors(gameConfiguration.StepperMotors);
                Devices.LoadServos(gameConfiguration.Servos);
                Devices.LoadLeds(gameConfiguration.Leds);

                // Initialise modes from config
                foreach (var mode in GamePlay.AllModes)
                {
                    var modeConfig =
                        gameConfiguration.Modes.FirstOrDefault(modeC => string.Equals(modeC.Title, mode.Title));

                    if (modeConfig != null)
                    {
                        // Initialise mode events
                        if (modeConfig.ModeEvents != null)
                        {
                            foreach (var modeEvent in mode.ModeEvents)
                            {
                                var modeEventConfig =
                                    modeConfig.ModeEvents.FirstOrDefault(
                                        modeC => string.Equals(modeC.Name, modeEvent.Name));
                                if (modeEventConfig != null && !string.IsNullOrEmpty(modeEventConfig.AssociatedSwitchName))
                                {
                                    // TODO: Handle the situation where the device is not found matching the string
                                    modeEvent.AssociatedSwitch = Devices.Switches[modeEventConfig.AssociatedSwitchName];
                                }
                            }
                        }

                        // Initialise required devices
                        if (modeConfig.RequiredDevices != null)
                        {
                            foreach (var requiredDevice in mode.RequiredDevices)
                            {
                                var requiredDeviceConfig =
                                    modeConfig.RequiredDevices.FirstOrDefault(
                                        reqDconfig => string.Equals(reqDconfig.Name, requiredDevice.Name));
                                if (requiredDeviceConfig != null)
                                {
                                    requiredDevice.DeviceName = requiredDeviceConfig.DeviceName;
                                }
                            }
                        }

                        // Initialise required media
                        if (modeConfig.RequiredMedia != null)
                        {
                            foreach (var requiredMedia in mode.RequiredMedia)
                            {
                                var requiredMediaConfig =
                                    modeConfig.RequiredMedia.FirstOrDefault(
                                        reqMconfig => string.Equals(reqMconfig.Name, requiredMedia.Name));
                                if (requiredMediaConfig != null)
                                {
                                    requiredMedia.Filename = requiredMediaConfig.Filename;
                                }
                            }
                        }
                    }
                }


                ServerName = gameConfiguration.ServerName;
                ServerIcon = gameConfiguration.ServerIcon;
                UseHardware = gameConfiguration.UseHardware;

                // Determine ALL the media files on the server
                var imagesFullPath = Directory.GetFiles(filePath + @"\MediaResources\Images\").ToList();
                var imagePathLength = (filePath + @"\MediaResources\Images\").Length;
                foreach (var imageFullPath in imagesFullPath)
                {
                    var image = imageFullPath.Remove(0, imagePathLength);
                    Images.Add(image);
                }

                var videosFullPath = Directory.GetFiles(filePath + @"\MediaResources\Videos\").ToList();
                var videosPathLength = (filePath + @"\MediaResources\Videos\").Length;
                foreach (var videoFullPath in videosFullPath)
                {
                    var video = videoFullPath.Remove(0, videosPathLength);
                    Videos.Add(video);
                }

                var soundsFullPath = Directory.GetFiles(filePath + @"\MediaResources\Sounds\").ToList();
                var soundPathLength = (filePath + @"\MediaResources\Sounds\").Length;
                foreach (var soundFullPath in soundsFullPath)
                {
                    var sound = soundFullPath.Remove(0, soundPathLength);
                    Sounds.Add(sound);
                }

                return true;
            }
            catch (Exception ex)
            {
                RsLogger.LogMessage(LogEventType.Error, OriginatorType.System, "Server", "Configuration", "Error processing configuration: " + ex.Message);
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
                RsLogger.LogMessage(LogEventType.Info, OriginatorType.System, "Server", "Simulation", "Server not using hardware. Simulation only.");
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
                    EventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
                    {
                        Device = updatedSwitch,
                        SwitchEvent = new Event()
                        {
                            Time = 5,
                            Type = EventType.SwitchClosedDebounced,
                            Value = updatedSwitch.Number
                        }
                    });

                    TimedAction.ExecuteWithDelay(new System.Action(delegate
                    {
                        EventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
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
                    EventAggregator.PublishOnUIThread(new UpdateSwitchEvent()
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
                RsLog.LogMessage(LogEventType.Info, OriginatorType.Coil, message.Device.Name, message.Command.ToString(), "Client command to pulse Coil.");
                
                // TODO: Need to fix this up here...
                //updatedCoil.State = "Pulsing";

                EventAggregator.PublishOnUIThread(new UpdateCoilEvent() { Device = updatedCoil });

                //updatedCoil.State = "Inactive";

                TimedAction.ExecuteWithDelay(new System.Action(delegate
                {
                    EventAggregator.PublishOnUIThread(new UpdateCoilEvent()
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
                // TODO: Need to fix this up here...
                //updatedStepperMotor.State = "ClockwiseLimit";

                EventAggregator.PublishOnUIThread(new UpdateStepperMotorEvent() { Device = updatedStepperMotor });
            }
            else if (message.Command == StepperMotorCommand.ToCounterClockwiseLimit)
            {
                //updatedStepperMotor.State = "CounterClockwiseLimit";
                EventAggregator.PublishOnUIThread(new UpdateStepperMotorEvent() { Device = updatedStepperMotor });
            }

            RsLogger.LogMessage(LogEventType.Info, OriginatorType.StepperMotor, message.Device.Name, message.Command.ToString(), "Client command to rotate Stepper Motor.");

           

            // Set the device into the desired state
            // message.Device


            // If appropriate hardware is connected then also drive the hardware to that state

            // if (_tempArduino == null)
            //  {
            //     _tempArduino = new ArduinoDevice();
            //  }
            // RsLogger.GetCurrent.LogTestMessage("Received device command request from client: " + message.TempControllerMessage);
            // _tempArduino.SendRequestToArduinoBoard(message.TempControllerMessage);

        }

        public void Handle(ServoCommandResult message)
        {
            var updatedServo = message.Device;

            if (message.Command == ServoCommand.ToClockwiseLimit)
            {
                // TODO: Look up name of position for clockwise limit for this device
                // TODO: Need to fix this up here...
                //updatedServo.State = "ClockwiseLimit";

                EventAggregator.PublishOnUIThread(new UpdateServoEvent() { Device = updatedServo });
            }
            else if (message.Command == ServoCommand.ToCounterClockwiseLimit)
            {
                //updatedServo.State = "CounterClockwiseLimit";
                EventAggregator.PublishOnUIThread(new UpdateServoEvent() { Device = updatedServo });
            }

            RsLogger.LogMessage(LogEventType.Info, OriginatorType.Servo, message.Device.Name, message.Command.ToString(), "Client command to rotate Servo.");

        }

        public void Handle(LedCommandResult message)
        {
            var updatedLed = message.Device;

            if (message.Command == LedCommand.MidIntesityOn)
            {
                // TODO: Need to fix this up here...
                //updatedLed.State = "MidIntesityOn";

                EventAggregator.PublishOnUIThread(new UpdateLedEvent() { Device = updatedLed });
            }
            else if (message.Command == LedCommand.FullOff)
            {
                //updatedLed.State = "Off";
                EventAggregator.PublishOnUIThread(new UpdateLedEvent() { Device = updatedLed });
            }
            RsLogger.LogMessage(LogEventType.Info, OriginatorType.Led, message.Device.Name, message.Command.ToString(), "Client command to change Led.");
        }

        #endregion

        #region Respond to Events from Hardware Devices

        public void Handle(UpdateSwitchEvent message)
        {
            // Update local state of switch...
            var sw = message.Device;
            if (sw != null)
            {
                // Only handle the Debounced contditions
                if (message.SwitchEvent.Type == EventType.SwitchClosedDebounced)
                {
                    // TODO: check what this means for NO/NC switches??!
                    sw.SetState(true);
                }
                else if (message.SwitchEvent.Type == EventType.SwitchOpenDebounced)
                {
                    sw.SetState(false);
                }
                else
                {
                    return;
                }

                Devices.UpdateSwitch(sw.Number, sw);

                GamePlay.ProcessSwitchEvent(message);

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
            _procController = null;
            _arduinoController = null;
            Display = null;
            Devices = null;
            GamePlay = null;


         //   BackgroundVideo = null;
         //   MainScore = null;

            if (Configure())
            {
                ConnectToHardware();
                EventAggregator.PublishOnUIThread(new ServerRestartedEvent());
            }
        }

        #region Configuration Related Methods

        public void Handle(RequestConfigEvent message)
        {
            ServerBusController.SendConfigurationMessage(_configuration.MachineConfiguration);
        }

        public void Handle(ConfigureMachineEvent message)
        {
            UseHardware = message.UseHardware;
        }

        public void Handle(ConfigureSwitchEvent message)
        {
            if (null != _configuration)
            {
                // Update local state
                MachineState.UpdateDevice(message.Device, message.RemoveDevice);

                // Write to config
                _configuration.MachineConfiguration.Switches = MachineState.Devices.AllSwitches();
                _configuration.WriteMachineToFile();
            }       
        }

        public void Handle(ConfigureCoilEvent message)
        {
            if (null != _configuration)
            {
                // Update local state
                MachineState.UpdateDevice(message.Device, message.RemoveDevice);

                // Write to config
                _configuration.MachineConfiguration.Coils = MachineState.Devices.AllCoils();
                _configuration.WriteMachineToFile();
            }
        }

        public void Handle(ConfigureStepperMotorEvent message)
        {
            if (null != _configuration)
            {
                // Update local state
                MachineState.UpdateDevice(message.Device, message.RemoveDevice);

                // Write to config
                _configuration.MachineConfiguration.StepperMotors = MachineState.Devices.AllStepperMotors();
                _configuration.WriteMachineToFile();
            }
        }

        public void Handle(ConfigureServoEvent message)
        {
            if (null != _configuration)
            {
                // Update local state
                MachineState.UpdateDevice(message.Device, message.RemoveDevice);

                // Write to config
                _configuration.MachineConfiguration.Servos = MachineState.Devices.AllServos();
                _configuration.WriteMachineToFile();
            }
        }

        public void Handle(ConfigureLedEvent message)
        {
            if (null != _configuration)
            {
                // Update local state
                MachineState.UpdateDevice(message.Device, message.RemoveDevice);

                // Write to config
                _configuration.MachineConfiguration.Leds = MachineState.Devices.AllLeds();
                _configuration.WriteMachineToFile();
            }
        }


        /*
         * 
         * ALL BELOW SHOULD BE IN COMMON CONFIGURATION
         * SHOULD BE REALLY EASY TO GO FROM CONFIG TO MACHINE STATE AND BACK AGAIN
         * 
         * 
         * */



//
//        private IMachineConfiguration PopulateConfiguration()
//        {
//            var gameConfiguration = new MachineConfiguration();
//            gameConfiguration.ImageSerialize();
//
//            // Need to convert the modes to something suitable for configuration saving
//            var modes = new List<ModeConfig>();
//            foreach (var mode in GamePlay.AllModes)
//            {
//                var modeConfig = new ModeConfig {Title = mode.Title};
//                var modeEventsConfig = mode.ModeEvents.Select(modeEvent =>
//                {
//                    Switch sw = modeEvent.AssociatedSwitch;
//                    return new ModeEventConfig
//                           {
//                                    Name = modeEvent.Name, 
//                                    AssociatedSwitchName = sw != null ? sw.Name : string.Empty
//                           };
//                }).ToList();
//                var requiredDevicesConfig = mode.RequiredDevices.Select(requiredDevice => new RequiredDeviceConfig
//                {
//                    Name = requiredDevice.Name,
//                    RequiredDeviceName = requiredDevice.DeviceName,
//                    TypeOfDevice = requiredDevice.TypeOfDevice.Name
//
//                }).ToList();
//                
//                modeConfig.ModeEvents = modeEventsConfig;
//                modeConfig.RequiredDevices = requiredDevicesConfig;
//                modes.Add(modeConfig);
//            }
//
//            gameConfiguration.Modes = modes;
//
//            gameConfiguration.ServerName = ServerName;
//            gameConfiguration.ServerIcon = ServerIcon;
//            gameConfiguration.UseHardware = UseHardware;
//
//            gameConfiguration.Switches = Devices.AllSwitches();
//            gameConfiguration.Coils = Devices.AllCoils();
//            gameConfiguration.StepperMotors = Devices.AllStepperMotors();
//            gameConfiguration.Servos = Devices.AllServos();
//            gameConfiguration.Leds = Devices.AllLeds();
//
//            var filePath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
//            gameConfiguration.MediaBaseFileLocation = filePath + @"\MediaResources";
//            gameConfiguration.Images = Images;
//            gameConfiguration.Videos = Videos;
//            gameConfiguration.Sounds = Sounds;
//
//            return gameConfiguration;
//        }

        #endregion


      

    }
}
