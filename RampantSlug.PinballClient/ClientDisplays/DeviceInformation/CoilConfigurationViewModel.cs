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

    public class CoilConfigurationViewModel : Screen, IDeviceConfigurationScreen
    {
        #region Fields

        private CoilViewModel _coil;

        #endregion

        #region Properties

        public CoilViewModel Coil
        {
            get { return _coil; }
            set
            {
                _coil = value;
                NotifyOfPropertyChange(() => Coil);
            }
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


        public ObservableCollection<HistoryRowViewModel> PreviousStates
        {
            get
            {
                return _coil.PreviousStates;
            }

        }
        

        #endregion

        #region Constructor
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilDevice"></param>
        public CoilConfigurationViewModel(CoilViewModel coilDevice)
        {
            _coil = coilDevice;
        }

        #endregion

        public void PulseState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_coil.Device as Coil, CoilCommand.PulseActive);
        }

        public void HoldState()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendCommandDeviceMessage(_coil.Device as Coil, CoilCommand.HoldActive);
        }

        public void SaveDevice()
        {
            var busController = IoC.Get<IClientBusController>();
            busController.SendConfigureDeviceMessage(_coil.Device as Coil);
        }
 
    }
}
