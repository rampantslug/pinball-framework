using Caliburn.Micro;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Magnum.Extensions;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{
    public sealed class DeviceInformationViewModel : Conductor<IScreen>.Collection.OneActive, IDeviceInformation, 
        IHandle<ShowSwitchConfig>,
        IHandle<ShowCoilConfig>,
        IHandle<ShowStepperMotorConfig>,
        IHandle<ShowServoConfig>,
        IHandle<ShowLedConfig>,
        IHandle<UpdatePlayfieldImage>,
        IHandle<HighlightSwitch>,
        IHandle<HighlightCoil>,
        IHandle<HighlightStepperMotor>,
        IHandle<HighlightServo>,
        IHandle<HighlightLed>
    {

        private IEventAggregator _eventAggregator;
        private DeviceViewModel _selectedDevice;
        private ImageSource _playfieldImage;
        private double _scalingFactor = 2;


        public string DeviceType
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.GetType().ToShortTypeName() : string.Empty;
            }
        }


        public ImageSource PlayfieldImage
        {
            get
            {
                if (_playfieldImage == null)
                {
                    var shell = IoC.Get<IShell>();
                    if(shell != null && shell.PlayfieldImage != null)
                        _playfieldImage = ImageConversion.ConvertStringToImage(shell.PlayfieldImage);
                }

                return _playfieldImage;
            }
            set
            {
                _playfieldImage = value;
                NotifyOfPropertyChange(() => PlayfieldImage);
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

        


        public DeviceInformationViewModel() 
        {
            DisplayName = "Device Info";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
           
        }

        /*
        *  Handle show config event
        */

        public void Handle(ShowSwitchConfig deviceMessage)
        {
            ShowSwitchConfiguration(deviceMessage.SwitchVm);  
        }

        public void Handle(ShowCoilConfig deviceMessage)
        {
            ShowCoilConfiguration(deviceMessage.CoilVm);
        }

        public void Handle(ShowStepperMotorConfig deviceMessage)
        {
            ShowStepperMotorConfiguration(deviceMessage.StepperMotorVm);
        }

        public void Handle(ShowServoConfig deviceMessage)
        {
            ShowServoConfiguration(deviceMessage.ServoVm);
        }

        public void Handle(ShowLedConfig deviceMessage)
        {
            ShowLedConfiguration(deviceMessage.LedVm);
        }

        /*
         *  Handle various highlight events
         */

        public void Handle(HighlightSwitch deviceMessage)
        {
            ShowSwitchConfiguration(deviceMessage.SwitchVm);
        }

        public void Handle(HighlightCoil deviceMessage)
        {
            ShowCoilConfiguration(deviceMessage.CoilVm);
        }

        public void Handle(HighlightStepperMotor deviceMessage)
        {
            ShowStepperMotorConfiguration(deviceMessage.StepperMotorVm);
        }

        public void Handle(HighlightServo deviceMessage)
        {
            ShowServoConfiguration(deviceMessage.ServoVm);
        }

        public void Handle(HighlightLed deviceMessage)
        {
            ShowLedConfiguration(deviceMessage.LedVm);
        }

        private void ShowSwitchConfiguration(SwitchViewModel switchVM)
        {
            ActivateItem(new SwitchConfigurationViewModel(switchVM));
            SelectedDevice = switchVM;
        }

        private void ShowCoilConfiguration(CoilViewModel coilVM)
        {
            ActivateItem(new CoilConfigurationViewModel(coilVM));
            SelectedDevice = coilVM;
        }

        private void ShowStepperMotorConfiguration(StepperMotorViewModel stepperMotorVM)
        {
            ActivateItem(new StepperMotorConfigurationViewModel(stepperMotorVM));
            SelectedDevice = stepperMotorVM;
        }

        private void ShowServoConfiguration(ServoViewModel servoVM)
        {
            ActivateItem(new ServoConfigurationViewModel(servoVM));
            SelectedDevice = servoVM;
        }

        private void ShowLedConfiguration(LedViewModel ledVM)
        {
            ActivateItem(new LedConfigurationViewModel(ledVM));
            SelectedDevice = ledVM;
        }


        /// <summary>
        /// Update playfield image based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(UpdatePlayfieldImage message)
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
                var parent = myGrid.Parent;
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

                    SelectedDevice.VirtualLocationX = SelectedDevice.VirtualLocationX + (int)(xDelta * _scalingFactor);
                    SelectedDevice.VirtualLocationY = SelectedDevice.VirtualLocationY + (int)(yDelta * _scalingFactor);
            }
        }

        #endregion

    }
}
