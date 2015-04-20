using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using RampantSlug.Common;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient {
    public interface IShell 
    {
        ObservableCollection<SwitchViewModel> Switches { get; set; }

        ObservableCollection<CoilViewModel> Coils { get; set; }

        ObservableCollection<StepperMotorViewModel> StepperMotors { get; set; }

        ObservableCollection<ServoViewModel> Servos { get; set; }

        ObservableCollection<LedViewModel> Leds { get; set; }
        
        string PlayfieldImage { get; set; }   

        void UpdateViewModels(Configuration configuration);
    }
}