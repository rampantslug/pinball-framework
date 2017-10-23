namespace Configuration
{
    public interface IRsConfiguration
    {
        string FilePath { get; }

        IMachineConfiguration MachineConfiguration { get; }

        void WriteMachineToFile();

        /// <summary>
        /// Convert the entire Configuration to Json code
        /// </summary>
        /// <returns>Pretty formatted Json code</returns>
        //string ToJson();


        /// <summary>
        /// Convert the entire Configuration to Json code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        //void ToFile(string filename);

        /// <summary>
        /// Convert the entire Configuration to Xml code and save to a file
        /// </summary>
        /// <param name="filename">The filename to save to</param>
        //void SaveAsXml(string filename);


        
    }
}