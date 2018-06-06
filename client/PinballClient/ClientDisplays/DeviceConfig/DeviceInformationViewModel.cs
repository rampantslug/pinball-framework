using Caliburn.Micro;
using System.ComponentModel.Composition;
using Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PinballClient.ClientDisplays.DeviceConfig;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;

namespace PinballClient.ClientDisplays.DeviceConfig
{
    [Export(typeof(IDeviceConfig))]
    public sealed class DeviceInformationViewModel : Conductor<IScreen>.Collection.OneActive, IDeviceConfig, 
        IHandle<ShowSwitchConfigEvent>,
        IHandle<ShowCoilConfigEvent>,
        IHandle<ShowStepperMotorConfigEvent>,
        IHandle<ShowServoConfigEvent>,
        IHandle<ShowLedConfigEvent>,
        IHandle<UpdatePlayfieldImageEvent>,
        IHandle<HighlightSwitchEvent>,
        IHandle<HighlightCoilEvent>,
        IHandle<HighlightStepperMotorEvent>,
        IHandle<HighlightServoEvent>,
        IHandle<HighlightLedEvent>
    {

        #region Properties

        public string DeviceType => SelectedDevice != null ? SelectedDevice.GetType().ToString() : string.Empty;


        public ImageSource PlayfieldImage
        {
            get
            {
                if (_playfieldImage == null)
                {
                    if(_gameState.PlayfieldImage != null)
                        _playfieldImage = ImageConversion.ConvertStringToImage(_gameState.PlayfieldImage);
                }

                return _playfieldImage;
            }
            set
            {
                _playfieldImage = value;
                NotifyOfPropertyChange(() => PlayfieldImage);
            }
        }

        public double ScaleFactorX
        {
            get
            {
                return _scaleFactorX;
            }
            set
            {
                _scaleFactorX = value;
                NotifyOfPropertyChange(() => ScaleFactorX);
            }
        }

        public double ScaleFactorY
        {
            get
            {
                return _scaleFactorY;
            }
            set
            {
                _scaleFactorY = value;
                NotifyOfPropertyChange(() => ScaleFactorY);
            }
        }



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
                NotifyOfPropertyChange(() => DeviceType);
            }
        }

        #endregion


        [ImportingConstructor]
        public DeviceInformationViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;

            DisplayName = "Device Info";

            // These 2 values assume an image size of 200x400
            ScaleFactorX = 2;
            ScaleFactorY = 4;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new DisplayLoadedEvent());
        }

        /*
        *  Handle show config event
        */

        public void Handle(ShowSwitchConfigEvent deviceMessage)
        {
            ShowSwitchConfiguration(deviceMessage.SwitchVm);  
        }

        public void Handle(ShowCoilConfigEvent deviceMessage)
        {
            ShowCoilConfiguration(deviceMessage.CoilVm);
        }

        public void Handle(ShowStepperMotorConfigEvent deviceMessage)
        {
            ShowStepperMotorConfiguration(deviceMessage.StepperMotorVm);
        }

        public void Handle(ShowServoConfigEvent deviceMessage)
        {
            ShowServoConfiguration(deviceMessage.ServoVm);
        }

        public void Handle(ShowLedConfigEvent deviceMessage)
        {
            ShowLedConfiguration(deviceMessage.LedVm);
        }

        /*
         *  Handle various highlight events
         */

        public void Handle(HighlightSwitchEvent deviceMessage)
        {
            ShowSwitchConfiguration(deviceMessage.SwitchVm);
        }

        public void Handle(HighlightCoilEvent deviceMessage)
        {
            ShowCoilConfiguration(deviceMessage.CoilVm);
        }

        public void Handle(HighlightStepperMotorEvent deviceMessage)
        {
            ShowStepperMotorConfiguration(deviceMessage.StepperMotorVm);
        }

        public void Handle(HighlightServoEvent deviceMessage)
        {
            ShowServoConfiguration(deviceMessage.ServoVm);
        }

        public void Handle(HighlightLedEvent deviceMessage)
        {
            ShowLedConfiguration(deviceMessage.LedVm);
        }

        private void ShowSwitchConfiguration(SwitchViewModel switchVm)
        {
            ActivateItem(new SwitchConfigViewModel(switchVm));
            SelectedDevice = switchVm;
        }

        private void ShowCoilConfiguration(CoilViewModel coilVm)
        {
            ActivateItem(new CoilConfigViewModel(coilVm));
            SelectedDevice = coilVm;
        }

        private void ShowStepperMotorConfiguration(StepperMotorViewModel stepperMotorVm)
        {
            ActivateItem(new StepperMotorConfigViewModel(stepperMotorVm));
            SelectedDevice = stepperMotorVm;
        }

        private void ShowServoConfiguration(ServoViewModel servoVm)
        {
            ActivateItem(new ServoConfigViewModel(servoVm));
            SelectedDevice = servoVm;
        }

        private void ShowLedConfiguration(LedViewModel ledVm)
        {
            ActivateItem(new LedConfigViewModel(ledVm));
            SelectedDevice = ledVm;
        }


        /// <summary>
        /// Update playfield image based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(UpdatePlayfieldImageEvent message)
        {
            PlayfieldImage = ImageConversion.ConvertStringToImage(message.PlayfieldImage);

        }

        #region Handle Mouse movement / Dragging of Device

        public void MouseEnter(object source)
        {
            // change mouse cursor
            Mouse.OverrideCursor = Cursors.Hand;
        }

        public void MouseLeave(object source)
        {
            // change mouse cursor
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public Point StartingPoint { get; set; }

        public void MouseDown(object source)
        {
            

            var myGrid = source as Grid;
            if (myGrid != null)
            {
                //var parent = myGrid.Parent;
                //if (parent != null)
                //{                 
                StartingPoint = Mouse.GetPosition(myGrid);

            }
        }

        public void MouseMove(object source)
        {
            var myGrid = source as Grid;
            if (Mouse.LeftButton == MouseButtonState.Pressed && myGrid != null && SelectedDevice != null)
            {

                    var currentPoint = Mouse.GetPosition(myGrid);
                    var xDelta = currentPoint.X - StartingPoint.X;
                    var yDelta = currentPoint.Y - StartingPoint.Y;

                    SelectedDevice.VirtualLocationX = SelectedDevice.VirtualLocationX + (int)(xDelta/ScaleFactorX);
                    SelectedDevice.VirtualLocationY = SelectedDevice.VirtualLocationY + (int)(yDelta/ScaleFactorY);
            }
        }

        #endregion


        private readonly IEventAggregator _eventAggregator;
        private readonly IGameState _gameState;

        private DeviceViewModel _selectedDevice;
        private ImageSource _playfieldImage;
        private double _scaleFactorX;
        private double _scaleFactorY;
    }
}
