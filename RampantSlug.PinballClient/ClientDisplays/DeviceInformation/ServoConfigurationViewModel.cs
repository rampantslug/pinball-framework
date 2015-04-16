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

    public class ServoConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private ServoViewModel _servo;

        #region Properties

        public ushort Number
        {
            get { return _servo.Number; }
        }

        public string Address
        {
            get { return _servo.Address; }
            set
            {
                _servo.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public string Name
        {
            get { return _servo.Name; }
            set
            {
                _servo.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }


   /*     public DateTime LastChangeTimeStamp
        {
            get { return _servo.LastChangeTimeStamp; }
            set
            {
                _servo.LastChangeTimeStamp = value;
                NotifyOfPropertyChange(() => LastChangeTimeStamp);
            }
        }

        // TODO: Should this be an enum of available colours based on what is set for the project        
        public string WiringColors
        {
            get { return _servo.WiringColors; }
            set
            {
                _servo.WiringColors = value;
                NotifyOfPropertyChange(() => WiringColors);
            }
        }*/

        #endregion

        public ServoConfigurationViewModel(ServoViewModel servo) 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

            _servo = servo;

        }



        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_servo.Device as Servo); 
        }
 
    }

}
