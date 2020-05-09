using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Compiler.Input;

namespace Compiler.Config
{
    class ConfigFileLoader
    {
        /**
         * Load config files and append roots as required.
         */
        public static JObject LoadConfigFile(IFileInterface file)
        {
            // Parse the config file as JSON
            JObject config;
            try
            {
                config = JObject.Parse(file.Contents());
            } catch (Newtonsoft.Json.JsonReaderException) {
                throw new Exception("Config file was invalid JSON" + file.GetPath());
            }

            // Validate the file
            if (!ConfigFileValidator.ConfigFileValid(config))
            {
                throw new Exception("Config file was invalid " + file.GetPath());
            }

            // Transform it to create full paths
            foreach (var item in config)
            {
                JArray fileArray = (JArray)item.Value;
                for (int key = 0; key < fileArray.Count; key++)
                {
                    fileArray[key] = file.DirectoryLocation() + "\\" + fileArray[key];
                }
            }

            return config;
        }
    }
}
