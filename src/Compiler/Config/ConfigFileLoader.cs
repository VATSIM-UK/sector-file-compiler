using System;
using System.Collections.Generic;
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
        public ConfigInclusionRules LoadConfigFiles(List<string> files)
        {
            ConfigInclusionRules config = new ConfigInclusionRules();

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
                    this.IterateAirportConfig(airportData, config, file);
                }

                // Load enroute data
                JToken enrouteData = jsonConfig.SelectToken("enroute");
                if (enrouteData != null)
                {
                    this.IterateConfigFileSections(
                        enrouteData,
                        EnrouteConfigFileSections.configFileSections,
                        x => OutputGroupFactory.CreateEnroute(x),
                        x => config.AddEnrouteInclusionRule(x),
                        GetFolderForConfigFile(file),
                        "enroute"
                    );
                }

                // Load misc data
                JToken miscData = jsonConfig.SelectToken("misc");
                if (miscData != null)
                {
                    this.IterateConfigFileSections(
                        miscData,
                        MiscConfigFileSections.configFileSections,
                        x => OutputGroupFactory.CreateMisc(x),
                        x => config.AddMiscInclusionRule(x),
                        GetFolderForConfigFile(file),
                        "misc"
                    );
                }

            }

            return config;
        }

        private string GetInvalidConfigSectionFormatMessage(string section)
        {
            return string.Format(
                "Invalid config section for {0} - must be the path to a file, or an array of paths",
                section
            );
        }

        private string GetMissingTypeMessage(string section)
        {
            return string.Format(
                "Invalid type field for section {0} - must be \"files\" or \"folders\"",
                section
            );
        }

        private string InvalidFilesListMessage(string section)
        {
            return string.Format(
                "Files list invalid in section {0} - must be array under key \"files\"",
                section
            );
        }

        private string GetInvalidFolderMessage(string section)
        {
            return string.Format(
                "Folder invalid in section {0} - must be string under key \"folder\"",
                section
            );
        }

        private string GetRecursiveMessage(string section)
        {
            return string.Format(
                "Recursive must be a boolean in section {0}",
                section
            );
        }

        private string GetIncludeAndExcludeMessage(string section)
        {
            return string.Format(
                "Cannot specify both include and exclude for folders in section {0}",
                section
            );
        }

        private string GetInvalidFilePathMessage(string section)
        {
            return string.Format(
                "Invalid file path in section {0} - must be a string",
                section
            );
        }

        private string GetInvalidIncludeExcludeListMessage(bool include, string section)
        {
            return string.Format(
                "{0} list must be an array in section {1}",
                include ? "Include" : "Exclude",
                section
            );
        }

        private string GetInvalidIncludeExcludeFileMessage(bool include, string section)
        {
            return string.Format(
                "{0} file must be a string section {1}",
                include ? "Include" : "Exclude",
                section
            );
        }

        private string GetInvalidIgnoreMissingMessage(string section)
        {
            return string.Format(
                "Invalid ignore_missing value in section {0} - must be a boolean",
                section
            );
        }

        private string GetInvalidConfigParentSectionFormatMessage(string section)
        {
            return string.Format(
                "Invalid config section for {0} - must be an object or array of objects",
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
                    string.Format("Invalid airport config in {0} must be an object", configFilePath)
                );
            }

            foreach (KeyValuePair<string, JToken> configItem in (JObject)airportConfig)
            {
                if (configItem.Value.Type != JTokenType.Object)
                {
                    throw new ConfigFileInvalidException(
                        string.Format("Invalid airport config[{0}] in {1} must be an object", configItem.Key, configFilePath)
                    );
                }

                // Get the airport folders
                string configFileFolder = this.GetFolderForConfigFile(configFilePath);
                string[] directories = Directory.GetDirectories(configFileFolder + Path.DirectorySeparatorChar + configItem.Key);

                // For each airport, iterate the config file sections
                foreach (string directory in directories)
                {
                    this.IterateConfigFileSections(
                        configItem.Value,
                        AirfieldConfigFileSections.configFileSections,
                        x => OutputGroupFactory.CreateAirport(x, Path.GetFileName(directory)),
                        x => config.AddAirportInclusionRule(x),
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

                this.LoadConfigSection(
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
                    this.ProcessConfigSectionObject(
                        token,
                        configFileSection,
                        outputGroup,
                        addInclusionRule,
                        configFilePath,
                        sectionRootString
                    );
                }
            } else {
                this.ProcessConfigSectionObject(
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
            JToken typeToken;
            if (
                !configObject.TryGetValue("type", out typeToken) ||
                typeToken.Type != JTokenType.String ||
                ((string) typeToken != "files" && (string) typeToken != "folder")
            ) {
                throw new ConfigFileInvalidException(
                    GetMissingTypeMessage("misc." + configFileSection.JsonPath)
                );
            }

            if ((string)typeToken == "files")
            {
                this.ProcessFilesList(
                    configObject,
                    configFileSection,
                    outputGroup,
                    addInclusionRule,
                    rootPath,
                    sectionRootString
                );
            } else
            {
                this.ProcessFolder(
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
            JToken folder;
            if (
                !folderObject.TryGetValue("folder", out folder) ||
                folder.Type != JTokenType.String
            ) {
                throw new ConfigFileInvalidException(
                    GetInvalidFolderMessage(sectionRootString + configFileSection.JsonPath)
                );
            }

            // Check if it's recursive
            bool recursive = false;
            JToken recursiveToken;
            bool hasRecursiveToken = folderObject.TryGetValue("recursive", out recursiveToken);
            if (hasRecursiveToken)
            {
                if (recursiveToken.Type != JTokenType.Boolean)
                {
                    throw new ConfigFileInvalidException(
                        GetRecursiveMessage(sectionRootString + configFileSection.JsonPath)
                    );
                }

                recursive = (bool)recursiveToken;
            }


            // Get the include and exclude lists and check both aren't there
            JToken include;
            JToken exclude;

            bool isInclude = folderObject.TryGetValue("include", out include);
            bool isExclude = folderObject.TryGetValue("exclude", out exclude);

            if (isInclude && isExclude)
            {
                throw new ConfigFileInvalidException(
                    GetIncludeAndExcludeMessage(sectionRootString + configFileSection.JsonPath)
                );
            }

            // If there's no includes or excludes, we can short circuit and stop here.
            if (!isInclude && !isExclude) {
                addInclusionRule(
                    new FolderInclusionRule(
                        this.NormaliseFilePath(rootPath, (string)folder),
                        recursive,
                        outputGroup,
                        configFileSection.DataType
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
                    GetInvalidIncludeExcludeListMessage(isInclude, sectionRootString + configFileSection.JsonPath)
                );
            }

            foreach (JToken file in fileList)
            {
                if (file.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidIncludeExcludeFileMessage(isInclude, sectionRootString + configFileSection.JsonPath)
                    );
                }

                files.Add((string) file);
            }

            addInclusionRule(
                new FolderInclusionRule(
                    this.NormaliseFilePath(rootPath, (string)folder),
                    recursive,
                    configFileSection.DataType,
                    outputGroup,
                    isExclude,
                    files
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
                    InvalidFilesListMessage(sectionRootString + configFileSection.JsonPath)
                );
            }

            // Determine whether we should ignore missing files
            bool ignoreMissing = false;
            JToken ignoreMissingToken;
            if (filesObject.TryGetValue("ignore_missing", out ignoreMissingToken))
            {
                if (ignoreMissingToken.Type != JTokenType.Boolean)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidIgnoreMissingMessage(sectionRootString + configFileSection.JsonPath)
                    );
                }

                ignoreMissing = (bool)ignoreMissingToken;
            }

            // Get the file paths and normalise against the config files folder
            List<string> filePaths = new List<string>();
            foreach (JToken file in (JArray)filesList)
            {
                if (file.Type != JTokenType.String)
                {
                    throw new ConfigFileInvalidException(
                        GetInvalidFilePathMessage(sectionRootString + configFileSection.JsonPath)
                    );
                }

                filePaths.Add(this.NormaliseFilePath(rootPath, (string)file));
            }

            // Add the rule
            addInclusionRule(
                new FileListInclusionRule(
                    filePaths,
                    ignoreMissing,
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
            return Path.GetFullPath(Path.GetDirectoryName(pathToConfigFile));
        }
    }
}
