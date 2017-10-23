using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to notifiy UI to show configuration of a Switch
    /// </summary>
    public class ShowSwitchConfigEvent
    {  
        /// <summary>
        /// ViewModel of the Switch to display
        /// </summary>
        public SwitchViewModel SwitchVm { get; set; }
    }

    /// <summary>
    /// Event to notifiy UI to show configuration of a Coil
    /// </summary>
    public class ShowCoilConfigEvent
    {
        /// <summary>
        /// ViewModel of the Coil to display
        /// </summary>
        public CoilViewModel CoilVm { get; set; }
    }

    /// <summary>
    /// Event to notifiy UI to show configuration of a Stepper Motor
    /// </summary>
    public class ShowStepperMotorConfigEvent
    {
        /// <summary>
        /// ViewModel of the Stepper Motor to display
        /// </summary>
        public StepperMotorViewModel StepperMotorVm { get; set; }
    }

    /// <summary>
    /// Event to notifiy UI to show configuration of a Servo
    /// </summary>
    public class ShowServoConfigEvent
    {
        /// <summary>
        /// ViewModel of the Servo to display
        /// </summary>
        public ServoViewModel ServoVm { get; set; }
    }

    /// <summary>
    /// Event to notifiy UI to show configuration of a Led
    /// </summary>
    public class ShowLedConfigEvent
    {
        /// <summary>
        /// ViewModel of the Led to display
        /// </summary>
        public LedViewModel LedVm { get; set; }
    }
}
