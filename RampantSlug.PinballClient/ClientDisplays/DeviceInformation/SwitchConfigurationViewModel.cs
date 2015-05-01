﻿using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RampantSlug.Common.Commands;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class SwitchConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private SwitchViewModel _switch;
        private ImageSource _deviceTypeImage;

        #region Properties


        public SwitchViewModel Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                NotifyOfPropertyChange(() => Switch);
            }
        }

        public string Address
        {
            get { return _switch.Address; }
            set
            {
                _switch.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public ImageSource DeviceTypeImage
        {
            get
            {
                return _deviceTypeImage;
            }
            set
            {
                _deviceTypeImage = value;
                NotifyOfPropertyChange(() => DeviceTypeImage);
            }
        }


   /*     public string Type
        {
            get { return _switch.Type.ToString(); }
            set
            {
                _switch.Type = (SwitchType) Enum.Parse(typeof (SwitchType),value, true);
                NotifyOfPropertyChange(() => Type);
            }
        }*/



    /*    public DateTime LastChangeTimeStamp
        {
            get { return _switch.LastChangeTimeStamp; }
            set
            {
                _switch.LastChangeTimeStamp = value;
                NotifyOfPropertyChange(() => LastChangeTimeStamp);
            }
        }

        // TODO: Should this be an enum of available colours based on what is set for the project        
        public string WiringColors
        {
            get { return _switch.WiringColors; }
            set
            {
                _switch.WiringColors = value;
                NotifyOfPropertyChange(() => WiringColors);
            }
        }*/

        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _switch.PreviousStates;
            }

        }
        

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchvm"></param>
        public SwitchConfigurationViewModel(SwitchViewModel switchvm) 
        {
            _switch = switchvm;


            var path = System.IO.Directory.GetCurrentDirectory();
            var additionalpath = path + @"\DeviceResources\Switches\rollover.jpg";

            DeviceTypeImage = new BitmapImage(new Uri(additionalpath));
        }



        public void SaveDevice()
        {
           var busController = IoC.Get<IClientBusController>();
           busController.SendConfigureDeviceMessage(_switch.Device as Switch); 
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_switch.Device as Switch, true);
        }

        public void PressState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.PressActive);
        }

        public void HoldState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.HoldActive);
        }
 
    }
}
