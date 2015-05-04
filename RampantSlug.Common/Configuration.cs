using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Newtonsoft.Json;
using RampantSlug.Common.Devices;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Serialization;

namespace RampantSlug.Common
{
    public class Configuration
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
        }

        public void ImageSerialize()
        {
            var blobData = ImageConversion.ConvertImageFileToString("Configuration/playfield-geometric.png");
            PlayfieldImage = blobData;
        }





        /// <summary>
        /// Initialize configuration from a string of Json code
        /// </summary>
        /// <param name="json">Json serialized Configuration data</param>
        /// <returns>A deserialized Configuration object</returns>
        public static Configuration FromJson(string json)
        {
            var configuration = JsonConvert.DeserializeObject<Configuration>(json);
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
            serializer.ContractResolver = new RsContractResolver();
 
            using (var sw = new StreamWriter(PathToFile))
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

    public class RsContractResolver : DefaultContractResolver
 {
        public new static readonly RsContractResolver Instance = new RsContractResolver();
 
     protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
     {
         JsonProperty property = base.CreateProperty(member, memberSerialization);

         if (property.PropertyName == "Number"
             || property.PropertyName == "LastChangeTimeStamp" 
             || property.PropertyName == "PlayfieldImage"
             || property.PropertyName == "State"
             || property.PropertyName == "IsActive")
        {
            property.ShouldSerialize =
                instance => false;
        }

        return property;
    }
}
}
