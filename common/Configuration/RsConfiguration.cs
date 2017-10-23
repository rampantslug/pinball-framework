using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Configuration
{
    /// <summary>
    /// Contains the required configuration information for a pinball machine.
    /// </summary>
    public class RsConfiguration : IRsConfiguration
    {
        public string FilePath { get; private set; }

        public IMachineConfiguration MachineConfiguration { get; private set; }

        // TODO: Add ledshows etc...

        public RsConfiguration(string machineConfigFilePath)
        {
            if (!string.IsNullOrEmpty(machineConfigFilePath))
            {
                FilePath = machineConfigFilePath;

                // Attempt to initialise MachineConfiguration from filePath
                MachineConfiguration = FromFile<MachineConfiguration>(FilePath);
                MachineConfiguration.ImageSerialize();
            }
        }


        /// <summary>
        /// Initialize configuration from a string of Json code
        /// </summary>
        /// <param name="json">Json serialized Configuration data</param>
        /// <returns>A deserialized Configuration object</returns>
        public static T FromJson<T>(string json)
        {
            var configuration = JsonConvert.DeserializeObject<T>(json);
            return configuration;
        }

        public void WriteMachineToFile()
        {
            ToFile(FilePath, MachineConfiguration);
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
        /// <param name="pathToFile">The file to deserialize</param>
        /// <returns>A MachineConfiguration object deserialized from the specified Json file</returns>
        public static T FromFile<T>(string pathToFile)
        {
            var streamReader = new StreamReader(pathToFile);
            var text = streamReader.ReadToEnd();
            streamReader.Close();
            return FromJson<T>(text);
        }

        /// <summary>
        /// Convert the entire Configuration to Json code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        private void ToFile(string filename, object configuration)
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new RsContractResolver() // Use contract resolver to exclude certain properties
            };
            using (var sw = new StreamWriter(filename))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, configuration);
            }
        }

        /// <summary>
        /// Convert the entire Configuration to Xml code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        public void SaveAsXml(string filename, object configuration)
        {
            var serializer = new XmlSerializer(typeof(IRsConfiguration));
            var textWriter = new StreamWriter(filename, false);
            serializer.Serialize(textWriter, configuration);
            textWriter.Close();
        }

        
    }
}
