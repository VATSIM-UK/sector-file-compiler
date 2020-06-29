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
                    Path.GetFullPath(file.GetPath()),
                    ConfigFileValidator.LastError
                );
                throw new ArgumentException(message);
            }

            // Transform it to create full paths
            foreach (var item in config)
            {
                if (item.Value.Type == JTokenType.Object)
                {
                    NormaliseSubsections(file, (JObject)item.Value);
                } else
                {
                    NormaliseFileArray(file, (JArray)item.Value);
                }
            }

            return config;
        }

        private static void NormaliseFileArray(IFileInterface file, JArray fileArray)
        {
            for (int key = 0; key < fileArray.Count; key++)
            {
                fileArray[key] = Path.GetFullPath(file.DirectoryLocation() + Path.DirectorySeparatorChar + fileArray[key]);
            }
        }

        private static void NormaliseSubsections(IFileInterface file, JObject config)
        {
            foreach (var item in config)
            {
                NormaliseFileArray(file, (JArray)item.Value);
            }
        }
    }
}
