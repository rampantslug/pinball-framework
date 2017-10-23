using System;
using System.Windows.Media;
using PinballClient.CommonViewModels;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to set visibility of ALL Switch devices
    /// </summary>
    public class AllSwitchesVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Event to set visibility of ALL Coil devices
    /// </summary>
    public class AllCoilsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Event to set visibility of ALL Stepper Motor devices
    /// </summary>
    public class AllStepperMotorsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Event to set visibility of ALL Servo devices
    /// </summary>
    public class AllServosVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Event to set visibility of ALL Led devices
    /// </summary>
    public class AllLedsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }
}
