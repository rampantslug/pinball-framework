using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessObjects.Devices;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;


namespace PinballClient.ClientDisplays.DeviceTree
{
    [Export(typeof(IDeviceTree))]
    public class DeviceTreeViewModel: 
        Screen, 
        IDeviceTree, 
        IHandle<CommonViewModelsLoadedEvent>
    {

        #region Properties

        public DeviceViewModel SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
                NotifyOfPropertyChange(() => SelectedDevice);
                if (SelectedDevice != null)
                {
                    SelectedDevice.IsSelected = true;
                }
            }
        }

        public ObservableCollection<DeviceViewModel> Switches
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

        public ObservableCollection<DeviceViewModel> Coils
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

        public ObservableCollection<DeviceViewModel> StepperMotors
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

        public ObservableCollection<DeviceViewModel> Servos
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

        public ObservableCollection<DeviceViewModel> Leds
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


        public bool SwitchesVisible
        {
            get
            {
                return _switchesVisible;
            }
            set
            {
                _switchesVisible = value;
                NotifyOfPropertyChange(() => SwitchesVisible);
            }
        }

        public bool CoilsVisible
        {
            get
            {
                return _coilsVisible;
            }
            set
            {
                _coilsVisible = value;
                NotifyOfPropertyChange(() => CoilsVisible);
            }
        }

        public bool StepperMotorsVisible
        {
            get
            {
                return _stepperMotorsVisible;
            }
            set
            {
                _stepperMotorsVisible = value;
                NotifyOfPropertyChange(() => StepperMotorsVisible);
            }
        }

        public bool ServosVisible
        {
            get
            {
                return _servosVisible;
            }
            set
            {
                _servosVisible = value;
                NotifyOfPropertyChange(() => ServosVisible);
            }
        }

        public bool LedsVisible
        {
            get
            {
                return _ledsVisible;
            }
            set
            {
                _ledsVisible = value;
                NotifyOfPropertyChange(() => LedsVisible);
            }
        }

        #endregion

        #region Constructor


        [ImportingConstructor]
        public DeviceTreeViewModel(IEventAggregator eventAggregator, IGameState gameState, IClientCommsController clientCommsController)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;
            _clientCommsController = clientCommsController;

            DisplayName = "Devices";

            SwitchesVisible = true;
            CoilsVisible = true;
            StepperMotorsVisible = true;
            ServosVisible = true;
            LedsVisible = true;
        }

        #endregion

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new DisplayLoadedEvent());
        }

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoadedEvent message)
        {
            BuildTree();
        }

        private void BuildTree() 
        {
            Switches = new ObservableCollection<DeviceViewModel>(_gameState.Switches);
            Coils = new ObservableCollection<DeviceViewModel>(_gameState.Coils);
            StepperMotors = new ObservableCollection<DeviceViewModel>(_gameState.StepperMotors);
            Servos = new ObservableCollection<DeviceViewModel>(_gameState.Servos);
            Leds = new ObservableCollection<DeviceViewModel>(_gameState.Leds);
        }

        public void AddSwitch()
        {
            _eventAggregator.PublishOnUIThread(new ShowSwitchConfigEvent() { SwitchVm = new SwitchViewModel(new Switch(), _clientCommsController, _eventAggregator) });
        }

        public void AddCoil()
        {
            _eventAggregator.PublishOnUIThread(new ShowCoilConfigEvent() { CoilVm = new CoilViewModel(new Coil(), _clientCommsController, _eventAggregator) });
        }

        public void AddStepperMotor()
        {
            _eventAggregator.PublishOnUIThread(new ShowStepperMotorConfigEvent() { StepperMotorVm = new StepperMotorViewModel(new StepperMotor(), _clientCommsController, _eventAggregator) });
        }

        public void AddServo()
        {
            _eventAggregator.PublishOnUIThread(new ShowServoConfigEvent() { ServoVm = new ServoViewModel(new Servo(), _clientCommsController, _eventAggregator) });
        }

        public void AddLed()
        {
            _eventAggregator.PublishOnUIThread(new ShowLedConfigEvent() { LedVm = new LedViewModel(new Led(), _clientCommsController, _eventAggregator) });
        }

        public void HideShowSwitches()
        {
            SwitchesVisible = !SwitchesVisible;
            _eventAggregator.PublishOnUIThread(new AllSwitchesVisibilityEvent() { IsVisible = SwitchesVisible });       
        }

        public void HideShowCoils()
        {
            CoilsVisible = !CoilsVisible;
            _eventAggregator.PublishOnUIThread(new AllCoilsVisibilityEvent() { IsVisible = CoilsVisible });
        }

        public void HideShowStepperMotors()
        {
            StepperMotorsVisible = !StepperMotorsVisible;
            _eventAggregator.PublishOnUIThread(new AllStepperMotorsVisibilityEvent() { IsVisible = StepperMotorsVisible });
        }

        public void HideShowServos()
        {
            ServosVisible = !ServosVisible;
            _eventAggregator.PublishOnUIThread(new AllServosVisibilityEvent() { IsVisible = ServosVisible });
        }

        public void HideShowLeds()
        {
            LedsVisible = !LedsVisible;
            _eventAggregator.PublishOnUIThread(new AllLedsVisibilityEvent() { IsVisible = LedsVisible });
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly IClientCommsController _clientCommsController;
        private readonly IGameState _gameState;

        private ObservableCollection<DeviceViewModel> _switches;
        private ObservableCollection<DeviceViewModel> _coils;
        private ObservableCollection<DeviceViewModel> _stepperMotors;
        private ObservableCollection<DeviceViewModel> _servos;
        private ObservableCollection<DeviceViewModel> _leds;

        private DeviceViewModel _selectedDevice;

        private bool _switchesVisible;
        private bool _coilsVisible;
        private bool _stepperMotorsVisible;
        private bool _servosVisible;
        private bool _ledsVisible;
    }
}
