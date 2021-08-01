using System;
using System.Collections.Generic;
using Compiler.Exception;
using Compiler.Input.Builder;
using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Input.Sorter;
using Compiler.Input.Validator;
using Compiler.Output;
using Newtonsoft.Json.Linq;

namespace Compiler.Config
{
    public class FolderInclusionRuleLoader
    {
        private readonly ConfigPath path;
        private readonly OutputGroup outputGroup;
        private readonly InputFilePathNormaliser pathNormaliser;
        private readonly FilelistNotEmpty filelistNotEmptyValidator;

        public FolderInclusionRuleLoader(
            ConfigPath path,
            OutputGroup outputGroup,
            InputFilePathNormaliser pathNormaliser, 
            FilelistNotEmpty filelistNotEmptyValidator
        ) {
            this.path = path;
            this.outputGroup = outputGroup;
            this.pathNormaliser = pathNormaliser;
            this.filelistNotEmptyValidator = filelistNotEmptyValidator;
        }

        public IInclusionRule CreateRule(JObject config)
        {
            return ApplyFilesetValidator( 
                ApplySorter(
                    ApplyIncludeExcludeFilter(
                        config,
                        ApplyPatternFilter(
                            config,
                            ApplyGenerator(
                                config,
                                FileInclusionRuleBuilder.Begin()
                            )
                        )
                    )
                )
            )
                .SetDescriptor(new RuleDescriptor($"Folder inclusion rule in {path}"))
                .SetOutputGroup(outputGroup)
                .SetDataType(path.Section.DataType)
                .Build();
        }

        private IFileInclusionRuleBuilder ApplyFilesetValidator(IFileInclusionRuleBuilder builder)
        {
            return builder.AddFilesetValidator(filelistNotEmptyValidator);
        }

        private IFileInclusionRuleBuilder ApplySorter(IFileInclusionRuleBuilder builder)
        {
            return builder.AddSorter(new AlphabeticalPathSorter());
        }

        private IFileInclusionRuleBuilder ApplyIncludeExcludeFilter(JObject config, IFileInclusionRuleBuilder builder)
        {
            // Get the include and exclude lists and check both aren't there
            bool isInclude = config.TryGetValue("include", out var include);
            bool isExclude = config.TryGetValue("exclude", out var exclude);

            if (isInclude && isExclude)
            {
                ThrowIncludeExcludeException();
            }

            if (!isInclude && !isExclude)
            {
                return builder;
            }

            List<string> files = new List<string>();
            JToken fileList = isInclude ? include : exclude;
            if (fileList.Type != JTokenType.Array)
            {
                ThrowIncludeExcludeNotArrayException(isInclude ? "Include" : "Exclude");
            }

            foreach (JToken file in fileList)
            {
                if (file.Type != JTokenType.String)
                {
                    ThrowIncludeExcludeInvalidException(isInclude ? "Include" : "Exclude");
                }

                files.Add((string) file);
            }

            return builder.AddFilter(isInclude ? new IncludeFileFilter(files) : new ExcludeFileFilter(files));
        }

        private IFileInclusionRuleBuilder ApplyPatternFilter(JObject config, IFileInclusionRuleBuilder builder)
        {
            // Handle inclusion patterns
            if (config.TryGetValue("pattern", out JToken pattern))
            {
                if (pattern.Type != JTokenType.String)
                {
                    ThrowInvalidPatternException();
                }

                try
                {
                    builder.AddFilter(new FilePatternFilter(new(pattern.ToString())));
                }
                catch (ArgumentException)
                {
                    ThrowInvalidPatternException();
                }
            }

            return builder;
        }

        private IFileInclusionRuleBuilder ApplyGenerator(JObject config, IFileInclusionRuleBuilder builder)
        {
            // Get the folder name
            if (
                !config.TryGetValue("folder", out JToken folder) ||
                folder.Type != JTokenType.String
            )
            {
                ThrowInvalidFolderException();
            }

            // Check if it's recursive
            bool recursive = false;
            bool hasRecursiveToken = config.TryGetValue("recursive", out var recursiveToken);
            if (hasRecursiveToken)
            {
                if (recursiveToken.Type != JTokenType.Boolean)
                {
                    ThrowRecursiveException();
                }

                recursive = (bool) recursiveToken;
            }

            return builder.SetGenerator(GetFileListGenerator((string) folder, recursive));
        }

        private IFileListGenerator GetFileListGenerator(string folderPath, bool recursive)
        {
            return recursive
                ? new RecursiveFolderFileListGenerator(pathNormaliser.NormaliseFilePath(folderPath))
                : new FolderFileListGenerator(pathNormaliser.NormaliseFilePath(folderPath));
        }

        private void ThrowInvalidFolderException()
        {
            ThrowConfigFileException(
                $"Folder invalid in section {path} - must be string under key \"folder\""
            );
        }

        private void ThrowInvalidPatternException()
        {
            ThrowConfigFileException(
                $"Pattern invalid in section {path} - must be a regular expression string"
            );
        }

        private void ThrowRecursiveException()
        {
            ThrowConfigFileException(
                $"Recursive must be a boolean in section {path}"
            );
        }

        private void ThrowIncludeExcludeException()
        {
            ThrowConfigFileException(
                $"Cannot specify both include and exclude for folders in section {path}"
            );
        }
        
        private void ThrowIncludeExcludeInvalidException(string includeExclude)
        {
            ThrowConfigFileException(
                $"{includeExclude} file must be a string in section enroute.ownership"
            );
        }
        
        private void ThrowIncludeExcludeNotArrayException(string includeExclude)
        {
            ThrowConfigFileException(
                $"{includeExclude} list must be an array in section enroute.ownership"
            );
        }

        private void ThrowConfigFileException(string message)
        {
            throw new ConfigFileInvalidException(message);
        }
    }
}
