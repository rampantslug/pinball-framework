using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Caliburn.Micro;
using Common;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Collection of Switch objects wrapped in View Model.
        /// </summary>
        ObservableCollection<SwitchViewModel> Switches { get; set; }

        /// <summary>
        /// Collection of Coil objects wrapped in View Model.
        /// </summary>
        ObservableCollection<CoilViewModel> Coils { get; set; }

        /// <summary>
        /// Collection of Stepper Motor objects wrapped in View Model.
        /// </summary>
        ObservableCollection<StepperMotorViewModel> StepperMotors { get; set; }

        /// <summary>
        /// Collection of Servo objects wrapped in View Model.
        /// </summary>
        ObservableCollection<ServoViewModel> Servos { get; set; }

        /// <summary>
        /// Collection of Leds objects wrapped in View Model.
        /// </summary>
        ObservableCollection<LedViewModel> Leds { get; set; }

        string PlayfieldImage { get; set; }

        List<Mode> Modes { get; set; }

        List<string> Images { get; set; }
        List<string> Videos { get; set; }
        List<string> Sounds { get; set; }
        IObservableCollection<LedShowViewModel> Shows { get; set; }
    }
}
