using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.DeviceAddress;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.SwitchMatrix
{
    public sealed class SwitchMatrixItemViewModel: Screen
    {
        private SwitchViewModel _switch;

        public SwitchViewModel Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                NotifyOfPropertyChange(() => Switch);
            }
        }

        public int MatrixColumn
        {
            get
            {
                var smAddress = _switch.Address as PsmAddress;
                return smAddress.MatrixColumn;
            }
        }

        public int MatrixRow
        {
            get
            {
                var smAddress = _switch.Address as PsmAddress;
                return smAddress.MatrixRow;
            }
        }


        public SwitchMatrixItemViewModel(SwitchViewModel switchViewModel)
        {
            _switch = switchViewModel;
        }

        public void PressSwitch()
        {
            Switch.ActivateDeviceState();
        }
    

    }
}
