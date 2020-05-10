using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Compiler.Input;
using System.IO;

namespace Compiler.Config
{
    public class ConfigFileLoader
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
            } catch (Newtonsoft.Json.JsonReaderException e) {
                throw new ArgumentException("Invalid JSON in " + file.GetPath() + ": " + e.Message);
            }

            // Validate the file
            if (!ConfigFileValidator.ConfigFileValid(config))
            {
                string message = String.Format(
                    "Invalid format in {0}: {1}",
                    file.GetPath(),
                    ConfigFileValidator.lastError
                );
                throw new ArgumentException(message);
            }

            // Transform it to create full paths
            foreach (var item in config)
            {
                JArray fileArray = (JArray)item.Value;
                for (int key = 0; key < fileArray.Count; key++)
                {
                    fileArray[key] = Path.GetFullPath(file.DirectoryLocation() + "\\" + fileArray[key]);
                }
            }

            return config;
        }
    }
}
