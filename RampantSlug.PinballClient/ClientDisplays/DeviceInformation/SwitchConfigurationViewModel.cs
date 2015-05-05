using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using RampantSlug.Common.Commands;
using RampantSlug.PinballClient.CommonViewModels;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class SwitchConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private SwitchViewModel _switch;
        private ImageSource _refinedTypeImage;

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

        public ImageSource RefinedTypeImage
        {
            get
            {
                return _refinedTypeImage;
            }
            set
            {
                _refinedTypeImage = value;
                NotifyOfPropertyChange(() => RefinedTypeImage);
            }
        }

        public bool RefinedTypeImageExists
        {
            get
            {
                return RefinedTypeImage != null;
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
            var additionalpath = path + @"\DeviceResources\Switches\" + switchvm.RefinedType +  ".png";

            if (File.Exists(additionalpath))
            {
                RefinedTypeImage = new BitmapImage(new Uri(additionalpath));
            }
            else
            {
                // Image is null so Text below it should show up??
            }


            
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

        public async void SelectRefinedType()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            MessageDialogResult result = await metroWindow.ShowMessageAsync("This is the title", "Some message", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Do something
            }
        }

    }
}
