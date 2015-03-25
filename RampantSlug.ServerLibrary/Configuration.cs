using Newtonsoft.Json;
using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RampantSlug.ServerLibrary
{
    class Configuration
    {

        public List<Switch> Switches { get; set; }
        public List<Coil> Coils { get; set; }


         /// <summary>
        /// Creates a new Configuration object and initializes all subconfiguration objects
        /// </summary>
        public Configuration()
        {
            Switches = new List<Switch>();
            Coils = new List<Coil>();
  
        }




        /// <summary>
        /// Initialize configuration from a string of Json code
        /// </summary>
        /// <param name="json">Json serialized Configuration data</param>
        /// <returns>A deserialized Configuration object</returns>
        public static Configuration FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Configuration>(json);
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
