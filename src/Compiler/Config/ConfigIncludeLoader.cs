using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Exception;
using Compiler.Input.Rule;
using Compiler.Input.Sorter;
using Compiler.Output;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigIncludeLoader
    {
        private readonly FolderInclusionRuleLoaderFactory folderFactory;
        private readonly FileInclusionRuleLoaderFactory fileFactory;

        public ConfigIncludeLoader(
            FolderInclusionRuleLoaderFactory folderFactory,
            FileInclusionRuleLoaderFactory fileFactory
        )
        {
            this.folderFactory = folderFactory;
            this.fileFactory = fileFactory;
        }

        public void LoadConfig(
            ConfigInclusionRules inclusionRules,
            JObject jsonConfig,
            string fileName
        )
        {
            // Load airport data
            JToken airportData = jsonConfig.SelectToken("includes.airports");
            if (airportData != null)
            {
                IterateAirportConfig(airportData, inclusionRules, fileName);
            }

            // Load enroute data
            JToken enrouteData = jsonConfig.SelectToken("includes.enroute");
            if (enrouteData != null)
            {
                IterateConfigFileSections(
                    enrouteData,
                    EnrouteConfigFileSections.ConfigFileSections,
                    OutputGroupFactory.CreateEnroute,
                    inclusionRules.AddEnrouteInclusionRule,
                    GetFolderForConfigFile(fileName),
                    "enroute"
                );
            }

            // Load misc data
            JToken miscData = jsonConfig.SelectToken("includes.misc");
            if (miscData != null)
            {
                IterateConfigFileSections(
                    miscData,
                    MiscConfigFileSections.ConfigFileSections,
                    OutputGroupFactory.CreateMisc,
                    inclusionRules.AddMiscInclusionRule,
                    GetFolderForConfigFile(fileName),
                    "misc"
                );
            }
        }

        private string GetMissingTypeMessage(string section)
        {
            return $"Invalid type field for section {section} - must be \"files\" or \"folders\"";
        }

        private string GetInvalidConfigParentSectionFormatMessage(string section)
        {
            return $"Invalid config section for {section} - must be an object or array of objects";
        }

        /**
         * Airports are special as there are many folders to go through for things and in some
         * cases we might want to exclude certain files from airports. So iterate through
         * each configuration of airports.
         */
        private void IterateAirportConfig(
            JToken airportConfig,
            ConfigInclusionRules config,
            string configFilePath
        )
        {
            if (airportConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(
                    $"Invalid airport config in {configFilePath} must be an object"
                );
            }

            foreach (KeyValuePair<string, JToken> configItem in (JObject)airportConfig)
            {
                if (configItem.Value.Type != JTokenType.Object)
                {
                    throw new ConfigFileInvalidException(
                        $"Invalid airport config[{configItem.Key}] in {configFilePath} must be an object"
                    );
                }

                // Get the airport folders
                string configFileFolder = GetFolderForConfigFile(configFilePath);
                var directories = Directory
                    .GetDirectories(configFileFolder + Path.DirectorySeparatorChar + configItem.Key)
                    .OrderBy(directory => directory, new AlphabeticalPathSorter()).ToList();

                /*
                 * Airports are a bit special as there are lots of them.
                 * Rather than cycling each airport and applying all the rules, cycle the rules and then apply the
                 * airports, that way we maintain any desired ordering.
                 */
                foreach (ConfigFileSection configSection in AirfieldConfigFileSections.ConfigFileSections)
                {
                    JToken configObjectSection = configItem.Value.SelectToken(configSection.JsonPath);
                    if (configObjectSection == null)
                    {
                        continue;
                    }


                    if (configObjectSection.Type == JTokenType.Array)
                    {
                        foreach (JToken token in (JArray)configObjectSection)
                        {
                            foreach (var directory in directories)
                            {
                                ProcessConfigSectionObject(
                                    token,
                                    configSection,
                                    OutputGroupFactory.CreateAirport(configSection, Path.GetFileName(directory)),
                                    config.AddAirportInclusionRule,
                                    directory,
                                    "airport"
                                );
                            }
                        }
                    }
                    else
                    {
                        foreach (var directory in directories)
                        {
                            ProcessConfigSectionObject(
                                configObjectSection,
                                configSection,
                                OutputGroupFactory.CreateAirport(configSection, Path.GetFileName(directory)),
                                config.AddAirportInclusionRule,
                                directory,
                                "airport"
                            );
                        }
                    }
                }
            }
        }

        private void IterateConfigFileSections(
            JToken jsonConfig,
            IEnumerable<ConfigFileSection> configFileSections,
            Func<ConfigFileSection, OutputGroup> createOutputGroup,
            Action<IInclusionRule> addInclusionRule,
            string configFilePath,
            string sectionRootString
        )
        {
            foreach (ConfigFileSection configSection in configFileSections)
            {
                JToken configObjectSection = jsonConfig.SelectToken(configSection.JsonPath);
                if (configObjectSection == null)
                {
                    continue;
                }

                LoadConfigSection(
                    configObjectSection,
                    configSection,
                    createOutputGroup(configSection),
                    addInclusionRule,
                    configFilePath,
                    sectionRootString
                );
            }
        }

        /*
         * Process a section of config, iterating if it's an array.
         */
        private void LoadConfigSection(
            JToken jsonConfig,
            ConfigFileSection configFileSection,
            OutputGroup outputGroup,
            Action<IInclusionRule> addInclusionRule,
            string configFilePath,
            string sectionRootString
        )
        {
            if (jsonConfig.Type == JTokenType.Array)
            {
                foreach (JToken token in (JArray)jsonConfig)
                {
                    ProcessConfigSectionObject(
                        token,
                        configFileSection,
                        outputGroup,
                        addInclusionRule,
                        configFilePath,
                        sectionRootString
                    );
                }
            }
            else
            {
                ProcessConfigSectionObject(
                    jsonConfig,
                    configFileSection,
                    outputGroup,
                    addInclusionRule,
                    configFilePath,
                    sectionRootString
                );
            }
        }

        private void ProcessConfigSectionObject(
            JToken jsonConfig,
            ConfigFileSection configFileSection,
            OutputGroup outputGroup,
            Action<IInclusionRule> addInclusionRule,
            string rootPath,
            string sectionRootString
        )
        {
            if (jsonConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(GetInvalidConfigParentSectionFormatMessage(sectionRootString));
            }

            JObject configObject = (JObject)jsonConfig;

            // Check the type field to see what we're dealing with
            if (
                !configObject.TryGetValue("type", out var typeToken) ||
                typeToken.Type != JTokenType.String ||
                ((string)typeToken != "files" && (string)typeToken != "folder")
            )
            {
                throw new ConfigFileInvalidException(
                    GetMissingTypeMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            if ((string)typeToken == "files")
            {
                addInclusionRule(
                    fileFactory.Create(
                        new ConfigPath(sectionRootString, configFileSection),
                        outputGroup,
                        rootPath
                    ).CreateRule(configObject)
                );
            }
            else
            {
                addInclusionRule(
                    folderFactory.Create(
                        new ConfigPath(sectionRootString, configFileSection),
                        outputGroup,
                        rootPath
                    ).CreateRule(configObject)
                );
            }
        }

        private string GetFolderForConfigFile(string pathToConfigFile)
        {
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile) ?? string.Empty);
        }
    }
}
