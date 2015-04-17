﻿using Caliburn.Micro;
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
        IHandle<UpdatePlayfieldImage>,
        IHandle<HighlightSwitch>,
        IHandle<HighlightCoil>,
        IHandle<HighlightStepperMotor>,
        IHandle<HighlightServo>
    {

        private IEventAggregator _eventAggregator;
        private DeviceViewModel _selectedDevice;
        private ImageSource _playfieldImage;
        private double _scalingFactor = 2;

        public ushort DeviceId
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Number : (ushort) 0;
            }
        }

        public string DeviceType
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.GetType().ToShortTypeName() : string.Empty;
            }
        }

        public string DeviceName
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Name : "Device name";
            }
        }

        public string DeviceAddress
        {
            get
            {
                return SelectedDevice != null ? SelectedDevice.Address : string.Empty;
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
                NotifyOfPropertyChange(() => DeviceId);
                NotifyOfPropertyChange(() => DeviceType);
                NotifyOfPropertyChange(() => DeviceName);
                NotifyOfPropertyChange(() => DeviceAddress);
            }
        }

        


        public DeviceInformationViewModel() 
        {
            DisplayName = "Device Info";
            IsMouseDown = false;

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
            SelectedDevice = deviceMessage.SwitchVm;   
        }

        /*
         *  Handle various highlight events
         */

        public void Handle(HighlightSwitch deviceMessage)
        {
            // Get correct viewmodel from shell
           // var shell = IoC.Get<IShell>();
           // var realVM = shell.Switches.FirstOrDefault(sw => sw.Number == deviceMessage.SwitchVm.Number);
          //  if (realVM != null)
          //  {
            ActivateItem(new SwitchConfigurationViewModel(deviceMessage.SwitchVm));
            SelectedDevice = deviceMessage.SwitchVm;
           // }
        }

        public void Handle(HighlightCoil deviceMessage)
        {
            ActivateItem(new CoilConfigurationViewModel(deviceMessage.CoilVm));
            SelectedDevice = deviceMessage.CoilVm;
        }

        public void Handle(HighlightStepperMotor deviceMessage)
        {
            ActivateItem(new StepperMotorConfigurationViewModel(deviceMessage.StepperMotorVm));
            SelectedDevice = deviceMessage.StepperMotorVm;
        }

        public void Handle(HighlightServo deviceMessage)
        {
            ActivateItem(new ServoConfigurationViewModel(deviceMessage.ServoVm));
            SelectedDevice = deviceMessage.ServoVm;
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
