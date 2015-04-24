using System;
using RampantSlug.Common;
using RampantSlug.PinballClient.ContractImplementations;
using MassTransit;
using RampantSlug.Contracts;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using RampantSlug.PinballClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using RampantSlug.PinballClient.ClientDisplays.LogMessages;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree;
using RampantSlug.PinballClient.ClientDisplays.GameStatus;
using RampantSlug.PinballClient.ClientDisplays.Playfield;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;

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

        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;

        // ViewModels for the various devices
        private ObservableCollection<SwitchViewModel> _switches;
        private ObservableCollection<CoilViewModel> _coils;
        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        private ObservableCollection<ServoViewModel> _servos;
        private ObservableCollection<LedViewModel> _leds;


        // Client Displays
        public IDeviceInformation DeviceInformation { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }
        public IGameStatus GameStatus { get; private set; }
        public IPlayfield Playfield { get; private set; }


        public MidPanelViewModel MidPanel { get; private set; }

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
            
            
        [ImportingConstructor]
        public ShellViewModel(
            IEventAggregator eventAggregator,
            ILogMessages logMessages,
            IDeviceInformation deviceInformation, 
            ISwitchMatrix switchMatrix, 
            IDeviceTree deviceTree,
            IGameStatus gameStatus,
            IPlayfield playfield       
            ) 
        {
            _eventAggregator = eventAggregator;
            LogMessages = logMessages;
            DeviceInformation = deviceInformation;
            SwitchMatrix = switchMatrix;
            DeviceTree = deviceTree;
            GameStatus = gameStatus;
            Playfield = playfield;

            _busController = IoC.Get<IClientBusController>();
            _busController.Start();

            MidTabs = new BindableCollection<IScreen>();
            RightTabs = new BindableCollection<IScreen>();
            
            DisplayName = "Client";

            MidPanel = new MidPanelViewModel(eventAggregator, deviceInformation, switchMatrix, gameStatus);

            Switches = new ObservableCollection<SwitchViewModel>();
            Coils = new ObservableCollection<CoilViewModel>();
            StepperMotors = new ObservableCollection<StepperMotorViewModel>();
            Servos = new ObservableCollection<ServoViewModel>();
            Leds = new ObservableCollection<LedViewModel>();
        }


        public void Exit()
        {
            _busController.Stop();
        }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

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

            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = DateTime.Now,
                EventType = "System",
                Name = "Received Settings",
                State = "OK",
                Information = "Updated system information from config."

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
            foreach (var swVM in Switches.Where(swVM => swVM.Number == deviceMessage.Device.Number))
            {
                swVM.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);             
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = deviceMessage.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Switch Event for: " + deviceMessage.Device.Name
            });
        }

        public void Handle(UpdateCoil deviceMessage)
        {
            foreach (var coilVM in Coils.Where(coilVM => coilVM.Number == deviceMessage.Device.Number))
            {
                coilVM.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = deviceMessage.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Coil Event for: " + deviceMessage.Device.Name
            });
        }

        public void Handle(UpdateStepperMotor deviceMessage)
        {
            foreach (var stepperMotorVM in StepperMotors.Where(stepperMotorVM => stepperMotorVM.Number == deviceMessage.Device.Number))
            {
                stepperMotorVM.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = deviceMessage.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Stepper Motor Event for: " + deviceMessage.Device.Name
            });
        }

        public void Handle(UpdateServo deviceMessage)
        {
            foreach (var servoVM in Servos.Where(servoVM => servoVM.Number == deviceMessage.Device.Number))
            {
                servoVM.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = deviceMessage.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Servo Event for: " + deviceMessage.Device.Name
            });
        }

        public void Handle(UpdateLed deviceMessage)
        {
            foreach (var ledVM in Leds.Where(ledVM => ledVM.Number == deviceMessage.Device.Number))
            {
                ledVM.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            _eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = deviceMessage.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Led Event for: " + deviceMessage.Device.Name
            });
        }

        #endregion

    }
}