using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using RampantSlug.Common.Devices;
using System.Windows.Media.Imaging;

namespace RampantSlug.Common
{
    public class Configuration
    {
        //[JsonIgnore]
        public string PlayfieldImage { get; set; }

        public List<Switch> Switches { get; set; }
        public List<Coil> Coils { get; set; }
        public List<Servo> Servos { get; set; }
        public List<StepperMotor> StepperMotors { get; set; }
        public List<DCMotor> DCMotors { get; set; }
        public List<Led> Leds { get; set; }



         /// <summary>
        /// Creates a new Configuration object and initializes all subconfiguration objects
        /// </summary>
        public Configuration()
        {
            Switches = new List<Switch>();
            Coils = new List<Coil>();
            DCMotors = new List<DCMotor>();
            StepperMotors = new List<StepperMotor>();
            Servos = new List<Servo>();
            Leds = new List<Led>();


         /*   PlayfieldImage = new BitmapImage();
            PlayfieldImage.BeginInit();
            PlayfieldImage.UriSource = new Uri("Configuration/playfield.png", UriKind.Relative);
            PlayfieldImage.EndInit();*/
            // PlayfieldImage.Source = logo;
        }

        private void ImageSerialize()
        {
            var blobData = ImageConversion.ConvertImageFileToString("Configuration/playfield.png");
            PlayfieldImage = blobData;
            /*using (MemoryStream ms = new MemoryStream())
            {
                // This is a BitmapImage fetched from a dictionary.
                BitmapImage image = new BitmapImage(new Uri("Configuration/playfield.png", UriKind.Relative));

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(ms);
                ms.Flush();
               // byte[] buffer = ms.GetBuffer();

                // Here I'm adding the byte[] array to SerializationInfo
                PlayfieldImage = ms.ToArray();
            }*/
        }





        /// <summary>
        /// Initialize configuration from a string of Json code
        /// </summary>
        /// <param name="json">Json serialized Configuration data</param>
        /// <returns>A deserialized Configuration object</returns>
        public static Configuration FromJson(string json)
        {
            var configuration = JsonConvert.DeserializeObject<Configuration>(json);
            // TODO: Move this out to be specific to PinbalServerDemo location. As Configuration exists on client also...
            configuration.ImageSerialize();
            return configuration;
        }

        /// <summary>
        /// Convert the entire Configuration to Json code
        /// </summary>
        /// <returns>Pretty formatted Json code</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Initialize configuration from a Json file on disk
        /// </summary>
        /// <param name="PathToFile">The file to deserialize</param>
        /// <returns>A MachineConfiguration object deserialized from the specified Json file</returns>
        public static Configuration FromFile(string PathToFile)
        {
            StreamReader streamReader = new StreamReader(PathToFile);
            string text = streamReader.ReadToEnd();
            return FromJson(text);
        }

        /// <summary>
        /// Convert the entire Configuration to Json code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        public void ToFile(string PathToFile)
        {
            var serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
 
            using (var sw = new StreamWriter(PathToFile + @"\testmachine.json"))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }


        /// <summary>
        /// Convert the entire Configuration to Xml code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        public void SaveAsXml(string filename)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            var textWriter = new StreamWriter(filename, false);
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }
    }
}
