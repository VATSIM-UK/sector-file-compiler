using System.Linq;
using Compiler.Exception;
using Compiler.Input.Builder;
using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Output;
using Newtonsoft.Json.Linq;
using FileExists = Compiler.Input.Validator.FileExists;

namespace Compiler.Config
{
    public class FileInclusionRuleLoader
    {
        private readonly ConfigPath path;
        private readonly OutputGroup outputGroup;
        private readonly InputFilePathNormaliser pathNormaliser;

        public FileInclusionRuleLoader(
            ConfigPath path,
            OutputGroup outputGroup,
            InputFilePathNormaliser pathNormaliser
        )
        {
            this.path = path;
            this.outputGroup = outputGroup;
            this.pathNormaliser = pathNormaliser;
        }

        public IInclusionRule CreateRule(JObject config)
        {
            return ApplyExcludeDirectory(
                    config,
                    ApplyIgnoreMissingFilter(
                        config,
                        ApplyExceptWhereExistsFilter(
                            config,
                            ApplyGenerator(
                                config,
                                FileInclusionRuleBuilder.Begin()
                            )
                        )
                    )
                )
                .SetDescriptor(new RuleDescriptor($"File inclusion rule in {path}"))
                .AddValidator(new FileExists())
                .SetOutputGroup(outputGroup)
                .SetDataType(path.Section.DataType)
                .Build();
        }

        private IFileInclusionRuleBuilder ApplyGenerator(JObject config, IFileInclusionRuleBuilder builder)
        {
            // Get the list of files
            JToken filesList;
            if (
                !config.TryGetValue("files", out filesList) ||
                filesList.Type != JTokenType.Array
            )
            {
                ThrowInvalidFilesListException();
            }

            // Get the file paths and normalise them
            if (filesList!.Any(file => file.Type != JTokenType.String))
            {
                ThrowInvalidFilePathException();
            }

            return builder.SetGenerator(
                new FileListGenerator(filesList.Select(file => pathNormaliser.NormaliseFilePath((string) file)))
            );
        }

        private IFileInclusionRuleBuilder ApplyExcludeDirectory(JObject config, IFileInclusionRuleBuilder builder)
        {
            if (!config.TryGetValue("exclude_directory", out JToken excludeDirectory))
            {
                return builder;
            }

            if (
                excludeDirectory.Type != JTokenType.Array ||
                excludeDirectory.Any(directory => directory.Type != JTokenType.String)
            )
            {
                ThrowInvalidExcludeDirectoryException();
            }

            return builder.AddFilter(
                new ExcludeByParentFolder(excludeDirectory.Select(directory => (string) directory))
            );
        }

        private IFileInclusionRuleBuilder ApplyIgnoreMissingFilter(
            JObject config,
            IFileInclusionRuleBuilder builder
        )
        {
            if (!config.TryGetValue("ignore_missing", out var ignoreMissingToken))
            {
                return builder;
            }

            if (ignoreMissingToken.Type != JTokenType.Boolean)
            {
                ThrowInvalidIgnoreMissingException();
            }

            return (bool) ignoreMissingToken ? builder.AddFilter(new Input.Filter.FileExists()) : builder;
        }

        private IFileInclusionRuleBuilder ApplyExceptWhereExistsFilter(JObject config,
            IFileInclusionRuleBuilder builder)
        {
            if (!config.TryGetValue("except_where_exists", out var exceptWhereExistsToken))
            {
                return builder;
            }

            if (exceptWhereExistsToken.Type != JTokenType.String)
            {
                ThrowInvalidExceptWhereExistsException();
            }

            return builder.AddFilter(
                new IgnoreWhenFileExists(pathNormaliser.NormaliseFilePath((string) exceptWhereExistsToken))
            );
        }

        private void ThrowInvalidFilesListException()
        {
            ThrowConfigFileException(
                $"Files list invalid in section {path} - must be array under key \"files\""
            );
        }

        private void ThrowInvalidExcludeDirectoryException()
        {
            ThrowConfigFileException(
                $"Invalid exclude_directory invalid in section {path} - must be an array of strings"
            );
        }

        private void ThrowInvalidFilePathException()
        {
            ThrowConfigFileException(
                $"Invalid file path in section {path} - must be a string"
            );
        }

        private void ThrowInvalidIgnoreMissingException()
        {
            ThrowConfigFileException(
                $"Invalid ignore_missing value in section {path} - must be a boolean"
            );
        }

        private void ThrowInvalidExceptWhereExistsException()
        {
            ThrowConfigFileException(
                $"Invalid except_where_exists value in section {path} - must be a string"
            );
        }

        private void ThrowConfigFileException(string message)
        {
            throw new ConfigFileInvalidException(message);
        }
    }
}
