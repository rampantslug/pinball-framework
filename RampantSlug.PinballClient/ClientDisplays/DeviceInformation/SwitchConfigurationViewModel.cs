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

    public class SwitchConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {

        private SwitchViewModel _switch;

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

        public ushort Number
        {
            get { return _switch.Number; }
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
            get { return _switch.SwitchName; }
            set
            {
                _switch.SwitchName = value;
                NotifyOfPropertyChange(() => Name);
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

        public string State
        {
            get
            {
                return _switch.SwitchState;
            }
            set
            {
                _switch.SwitchName = value;
                NotifyOfPropertyChange(() => State);
            }

        }

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

        public SwitchConfigurationViewModel(SwitchViewModel switchvm) 
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();
           // eventAggregator.Subscribe(this);

            _switch = switchvm;
            
        }



        public void SaveDevice()
        {
           var busController = IoC.Get<IClientBusController>();
           busController.SendConfigureDeviceMessage(_switch.Device as Switch); 
        }

        public void PulseState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.PulseActive);
        }

        public void HoldState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_switch.Device as Switch, SwitchCommand.HoldActive);
        }
 
    }

    public interface IDeviceConfigurationScreen : IScreen
    {

        void SaveDevice();
    }
}
