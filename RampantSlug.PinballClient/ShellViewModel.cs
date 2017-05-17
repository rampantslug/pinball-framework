using System;
using RampantSlug.PinballClient.ContractImplementations;
using MassTransit;
using RampantSlug.Contracts;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using RampantSlug.PinballClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using RampantSlug.Common.Logging;
using RampantSlug.PinballClient.ClientDisplays.LogMessages;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree;
using RampantSlug.PinballClient.ClientDisplays.GameStatus;
using RampantSlug.PinballClient.ClientDisplays.ModeTree;
using RampantSlug.PinballClient.ClientDisplays.Playfield;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;
using Configuration = RampantSlug.Common.Configuration;

namespace RampantSlug.PinballClient {
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IShell,

        IHandle<UpdateConfigEvent>,

        IHandle<ShowSwitchConfig>,

        // Device State Changes
        IHandle<UpdateSwitch>,
        IHandle<UpdateCoil>,
        IHandle<UpdateStepperMotor>,
        IHandle<UpdateServo>,
        IHandle<UpdateLed>

    {
        private IClientBusController _busController;
        private IEventAggregator _eventAggregator;
        private string _playfieldImage;

        private BindableCollection<IScreen> _leftTabs;
        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;

        // ViewModels for the various devices
        private ObservableCollection<SwitchViewModel> _switches;
        private ObservableCollection<CoilViewModel> _coils;
        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        private ObservableCollection<ServoViewModel> _servos;
        private ObservableCollection<LedViewModel> _leds;
        private bool _settingsFlyoutIsOpen;
        private bool _connectedToServer;
        private string _serverName;
        private bool _serverIsUsingHardware;
        private string _serverIpAddress;


        // Client Displays
        public IDeviceInformation DeviceInformation { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }
        public IGameStatus GameStatus { get; private set; }
        public IPlayfield Playfield { get; private set; }
        public IModeTree ModeTree { get; private set; }


        public MidPanelViewModel MidPanel { get; private set; }

        public BindableCollection<IScreen> LeftTabs
        {
            get
            {
                return _leftTabs;
            }
            set
            {
                _leftTabs = value;
                NotifyOfPropertyChange(() => LeftTabs);
            }
        }

        public BindableCollection<IScreen> MidTabs
        {
            get
            {
                return _midTabs;
            }
            set
            {
                _midTabs = value;
                NotifyOfPropertyChange(() => MidTabs);
            }
        }

        public BindableCollection<IScreen> RightTabs
        {
            get
            {
                return _rightTabs;
            }
            set
            {
                _rightTabs = value;
                NotifyOfPropertyChange(() => RightTabs);
            }
        }

        public ObservableCollection<SwitchViewModel> Switches
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

        public ObservableCollection<CoilViewModel> Coils
        {
            get
            {
                return _coils;
            }
            set
            {
                _coils = value;
                NotifyOfPropertyChange(() => Coils);
            }
        }

        public ObservableCollection<StepperMotorViewModel> StepperMotors
        {
            get
            {
                return _stepperMotors;
            }
            set
            {
                _stepperMotors = value;
                NotifyOfPropertyChange(() => StepperMotors);
            }
        }

        public ObservableCollection<ServoViewModel> Servos
        {
            get
            {
                return _servos;
            }
            set
            {
                _servos = value;
                NotifyOfPropertyChange(() => Servos);
            }
        }

        public ObservableCollection<LedViewModel> Leds
        {
            get
            {
                return _leds;
            }
            set
            {
                _leds = value;
                NotifyOfPropertyChange(() => Leds);
            }
        }

        public string PlayfieldImage
        {
            get
            {
                return _playfieldImage;
            }
            set
            {
                _playfieldImage = value;
                NotifyOfPropertyChange(() => PlayfieldImage);
            }
        }


        public bool SettingsFlyoutIsOpen
        {
            get
            {
                return _settingsFlyoutIsOpen;
            }
            set
            {
                _settingsFlyoutIsOpen = value;
                NotifyOfPropertyChange(() => SettingsFlyoutIsOpen);
            }
        }

        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
                NotifyOfPropertyChange(() => ServerName);
            }
        }

        public string ServerIpAddress
        {
            get
            {
                return _serverIpAddress;
            }
            set
            {
                _serverIpAddress = value;
                NotifyOfPropertyChange(() => ServerIpAddress);
            }
        }

        public bool ConnectedToServer
        {
            get
            {
                return _connectedToServer;
            }
            set
            {
                _connectedToServer = value;
                NotifyOfPropertyChange(() => ConnectedToServer);
            }
        }

        public bool ServerIsUsingHardware
        {
            get
            {
                return _serverIsUsingHardware;
            }
            set
            {
                _serverIsUsingHardware = value;
                NotifyOfPropertyChange(() => ServerIsUsingHardware);
            }
        }


        

            
        [ImportingConstructor]
        public ShellViewModel(
            IEventAggregator eventAggregator,
            ILogMessages logMessages,
            IDeviceInformation deviceInformation, 
            ISwitchMatrix switchMatrix, 
            IDeviceTree deviceTree,
            IGameStatus gameStatus,
            IPlayfield playfield,
            IModeTree modeTree
            ) 
        {
            _eventAggregator = eventAggregator;
            LogMessages = logMessages;
            DeviceInformation = deviceInformation;
            SwitchMatrix = switchMatrix;
            DeviceTree = deviceTree;
            GameStatus = gameStatus;
            Playfield = playfield;
            ModeTree = modeTree;

            _busController = IoC.Get<IClientBusController>();
            _busController.Start();

            LeftTabs = new BindableCollection<IScreen>();
            MidTabs = new BindableCollection<IScreen>();
            RightTabs = new BindableCollection<IScreen>();
            
            DisplayName = "Client";

            //MidPanel = new MidPanelViewModel(eventAggregator, deviceInformation, switchMatrix, gameStatus);

            Switches = new ObservableCollection<SwitchViewModel>();
            Coils = new ObservableCollection<CoilViewModel>();
            StepperMotors = new ObservableCollection<StepperMotorViewModel>();
            Servos = new ObservableCollection<ServoViewModel>();
            Leds = new ObservableCollection<LedViewModel>();

            ServerName = "Insert Server Name Here";
            ConnectedToServer = false;

            ServerIpAddress = ConfigurationManager.AppSettings.Get("DefaultServer");

        }


        public void Exit()
        {
            _busController.Stop();
        }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            LeftTabs.Add(DeviceTree);
            LeftTabs.Add(ModeTree);

            MidTabs.Add(DeviceInformation);
            MidTabs.Add(SwitchMatrix);
            MidTabs.Add(GameStatus);
            
            //RightTabs.Add(Playfield);
            //RightTabs.Add(GameStatus);

            _eventAggregator.Subscribe(this);

            _busController.RequestSettings();
        }

        public void GetSettings()
        {
            _busController.RequestSettings();
        }

        

        protected override void OnDeactivate(bool close)
        {
            Exit();
            base.OnDeactivate(close);
        }

        public void ConnectionSettings()
        {
            SettingsFlyoutIsOpen = !SettingsFlyoutIsOpen;
        }

        public void SaveAsDefault()
        {
            // TODO: Parse ServerIpAddress to make sure it is in the correct format...
            ConfigurationManager.AppSettings.Set("DefaultServer", ServerIpAddress);
        }

        public void RestartServer()
        {
            _busController.RestartServer();
            SettingsFlyoutIsOpen = false;
        }

        public void ConnectToServer()
        {
            _busController.RequestSettings();
            SettingsFlyoutIsOpen = false;
        }

       

        /// <summary>
        /// Request to update config of a device. Make DeviceInformation active if not already
        /// </summary>
        /// <param name="deviceMessage"></param>
        public void Handle(ShowSwitchConfig deviceMessage)
        {
            Playfield.Deactivate(false);
            DeviceInformation.Activate();
        }

        public void Handle(UpdateConfigEvent updateConfigEvent)
        {
            UpdateViewModels(updateConfigEvent.MachineConfiguration);
        }


        /// <summary>
        /// Update ALL ViewModels based on config results
        /// </summary>
        /// <param name="config"></param>
        public void UpdateViewModels(Configuration config)
        {
            PlayfieldImage = config.PlayfieldImage;
            ServerName = config.ServerName;
            ServerIsUsingHardware = config.UseHardware;
            ConnectedToServer = true;

            // Create Switch View Models
            Switches.Clear();
            foreach (var sw in config.Switches)
            {
                Switches.Add(new SwitchViewModel(sw));
            }

            Coils.Clear();
            foreach (var coil in config.Coils)
            {
                Coils.Add(new CoilViewModel(coil));
            }

            StepperMotors.Clear();
            foreach (var stepperMotor in config.StepperMotors)
            {
                StepperMotors.Add(new StepperMotorViewModel(stepperMotor));
            }

            Servos.Clear();
            foreach (var servo in config.Servos)
            {
                Servos.Add(new ServoViewModel(servo));
            }

            Leds.Clear();
            foreach (var led in config.Leds)
            {
                Leds.Add(new LedViewModel(led));
            }

            _eventAggregator.PublishOnUIThread(new LogEvent
            {
                Timestamp = DateTime.Now,
                EventType = LogEventType.Info,
                OriginatorType = OriginatorType.System,
                OriginatorName = "Machine Settings",
                Status = "Received",
                Information = "Received machine settings from server."
            });

            // Notify Client Displays that Common View Models are updated
            _eventAggregator.PublishOnUIThread(new CommonViewModelsLoaded());

            if (Switches.Count > 0)
            {
                _eventAggregator.PublishOnUIThread(new ShowSwitchConfig() {SwitchVm = Switches[0]});
            }

            // Notify Client Displays that Playfield Image is updated
            _eventAggregator.PublishOnUIThread(new UpdatePlayfieldImage() { PlayfieldImage = config.PlayfieldImage});
        }


        #region Update Individual Device View Models based on state changes


        public void Handle(UpdateSwitch deviceMessage)
        {
            var switchViewModel = Switches.FirstOrDefault(swVM => swVM.Number == deviceMessage.Device.Number);

            if (switchViewModel != null)
            {
                 switchViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Switch,
                    OriginatorName = switchViewModel.Name,
                    Status = switchViewModel.State,
                    Information = "Switch Event for: " + switchViewModel.Name
                });
            }
        }

        public void Handle(UpdateCoil deviceMessage)
        {
            var coilViewModel = Coils.FirstOrDefault(coilVM => coilVM.Number == deviceMessage.Device.Number);

            if (coilViewModel != null)
            {
                coilViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Coil,
                    OriginatorName = coilViewModel.Name,
                    Status = coilViewModel.State,
                    Information = "Coil Event for: " + coilViewModel.Name
                });
            }
        }

        public void Handle(UpdateStepperMotor deviceMessage)
        {
            var stepperMotorViewModel = StepperMotors.FirstOrDefault(stepperMotorVM => stepperMotorVM.Number == deviceMessage.Device.Number);

            if (stepperMotorViewModel != null)
            {
                stepperMotorViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.StepperMotor,
                    OriginatorName = stepperMotorViewModel.Name,
                    Status = stepperMotorViewModel.State,
                    Information = "StepperMotor Event for: " + stepperMotorViewModel.Name
                });
            }
        }

        public void Handle(UpdateServo deviceMessage)
        {
            var servoViewModel = Servos.FirstOrDefault(servoVM => servoVM.Number == deviceMessage.Device.Number);

            if (servoViewModel != null)
            {
                servoViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Servo,
                    OriginatorName = servoViewModel.Name,
                    Status = servoViewModel.State,
                    Information = "Servo Event for: " + servoViewModel.Name
                });
            }
        }

        public void Handle(UpdateLed deviceMessage)
        {
            var ledViewModel = Leds.FirstOrDefault(ledVM => ledVM.Number == deviceMessage.Device.Number);

            if (ledViewModel != null)
            {
                ledViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Led,
                    OriginatorName = ledViewModel.Name,
                    Status = ledViewModel.State,
                    Information = "Led Event for: " + ledViewModel.Name
                });
            }
        }

        #endregion

    }
}