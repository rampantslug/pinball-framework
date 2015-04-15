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
        IHandle<ShowDeviceConfig>, IHandle<ConfigResults>, IHandle<DeviceChange>

    {
        private IClientBusController _busController;
        private IEventAggregator _eventAggregator;
        private ImageSource _playfieldImage;

        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;

        // ViewModels for the various devices
        private ObservableCollection<SwitchViewModel> _switches;
        private ObservableCollection<CoilViewModel> _coils;
        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        private ObservableCollection<ServoViewModel> _servos;


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

        public ImageSource PlayfieldImage
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

            MidPanel = new MidPanelViewModel(eventAggregator, deviceInformation, playfield);

            Switches = new ObservableCollection<SwitchViewModel>();
            Coils = new ObservableCollection<CoilViewModel>();
            StepperMotors = new ObservableCollection<StepperMotorViewModel>();
            Servos = new ObservableCollection<ServoViewModel>();
        }


        public void Exit()
        {
            _busController.Stop();
        }

        protected override void OnViewLoaded(object view)
        {

            base.OnViewLoaded(view);

            //MidTabs.Add(Playfield);
            //MidTabs.Add(DeviceInformation);

            RightTabs.Add(SwitchMatrix);
            RightTabs.Add(GameStatus);

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
        public void Handle(ShowDeviceConfig deviceMessage)
        {
            Playfield.Deactivate(false);
            DeviceInformation.Activate();
        }

        /// <summary>
        /// Update ViewModels based on config results
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ConfigResults message)
        {
            PlayfieldImage = ImageConversion.ConvertStringToImage(message.MachineConfiguration.PlayfieldImage);

            // Create Switch View Models
            Switches.Clear();
            foreach (var sw in message.MachineConfiguration.Switches)
            {
                Switches.Add(new SwitchViewModel(sw));
            }

            Coils.Clear();
            foreach (var coil in message.MachineConfiguration.Coils)
            {
                Coils.Add(new CoilViewModel(coil));
            }

            StepperMotors.Clear();
            foreach (var stepperMotor in message.MachineConfiguration.StepperMotors)
            {
                StepperMotors.Add(new StepperMotorViewModel(stepperMotor));
            }

            Servos.Clear();
            foreach (var servo in message.MachineConfiguration.Servos)
            {
                Servos.Add(new ServoViewModel(servo));
            }  
    
            // Notify Client Displays that Common View Models are updated
            _eventAggregator.PublishOnUIThread(new CommonViewModelsLoaded());
        }

        /// <summary>
        /// Update single device based on notification
        /// </summary>
        /// <param name="message"></param>
        public void Handle(DeviceChange message)
        {
            var deviceViewModel = FindDeviceViewModelById(message.Device.Number);
            if (deviceViewModel != null)
            {
                deviceViewModel.Device = message.Device;
                deviceViewModel.Refresh();                
            }
        }

        private DeviceViewModel FindDeviceViewModelById(ushort deviceId)
        {
            foreach (var swVM in Switches.Where(swVM => swVM.Number == deviceId))
            {
                return swVM;
            }
            foreach (var coilVM in Coils.Where(coilVM => coilVM.Number == deviceId))
            {
                return coilVM;
            }
            foreach (var stepperMotorVM in StepperMotors.Where(stepperMotorVM => stepperMotorVM.Number == deviceId))
            {
                return stepperMotorVM;
            }
            foreach (var servoVM in Servos.Where(servoVM => servoVM.Number == deviceId))
            {
                return servoVM;
            }

            return null;
        }
    }
}