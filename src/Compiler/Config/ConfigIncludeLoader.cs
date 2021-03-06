﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Compiler.Exception;
using Compiler.Input;
using Compiler.Output;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class ConfigIncludeLoader
    {
        public void LoadConfig(
            ConfigInclusionRules inclusionRules,
            JObject jsonConfig,
            string fileName
        ) {
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
                    x => inclusionRules.AddEnrouteInclusionRule(x),
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
                    x => inclusionRules.AddMiscInclusionRule(x),
                    GetFolderForConfigFile(fileName),
                    "misc"
                );
            }
        }
        
        private string GetMissingTypeMessage(string section)
        {
            return $"Invalid type field for section {section} - must be \"files\" or \"folders\"";
        }

        private string InvalidFilesListMessage(string section)
        {
            return $"Files list invalid in section {section} - must be array under key \"files\"";
        }

        private string GetInvalidFolderMessage(string section)
        {
            return $"Folder invalid in section {section} - must be string under key \"folder\"";
        }
        
        private string GetInvalidPatternMessage(string section)
        {
            return $"Pattern invalid in section {section} - must be a regular expression string";
        }

        private string GetRecursiveMessage(string section)
        {
            return $"Recursive must be a boolean in section {section}";
        }

        private string GetIncludeAndExcludeMessage(string section)
        {
            return $"Cannot specify both include and exclude for folders in section {section}";
        }

        private string GetInvalidFilePathMessage(string section)
        {
            return $"Invalid file path in section {section} - must be a string";
        }

        private string GetInvalidIncludeExcludeListMessage(bool include, string section)
        {
            return $"{(include ? "Include" : "Exclude")} list must be an array in section {section}";
        }

        private string GetInvalidIncludeExcludeFileMessage(bool include, string section)
        {
            return $"{(include ? "Include" : "Exclude")} file must be a string in section {section}";
        }

        private string GetInvalidIgnoreMissingMessage(string section)
        {
            return $"Invalid ignore_missing value in section {section} - must be a boolean";
        }

        private string GetInvalidExceptWhereExistsMessage(string section)
        {
            return $"Invalid except_where_exists value in section {section} - must be a string";
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
                string[] directories = Directory.GetDirectories(configFileFolder + Path.DirectorySeparatorChar + configItem.Key);

                // For each airport, iterate the config file sections
                foreach (string directory in directories)
                {
                    IterateConfigFileSections(
                        configItem.Value,
                        AirfieldConfigFileSections.ConfigFileSections,
                        x => OutputGroupFactory.CreateAirport(x, Path.GetFileName(directory)),
                        config.AddAirportInclusionRule,
                        directory,
                        "airport"
                    );
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
        ) {
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
        ) {
            if (jsonConfig.Type == JTokenType.Array)
            {
                foreach (JToken token in (JArray) jsonConfig)
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
            } else {
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
        ) {
            if (jsonConfig.Type != JTokenType.Object)
            {
                throw new ConfigFileInvalidException(GetInvalidConfigParentSectionFormatMessage(sectionRootString));
            }

            JObject configObject = (JObject)jsonConfig;

            // Check the type field to see what we're dealing with
            if (
                !configObject.TryGetValue("type", out var typeToken) ||
                typeToken.Type != JTokenType.String ||
                ((string) typeToken != "files" && (string) typeToken != "folder")
            ) {
                throw new ConfigFileInvalidException(
                    GetMissingTypeMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            if ((string)typeToken == "files")
            {
                ProcessFilesList(
                    configObject,
                    configFileSection,
                    outputGroup,
                    addInclusionRule,
                    rootPath,
                    sectionRootString
                );
            } else
            {
                ProcessFolder(
                    configObject,
                    configFileSection,
                    outputGroup,
                    addInclusionRule,
                    rootPath,
                    sectionRootString
                );
            }
        }

        /**
         * Processes a folder and creates an inclusion rule from it.
         */
        private void ProcessFolder(
            JObject folderObject,
            ConfigFileSection configFileSection,
            OutputGroup outputGroup,
            Action<IInclusionRule> addInclusionRule,
            string rootPath,
            string sectionRootString
        ) {
            // Get the folder
            if (
                !folderObject.TryGetValue("folder", out JToken folder) ||
                folder.Type != JTokenType.String
            ) {
                throw new ConfigFileInvalidException(
                    GetInvalidFolderMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            // Check if it's recursive
            bool recursive = false;
            bool hasRecursiveToken = folderObject.TryGetValue("recursive", out var recursiveToken);
            if (hasRecursiveToken)
            {
                if (recursiveToken.Type != JTokenType.Boolean)
                {
                    throw new ConfigFileInvalidException(
                        GetRecursiveMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                recursive = (bool)recursiveToken;
            }
            
            // Handle inclusion patterns
            Regex patternRegex = null;
            if (folderObject.TryGetValue("pattern", out JToken pattern)) {
                if (pattern.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidPatternMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                try
                {
                    patternRegex = new Regex(pattern.ToString());
                }
                catch (ArgumentException)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidPatternMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }
            }


            // Get the include and exclude lists and check both aren't there
            bool isInclude = folderObject.TryGetValue("include", out var include);
            bool isExclude = folderObject.TryGetValue("exclude", out var exclude);

            if (isInclude && isExclude)
            {
                throw new ConfigFileInvalidException(
                    GetIncludeAndExcludeMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            // If there's no includes or excludes, we can short circuit and stop here.
            if (!isInclude && !isExclude) {
                addInclusionRule(
                    new FolderInclusionRule(
                        NormaliseFilePath(rootPath, (string)folder),
                        recursive,
                        configFileSection.DataType,
                        outputGroup,
                        includePattern: patternRegex
                    )
                );
                return;
            }

            // Lets make a list of files to include or exclude
            List<string> files = new List<string>();
            JToken fileList = isInclude ? include : exclude;
            if (fileList.Type != JTokenType.Array)
            {
                throw new ConfigFileInvalidException(
                    GetInvalidIncludeExcludeListMessage(isInclude, $"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            foreach (JToken file in fileList)
            {
                if (file.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidIncludeExcludeFileMessage(isInclude, $"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                files.Add((string) file);
            }

            addInclusionRule(
                new FolderInclusionRule(
                    NormaliseFilePath(rootPath, (string)folder),
                    recursive,
                    configFileSection.DataType,
                    outputGroup,
                    isExclude,
                    files,
                    patternRegex
                )
            );
        }

        /**
         * Process an explicit list of files and creates an inclusion
         * rule out of them.
         */
        private void ProcessFilesList(
            JObject filesObject,
            ConfigFileSection configFileSection,
            OutputGroup outputGroup,
            Action<IInclusionRule> addInclusionRule,
            string rootPath,
            string sectionRootString
        ) {
            // Get the list of files
            JToken filesList;
            if (
                !filesObject.TryGetValue("files", out filesList) ||
                filesList.Type != JTokenType.Array
            ) {
                throw new ConfigFileInvalidException(
                    InvalidFilesListMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                );
            }

            // Determine whether we should ignore missing files
            bool ignoreMissing = false;
            if (filesObject.TryGetValue("ignore_missing", out var ignoreMissingToken))
            {
                if (ignoreMissingToken.Type != JTokenType.Boolean)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidIgnoreMissingMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                ignoreMissing = (bool)ignoreMissingToken;
            }

            // Should we apply any conditional logic here
            string exceptWhereExists = "";
            if (filesObject.TryGetValue("except_where_exists", out var exceptWhereExistsToken))
            {
                if (exceptWhereExistsToken.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidExceptWhereExistsMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                exceptWhereExists = NormaliseFilePath(rootPath, (string) exceptWhereExistsToken);
            }

            // Get the file paths and normalise against the config files folder
            List<string> filePaths = new List<string>();
            foreach (JToken file in (JArray)filesList)
            {
                if (file.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidFilePathMessage($"{sectionRootString}.{configFileSection.JsonPath}")
                    );
                }

                filePaths.Add(NormaliseFilePath(rootPath, (string)file));
            }

            // Add the rule
            addInclusionRule(
                new FileListInclusionRule(
                    filePaths,
                    ignoreMissing,
                    exceptWhereExists,
                    configFileSection.DataType,
                    outputGroup
                )
            );
        }

        private string NormaliseFilePath(string configFileFolder, string relativeInputFilePath)
        {
            return Path.GetFullPath(configFileFolder + Path.DirectorySeparatorChar + relativeInputFilePath);
        }

        private string GetFolderForConfigFile(string pathToConfigFile)
        {
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile) ?? string.Empty);
        }
    }
}