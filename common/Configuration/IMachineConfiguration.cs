using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Devices;

namespace Configuration
{
    public interface IMachineConfiguration
    {
        string ServerName { get; set; }
        string ServerIcon { get; set; }
        bool UseHardware { get; set; }
        string PlayfieldImage { get; set; }

        // Game Devices
        List<Switch> Switches { get; set; }
        List<Coil> Coils { get; set; }
        List<Servo> Servos { get; set; }
        List<StepperMotor> StepperMotors { get; set; }
        List<DCMotor> DCMotors { get; set; }
        List<Led> Leds { get; set; }
        List<Mode> Modes { get; set; }

        // Game Media
        string MediaBaseFileLocation { get; set; }
        List<string> Images { get; set; }
        List<string> Videos { get; set; }

        List<string> Sounds { get; set; }

        void ImageSerialize();


    }
}
