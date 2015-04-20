using System;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class AllSwitchesVisibility
    {
        public bool IsVisible { get; set; }
    }

    public class AllCoilsVisibility
    {
        public bool IsVisible { get; set; }
    }

    public class AllStepperMotorsVisibility
    {
        public bool IsVisible { get; set; }
    }

    public class AllServosVisibility
    {
        public bool IsVisible { get; set; }
    }

    public class AllLedsVisibility
    {
        public bool IsVisible { get; set; }
    }
}
