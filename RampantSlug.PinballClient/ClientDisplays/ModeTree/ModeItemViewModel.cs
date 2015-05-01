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
    public class ModeItemViewModel 
    {
        private readonly string _modeName;
        private ObservableCollection<SwitchHandlerViewModel> _children;

        public ModeItemViewModel(string modeName, ObservableCollection<SwitchHandlerViewModel> switchHandlers)
        {
            _modeName = modeName;
            _children = new ObservableCollection<SwitchHandlerViewModel>();

            foreach (var switchHandler in switchHandlers)
            {
                Children.Add(switchHandler);
            }
        }

        public string ModeName
        {
            get { return _modeName; }
        }

        public ObservableCollection<SwitchHandlerViewModel> Children
        {
            get { return _children; }
        }
        
    }
}

