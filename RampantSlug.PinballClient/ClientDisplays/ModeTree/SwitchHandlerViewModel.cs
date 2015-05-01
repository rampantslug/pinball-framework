using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.ModeTree
{
    public class SwitchHandlerViewModel : Screen
    {
        private string _switchHandlerName;
        private string _associatedSwitch;
        private ushort _associatedSwitchId;

        public SwitchHandlerViewModel(string switchHandlerName, ushort associatedSwitchId, string associatedSwitch)
        {
            _switchHandlerName = switchHandlerName;
            _associatedSwitchId = associatedSwitchId;
            _associatedSwitch = associatedSwitch;
        }

        public string SwitchHandlerName
        {
            get { return _switchHandlerName; }
        }

        public string AssociatedSwitch
        {
            get
            {
                return _associatedSwitch;
            }
            set
            {
                value = _associatedSwitch;
                NotifyOfPropertyChange(()=> AssociatedSwitch);
            }
        }

        public ushort AssociatedSwitchId
        {
            get
            {
                return _associatedSwitchId;
            }
            set
            {
                value = _associatedSwitchId;
                NotifyOfPropertyChange(() => AssociatedSwitchId);
            }
        }
      
    }
}

