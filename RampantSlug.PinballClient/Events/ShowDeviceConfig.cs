using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class ShowSwitchConfig
    {       
        public SwitchViewModel SwitchVm { get; set; }
    }

    public class ShowCoilConfig
    {
        public CoilViewModel CoilVm { get; set; }
    }

    public class ShowStepperMotorConfig
    {
        public StepperMotorViewModel StepperMotorVm { get; set; }
    }

    public class ShowServoConfig
    {
        public ServoViewModel ServoVm { get; set; }
    }

    public class ShowLedConfig
    {
        public LedViewModel LedVm { get; set; }
    }
}
