using System;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class AllSwitchesVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    public class AllCoilsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    public class AllStepperMotorsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    public class AllServosVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }

    public class AllLedsVisibilityEvent
    {
        public bool IsVisible { get; set; }
    }
}
