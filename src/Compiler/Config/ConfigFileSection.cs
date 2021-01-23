using Compiler.Input;

namespace Compiler.Config
{
    /*
     * Represents a valid section of config file
     */
    public struct ConfigFileSection
    {
        public ConfigFileSection(string jsonPath, InputDataType dataType, string outputGroupDescriptor = null)
        {
            this.JsonPath = jsonPath;
            this.DataType = dataType;
            this.OutputGroupDescriptor = outputGroupDescriptor;
        }

        /*
         * The path in the JSON to the section
         */
        public string JsonPath { get; }

        /*
         * The type of data contained within this config file section
         */
        public InputDataType DataType { get; }

        /*
         * A string that will identify what kind of data is in this section
         * - e.g. Geo, Regions
         */
        public string OutputGroupDescriptor { get; }
    }
}
