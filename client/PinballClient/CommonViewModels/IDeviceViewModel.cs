using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Hardware.DeviceAddress;

namespace PinballClient.CommonViewModels
{
    public interface IDeviceViewModel
    {

        ushort Number { get; }
        string Name { get; set; }
        IDevice Device { get; set; }
        string State { get; }
        IAddress Address { get; set; }

        void ConfigureDevice();

        //void HighlightSelected();

        

        string RefinedType { get; set; }

        ObservableCollection<HistoryRowViewModel> PreviousStates { get; }
        
        void Save();
        void Remove();
    }
}
