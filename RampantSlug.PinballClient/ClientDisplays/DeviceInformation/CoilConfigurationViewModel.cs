using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class CoilConfigurationViewModel : Screen
    {

        private CoilViewModel _coil;

        #region Properties

        public ushort Number
        {
            get { return _coil.Number; }
        }

        public string Address
        {
            get { return _coil.Address; }
            set
            {
                _coil.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public string Name
        {
            get { return _coil.Name; }
            set
            {
                _coil.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }



 

  /*      public DateTime LastChangeTimeStamp
        {
            get { return _coil.LastChangeTimeStamp; }
            set
            {
                _coil.LastChangeTimeStamp = value;
                NotifyOfPropertyChange(() => LastChangeTimeStamp);
            }
        }

        // TODO: Should this be an enum of available colours based on what is set for the project        
        public string WiringColors
        {
            get { return _coil.WiringColors; }
            set
            {
                _coil.WiringColors = value;
                NotifyOfPropertyChange(() => WiringColors);
            }
        }*/

        #endregion

        public CoilConfigurationViewModel(CoilViewModel coilDevice) 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

            _coil = coilDevice;

        }

        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_coil.Device as Coil);
        }
 
    }
}
