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

        private readonly OutputGroupRepository outputGroupRepository;

        public ConfigFileLoader(OutputGroupRepository outputGroupRepository)
        {
            this.outputGroupRepository = outputGroupRepository;
        }

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
                    if (airportData.Type != JTokenType.Array)
                    {
                        throw new ConfigFileInvalidException(
                            string.Format("Invalid airport config in {0}... must be an array", file)
                        );
                    }

                    int airportConfigIndex = 0;
                    foreach(JToken airportConfig in airportData)
                    {
                        if (airportConfig.Type != JTokenType.Object)
                        {
                            throw new ConfigFileInvalidException(
                                string.Format("Invalid airport config[{0}] in {1}... must be an array", airportConfigIndex, file)
                            );
                        }
                        airportConfigIndex++;
                        this.LoadAirportConfig(airportData, config, file);
                    }
                }

                // Load enroute data
                JToken enrouteData = jsonConfig.SelectToken("enroute");
                if (enrouteData != null)
                {
                    this.LoadEnrouteConfig(enrouteData, config, file);
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
            if (section.Type != JTokenType.Array && section.Type != JTokenType.String)
            {
                return false;
            }

            if (section.Type == JTokenType.String)
            {
                return true;
            }

            foreach (JToken item in section)
            {
                if (item.Type != JTokenType.String)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidConfigSectionSubitemFormat(JToken section)
        {
            return section.Type == JTokenType.String;
        }

        private void LoadMiscConfig(JToken jsonConfig, ConfigFileList configFile, string configFilePath)
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

                OutputGroup outputGroup = OutputGroupFactory.CreateMisc(configSection);
                if (configObjectSection.Type == JTokenType.String)
                {
                    outputGroup.AddFile(this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>()));
                    configFile.AddFile(
                        SectorDataFileFactory.Create(
                            this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>()),
                            configSection.DataType
                        )
                    );
                } else
                {
                    foreach (JToken item in configObjectSection)
                    {
                        outputGroup.AddFile(this.NormaliseFilePath(configFilePath, item.Value<string>()));
                        configFile.AddFile(
                            SectorDataFileFactory.Create(
                                this.NormaliseFilePath(configFilePath, item.Value<string>()),
                                configSection.DataType
                            )
                        );
                    }
                }
            }
        }

        private void LoadEnrouteConfig(JToken jsonConfig, ConfigFileList configFile, string configFilePath)
        {
            if (jsonConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(GetInvalidConfigParentSectionFormatMessage("enroute"));
            }

            JObject configObject = (JObject)jsonConfig;
            foreach (ConfigFileSection configSection in EnrouteConfigFileSections.configFileSections)
            {
                JToken configObjectSection = configObject.SelectToken(configSection.JsonPath);
                if (configObjectSection == null)
                {
                    continue;
                }

                if (!IsValidConfigSectionFormat(configObjectSection))
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidConfigSectionFormatMessage("enroute." + configSection.JsonPath)
                    );
                }

                OutputGroup outputGroup = OutputGroupFactory.CreateEnroute(configSection);
                if (configObjectSection.Type == JTokenType.String)
                {
                    outputGroup.AddFile(this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>()));
                    configFile.AddFile(
                        SectorDataFileFactory.Create(
                            this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>()),
                            configSection.DataType
                        )
                    );
                }
                else
                {
                    foreach (JToken item in configObjectSection)
                    {
                        outputGroup.AddFile(this.NormaliseFilePath(configFilePath, item.Value<string>()));
                        configFile.AddFile(
                            SectorDataFileFactory.Create(
                                this.NormaliseFilePath(configFilePath, item.Value<string>()),
                                configSection.DataType
                            )
                        );
                    }
                }
            }
        }

        private void LoadAirportConfig(JToken jsonConfig, ConfigFileList configFile, string configFilePath)
        {
            if (jsonConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(GetInvalidConfigParentSectionFormatMessage("airport"));
            }

            JObject configObject = (JObject)jsonConfig;
            foreach (ConfigFileSection configSection in EnrouteConfigFileSections.configFileSections)
            {
                JToken configObjectSection = configObject.SelectToken(configSection.JsonPath);
                if (configObjectSection == null)
                {
                    continue;
                }

                if (!IsValidConfigSectionFormat(configObjectSection))
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidConfigSectionFormatMessage("airport." + configSection.JsonPath)
                    );
                }

                // Airport files may not exist, so we check for them
                OutputGroup outputGroup = OutputGroupFactory.CreateEnroute(configSection);
                if (configObjectSection.Type == JTokenType.String)
                {
                    string filePath = this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>());
                    if (!File.Exists(filePath))
                    {
                        continue;
                    }
                    outputGroup.AddFile(filePath);
                    configFile.AddFile(
                        SectorDataFileFactory.Create(
                            filePath,
                            configSection.DataType
                        )
                    );
                }
                else
                {
                    foreach (JToken item in configObjectSection)
                    {
                        string filePath = this.NormaliseFilePath(configFilePath, configObjectSection.Value<string>());
                        if (!File.Exists(filePath))
                        {
                            continue;
                        }
                        outputGroup.AddFile(filePath);
                        configFile.AddFile(
                            SectorDataFileFactory.Create(
                                filePath,
                                configSection.DataType
                            )
                        );
                    }
                }
            }
        }

        private string NormaliseFilePath(string pathToConfigFile, string relativeInputFilePath)
        {
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile) + Path.DirectorySeparatorChar + relativeInputFilePath);
        }
    }
}
