using System.Collections.Generic;
using System.Configuration;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Configuration;
using NSubstitute;
using PinballClient;
using PinballClient.ClientComms;
using PinballClient.ClientDisplays.DeviceConfig;
using PinballClient.ClientDisplays.DeviceControl;
using PinballClient.ClientDisplays.DeviceTree;
using PinballClient.ClientDisplays.GameStatus;
using PinballClient.ClientDisplays.LedShowEditor;
using PinballClient.ClientDisplays.LedShowTimeline;
using PinballClient.ClientDisplays.LogMessages;
using PinballClient.ClientDisplays.MediaTree;
using PinballClient.ClientDisplays.ModeTree;
using PinballClient.ClientDisplays.Playfield;
using PinballClient.ClientDisplays.PlayfieldProperties;
using PinballClient.ClientDisplays.ShowsList;
using PinballClient.ClientDisplays.SwitchMatrix;
using Xunit;

namespace PinballClientTests
{
    [Trait("Scope", "Client")]
    public class ShellViewModelTests
    {
        private IGameState _gameState;

        private ShellViewModel GetNewShell()
        {
            var eventAggregator = Substitute.For<IEventAggregator>();
            var clientCommsController = Substitute.For<IClientCommsController>();
            var clientToLocalCommsController = Substitute.For<IClientToLocalCommsController>();
            var clientToServerCommsController = Substitute.For<IClientToServerCommsController>();
            _gameState = new GameState();

            var deviceConfig = Substitute.For<IDeviceConfig>();
            var deviceControl = Substitute.For<IDeviceControl>();
            var deviceTree = Substitute.For<IDeviceTree>();
            var gameStatus = Substitute.For<IGameStatus>();
            var ledShowEditor = Substitute.For<ILedShowEditor>();
            var ledShowTimeline = Substitute.For<ILedShowTimeline>();
            var logMessages = Substitute.For<ILogMessages>();
            var mediaTree = Substitute.For<IMediaTree>();
            var modeTree = Substitute.For<IModeTree>();
            var playfield = Substitute.For<IPlayfield>();
            var playfieldProperties = Substitute.For<IPlayfieldProperties>();
            var showsList = Substitute.For<IShowsList>();
            var switchMatrix = Substitute.For<ISwitchMatrix>();
            
            return new ShellViewModel(
                eventAggregator, 
                clientCommsController,
                clientToLocalCommsController,
                clientToServerCommsController,
                _gameState, 

                deviceConfig,
                deviceControl,
                deviceTree,
                gameStatus,
                ledShowEditor,
                ledShowTimeline,
                logMessages,
                mediaTree,
                modeTree,
                playfield,
                playfieldProperties,
                showsList,
                switchMatrix);
        }


        [Fact]
        public void SaveAsDefault_ExportsCorrectSetting_ToAppConfig()
        {
            var shell = GetNewShell();
            const string testIp = "123.0.0.1";

            shell.ServerIpAddress = testIp;
            shell.SaveAsDefault();

            var serverIpAddress = ConfigurationManager.AppSettings.Get("DefaultServer");

            Assert.Equal(testIp, serverIpAddress);
        }

        [Fact]
        public void UpdateDeviceViewModels_AddsCorrectNumberVMs_ToGameState()
        {
            var shell = GetNewShell();

            shell.UpdateDeviceViewModels(GetSwitches(), GetCoils(), GetStepperMotors(), GetServos(), GetLeds());

            var switchCount = _gameState.Switches.Count;
            var coilCount = _gameState.Coils.Count;
            var servoCount = _gameState.Servos.Count;
            var stepperMotorCount = _gameState.StepperMotors.Count;
            var ledCount = _gameState.Leds.Count;

            Assert.Equal(2, switchCount);
            Assert.Equal(2, coilCount);
            Assert.Equal(2, servoCount);
            Assert.Equal(2, stepperMotorCount);
            Assert.Equal(2, ledCount);
        }

        [Fact]
        public void UpdateCommonViewModels_InitialisesAllGameState_FromConfig()
        {
            var shell = GetNewShell();
            var soundsList = new List<string>() { TestString1, TestString2 };
            var videosList = new List<string>() { TestString2, TestString3 };
            var imagesList = new List<string>() { TestString3, TestString4 };

            var config = new MachineConfiguration
            {
                PlayfieldImage = TestString1,
                ServerName = TestString2,
                ServerIcon = TestString3,
                MediaBaseFileLocation = TestString4,
                UseHardware = false,

                Sounds = soundsList,
                Videos = videosList,
                Images = imagesList,
                //Modes = Substitute.For<List<ModeConfig>>(),

                Switches = GetSwitches(),
                Coils = GetCoils(),
                //DCMotors = GetDCMotors(),
                StepperMotors = GetStepperMotors(),
                Servos = GetServos(),
                Leds = GetLeds(),

            };

            shell.UpdateCommonViewModels(config);
            

            Assert.Equal(TestString1, _gameState.PlayfieldImage);
            Assert.Equal(TestString2, shell.ConfigName);
            //Assert.Equal(testString3, shell.);
            Assert.Equal(TestString4, shell.MediaBaseFileLocation);

            Assert.Equal(soundsList, _gameState.Sounds);
            Assert.Equal(videosList, _gameState.Videos);
            Assert.Equal(imagesList, _gameState.Images);

            Assert.NotEmpty(_gameState.Switches);
            Assert.NotEmpty(_gameState.Coils);
            Assert.NotEmpty(_gameState.StepperMotors);
            Assert.NotEmpty(_gameState.Servos);
            Assert.NotEmpty(_gameState.Leds);
        }

        #region Create TestData

        private const string TestString1 = "The";
        private const string TestString2 = "Quick";
        private const string TestString3 = "Brown";
        private const string TestString4 = "Fox";

        private static List<Switch> GetSwitches()
        {
            var switches = new List<Switch>
            {
                new Switch()
                {
                    Name = "Test switch 1"
                },
                new Switch()
                {
                    Name = "Test switch 2"
                }
            };
            return switches;
        }

        private static List<Coil> GetCoils()
        {
            var coils = new List<Coil>
            {
                new Coil()
                {
                    Name = "Test Coil 1"
                },
                new Coil()
                {
                    Name = "Test Coil 2"
                }
            };
            return coils;
        }

        private static List<StepperMotor> GetStepperMotors()
        {
            var stepperMotors = new List<StepperMotor>
            {
                new StepperMotor()
                {
                    Name = "Test StepperMotor 1"
                },
                new StepperMotor()
                {
                    Name = "Test StepperMotor 2"
                }
            };
            return stepperMotors;
        }

        private static List<Servo> GetServos()
        {
            var servos = new List<Servo>
            {
                new Servo()
                {
                    Name = "Test Servo 1"
                },
                new Servo()
                {
                    Name = "Test Servo 2"
                }
            };
            return servos;
        }

        private static List<Led> GetLeds()
        {
            var leds = new List<Led>
            {
                new Led()
                {
                    Name = "Test Led 1"
                },
                new Led()
                {
                    Name = "Test Led 2"
                }
            };
            return leds;
        }

        #endregion

    }
}
