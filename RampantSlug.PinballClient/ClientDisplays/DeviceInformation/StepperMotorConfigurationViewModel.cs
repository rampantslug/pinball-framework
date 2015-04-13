using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{

    public class StepperMotorConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private StepperMotor _stepperMotor;

        #region Properties

        public ushort Number
        {
            get { return _stepperMotor.Number; }
            set
            {
                _stepperMotor.Number = value;
                NotifyOfPropertyChange(() => Number);
            }
        }

        public string Address
        {
            get { return _stepperMotor.Address; }
            set
            {
                _stepperMotor.Address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public string Name
        {
            get { return _stepperMotor.Name; }
            set
            {
                _stepperMotor.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

    

        public DateTime LastChangeTimeStamp
        {
            get { return _stepperMotor.LastChangeTimeStamp; }
            set
            {
                _stepperMotor.LastChangeTimeStamp = value;
                NotifyOfPropertyChange(() => LastChangeTimeStamp);
            }
        }

        // TODO: Should this be an enum of available colours based on what is set for the project        
        public string WiringColors
        {
            get { return _stepperMotor.WiringColors; }
            set
            {
                _stepperMotor.WiringColors = value;
                NotifyOfPropertyChange(() => WiringColors);
            }
        }

        #endregion

        public StepperMotorConfigurationViewModel(StepperMotor stepperMotor) 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

            _stepperMotor = stepperMotor;

        }



        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_stepperMotor); 
        }
 
    }
}
