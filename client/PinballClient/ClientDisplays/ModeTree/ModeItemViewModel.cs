using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace PinballClient.ClientDisplays.ModeTree
{
    public class ModeItemViewModel : Screen
    {

        public string ModeName { get; }

        public ObservableCollection<ModeEventDeviceViewModel> Children { get; }

        public ObservableCollection<ModeEventViewModel> ModeEvents { get; }

        public ObservableCollection<ModeRequiredDeviceViewModel> RequiredDevices { get; }

        public ObservableCollection<ModeMediaResourceViewModel> MediaResources { get; }


        public ModeItemViewModel(string modeName, 
            ObservableCollection<ModeEventViewModel> modeEvents, 
            ObservableCollection<ModeRequiredDeviceViewModel> requiredDevices, 
            ObservableCollection<ModeMediaResourceViewModel> mediaResources)
        {
            ModeName = modeName;

            ModeEvents = modeEvents;
            RequiredDevices = requiredDevices;
            MediaResources = mediaResources;

            Children = new ObservableCollection<ModeEventDeviceViewModel>();
        }
    }
}

