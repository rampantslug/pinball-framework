using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to notify UI to Highlight/Select/Make Active this Switch
    /// </summary>
    public class HighlightSwitchEvent
    {
        /// <summary>
        /// ViewModel of the Switch to highlight
        /// </summary>
        public SwitchViewModel SwitchVm { get; set; }
    }

    /// <summary>
    /// Event to notify UI to Highlight/Select/Make Active this Coil
    /// </summary>
    public class HighlightCoilEvent
    {
        /// <summary>
        /// ViewModel of the Coil to highlight
        /// </summary>
        public CoilViewModel CoilVm { get; set; }
    }

    /// <summary>
    /// Event to notify UI to Highlight/Select/Make Active this Stepper Motor
    /// </summary>
    public class HighlightStepperMotorEvent
    {
        /// <summary>
        /// ViewModel of the Stepper Motor to highlight
        /// </summary>
        public StepperMotorViewModel StepperMotorVm { get; set; }
    }

    /// <summary>
    /// Event to notify UI to Highlight/Select/Make Active this Servo
    /// </summary>
    public class HighlightServoEvent
    {
        /// <summary>
        /// ViewModel of the Servo to highlight
        /// </summary>
        public ServoViewModel ServoVm { get; set; }
    }

    /// <summary>
    /// Event to notify UI to Highlight/Select/Make Active this Led
    /// </summary>
    public class HighlightLedEvent
    {
        /// <summary>
        /// ViewModel of the Led to highlight
        /// </summary>
        public LedViewModel LedVm { get; set; }
    }
}
