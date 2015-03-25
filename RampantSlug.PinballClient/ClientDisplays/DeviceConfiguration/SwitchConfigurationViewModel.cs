using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceConfiguration
{

    public class SwitchConfigurationViewModel : Screen
    {

        private Switch _switch;

        #region Properties

        public int Number
        {
            get { return _switch.Number; }
            set
            {
                _switch.Number = value;
                NotifyOfPropertyChange(() => Number);
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

        public string Name
        {
            get { return _switch.Name; }
            set
            {
                _switch.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        // TODO: This needs to be changed to enum of switch types
        public string SwitchType
        {
            get { return _switch.SwitchType; }
            set
            {
                _switch.SwitchType = value;
                NotifyOfPropertyChange(() => SwitchType);
            }
        }

        public string State
        {
            get { return _switch.State; }
            set
            {
                _switch.State = value;
                NotifyOfPropertyChange(() => State);
            }
        }

        public DateTime LastChangeTimeStamp
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
        }

        #endregion

        public SwitchConfigurationViewModel(Switch switchDevice) 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

            _switch = switchDevice;

        }


 
    }
}
