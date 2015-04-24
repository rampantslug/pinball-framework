using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class LedConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private LedViewModel _led;

        #region Properties


        public LedViewModel Led
        {
            get { return _led; }
            set
            {
                _led = value;
                NotifyOfPropertyChange(() => Led);
            }
        }

        public string Address
        {
            get { return _led.Address; }
            set
            {
                _led.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _led.PreviousStates;
            }

        }
        

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledvm"></param>
        public LedConfigurationViewModel(LedViewModel ledvm) 
        {
            _led = ledvm;
            
        }



        public void SaveDevice()
        {
           var busController = IoC.Get<IClientBusController>();
           busController.SendConfigureDeviceMessage(_led.Device as Led); 
        }

        public void RemoveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_led.Device as Led, true);
        }

        public void ActivateLed()
        {
            _led.ActivateLed();
        }

        public void DeactivateLed()
        {
            _led.DeactivateLed();
        }
 
    }
}
