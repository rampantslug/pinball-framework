using System.Collections.ObjectModel;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient {
    public interface IShell 
    {
        ObservableCollection<SwitchViewModel> Switches { get; set; }

        ObservableCollection<CoilViewModel> Coils { get; set; }

        ObservableCollection<StepperMotorViewModel> StepperMotors { get; set; }

        ObservableCollection<ServoViewModel> Servos { get; set; }
    }
}