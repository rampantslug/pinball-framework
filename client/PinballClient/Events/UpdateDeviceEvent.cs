using System;
using System.Windows.Media;
using BusinessObjects.Devices;
using PinballClient.CommonViewModels;

namespace PinballClient.Events
{
    /// <summary>
    /// Event that notifies of an updated Switch
    /// </summary>
    public class UpdateSwitchEvent
    {
        /// <summary>
        /// Timestamp the update occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Device with updated values
        /// </summary>
        public Switch Device { get; set; } 
    }

    /// <summary>
    /// Event that notifies of an updated Coil
    /// </summary>
    public class UpdateCoilEvent
    {
        /// <summary>
        /// Timestamp the update occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Device with updated values
        /// </summary>
        public Coil Device { get; set; }
    }

    /// <summary>
    /// Event that notifies of an updated Stepper Motor
    /// </summary>
    public class UpdateStepperMotorEvent
    {
        /// <summary>
        /// Timestamp the update occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Device with updated values
        /// </summary>
        public StepperMotor Device { get; set; }
    }

    /// <summary>
    /// Event that notifies of an updated Servo
    /// </summary>
    public class UpdateServoEvent
    {
        /// <summary>
        /// Timestamp the update occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Device with updated values
        /// </summary>
        public Servo Device { get; set; }
    }

    /// <summary>
    /// Event that notifies of an updated Led
    /// </summary>
    public class UpdateLedEvent
    {
        /// <summary>
        /// Timestamp the update occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Device with updated values
        /// </summary>
        public Led Device { get; set; }
    }
}
