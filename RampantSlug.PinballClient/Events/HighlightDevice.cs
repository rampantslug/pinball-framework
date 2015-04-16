using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class HighlightSwitch
    {
        public SwitchViewModel SwitchVm { get; set; }
    }

    public class HighlightCoil
    {
        public CoilViewModel CoilVm { get; set; }
    }

    public class HighlightStepperMotor
    {
        public StepperMotorViewModel StepperMotorVm { get; set; }
    }

    public class HighlightServo
    {
        public ServoViewModel ServoVm { get; set; }
    }
}
