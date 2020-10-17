using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Compiler.Input;
using System.IO;
using Compiler.Exception;
using Compiler.Output;

namespace Compiler.Config
{
    public class ConfigFileLoader
    {
        /**
         * Load config files and append roots as required.
         */
        public ConfigFileList LoadConfigFiles(List<string> files)
        {
            ConfigFileList config = new ConfigFileList();

            foreach (string file in files)
            {
                // Parse the config file as JSON
                JObject jsonConfig;
                try
                {
                    jsonConfig = JObject.Parse(File.ReadAllText(file));
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    throw new ConfigFileInvalidException("Invalid JSON in " + file + ": " + e.Message);
                }

                // Load airport data
                JToken airportData = jsonConfig.SelectToken("airports");
                if (airportData != null)
                {

                }

                // Load enroute data
                JToken enrouteData = jsonConfig.SelectToken("enroute");
                if (enrouteData != null)
                {

                }

                // Load misc data
                JToken miscData = jsonConfig.SelectToken("misc");
                if (miscData != null)
                {
                    this.LoadMiscConfig(miscData, config, file);
                }

            }

            return config;
        }

        private string GetInvalidConfigSectionFormatMessage(string section)
        {
            return String.Format(
                "Invalid config section for {0} - must be the path to a file, or an array of paths",
                section
            );
        }

        private string GetInvalidConfigParentSectionFormatMessage(string section)
        {
            return String.Format(
                "Invalid config section for {0} - must be an object",
                section
            );
        }

        private bool IsValidConfigSectionFormat(JToken section)
        {
            return section.Type == JTokenType.Array || section.Type == JTokenType.String;
        }

        private void LoadMiscConfig(JToken jsonConfig, ConfigFileList configFile, string pathRoot)
        {
            if (jsonConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(GetInvalidConfigParentSectionFormatMessage("misc"));
            }

            JObject configObject = (JObject)jsonConfig;
            foreach (ConfigFileSection configSection in MiscConfigFileSections.configFileSections)
            {
                JToken configObjectSection = configObject.SelectToken(configSection.JsonPath);
                if (configObjectSection == null)
                {
                    continue;
                }

                if (!IsValidConfigSectionFormat(configObjectSection))
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidConfigSectionFormatMessage("misc." + configSection.JsonPath)
                    );
                }

                if (configObjectSection.Type == JTokenType.String)
                {
                    configFile.AddFile(
                        SectorDataFileFactory.Create(
                            configObjectSection.Value<string>(),
                            configSection.DataType,
                            string.Format("Start of misc {0}", configSection.
                        )
                    )
                }

                // Create the new group for the data
                AbstractOutputGroup group;
                if (configSection.OutputGroupDescriptor == null)
                {
                    group = new NullOutputGroup(configSection.DataType);
                } else
                {
                    group = new MiscOutputGroup(configSection.DataType, configSection.OutputGroupDescriptor);
          0      }
                this.outputGroupRepository.AddGroup(group);

                // Create the files, attach the files to the group

            }

            // Parse the sections
            foreach (var item in config)
            {
                ConfigSectionsMisc configSection = ConfigSectionsMiscMapper.GetSectionForConfigKey(item.Key);
                if (configSection == ConfigSectionsMisc.INVALID)
                {
                    throw new ConfigFileInvalidException("Invalid misc section " + item.Key);
                }

                // Normalise any files, parse arrays if required and return a file index
            }
        }

        private void LoadEnrouteConfig(JObject config)
        {
            // Parse the sections
            foreach (var item in config)
            {
                ConfigSectionsEnroute configSection = ConfigSectionsEnrouteMapper.GetSectionForConfigKey(item.Key);
                if (configSection == ConfigSectionsEnroute.INVALID)
                {
                    throw new ConfigFileInvalidException("Invalid enroute section " + item.Key);
                }

                // Normalise any files, parse arrays if required and return a file index
            }
        }

        private void LoadAirportConfig(JObject config)
        {
            // Parse the sections
            foreach (var item in config)
            {
                ConfigSectionsAirport configSection = ConfigSectionsAirportMapper.GetSectionForConfigKey(item.Key);
                if (configSection == ConfigSectionsAirport.INVALID)
                {
                    throw new ConfigFileInvalidException("Invalid airport section " + item.Key);
                }

                // Split off for SMR/Ground Map as required

                // Normalise any files, parse arrays if required and return a file index
            }
        }

        //private static void NormaliseFileArray(IFileInterface file, JArray fileArray)
        //{
        //    for (int key = 0; key < fileArray.Count; key++)
        //    {
        //        fileArray[key] = Path.GetFullPath(file.DirectoryLocation() + Path.DirectorySeparatorChar + fileArray[key]);
        //    }
        //}

        private string NormaliseFilePath(string pathToConfigFile, string relativeInputFilePath)
        {
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile) + Path.DirectorySeparatorChar + relativeInputFilePath);
        }
    }
}
