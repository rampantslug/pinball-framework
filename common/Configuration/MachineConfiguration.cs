using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Devices;

namespace Configuration
{
    public class MachineConfiguration : IMachineConfiguration
    {
        public string ServerName { get; set; }
        public string ServerIcon { get; set; }
        public bool UseHardware { get; set; }

        public string PlayfieldImage { get; set; }

        public List<Switch> Switches { get; set; }
        public List<Coil> Coils { get; set; }
        public List<Servo> Servos { get; set; }
        public List<StepperMotor> StepperMotors { get; set; }
        public List<DCMotor> DCMotors { get; set; }
        public List<Led> Leds { get; set; }

        public List<Mode> Modes { get; set; }

        public string MediaBaseFileLocation { get; set; }
        public List<string> Images { get; set; }
        public List<string> Videos { get; set; }
        public List<string> Sounds { get; set; }


        /// <summary>
        /// Creates a new Configuration object and initializes all subconfiguration objects
        /// </summary>
        public MachineConfiguration()
        {
            Switches = new List<Switch>();
            Coils = new List<Coil>();
            DCMotors = new List<DCMotor>();
            StepperMotors = new List<StepperMotor>();
            Servos = new List<Servo>();
            Leds = new List<Led>();

            Modes = new List<Mode>();
            Images = new List<string>();
            Videos = new List<string>();
            Sounds = new List<string>();
        }

        public void ImageSerialize()
        {
            //TODO: Would create circular reference. Move ImageConversion to Configuration?
          //  var blobData = ImageConversion.ConvertImageFileToString("Configuration/playfield.png");
          //  PlayfieldImage = blobData;
        }
        
    }
}
