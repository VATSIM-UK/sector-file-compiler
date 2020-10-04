using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Config
{
    /*
     * Represents a valid section of config file
     */
    public struct ConfigFileSection
    {
        public ConfigFileSection(string jsonPath, InputDataType dataType)
        {
            this.JsonPath = jsonPath;
            this.DataType = dataType;
        }


        /*
         * The path in the JSON to the section
         */
        public string JsonPath { get; }

        /*
         * The type of data contained within this config file section
         */
        public InputDataType DataType { get; }
    }
}
