
using Compiler.Argument;
using Compiler.Input.Validator;
using Compiler.Output;

namespace Compiler.Config
{
    public class FolderInclusionRuleLoaderFactory
    {
        private readonly CompilerArguments arguments;

        public FolderInclusionRuleLoaderFactory(CompilerArguments arguments)
        {
            this.arguments = arguments;
        }

        public FolderInclusionRuleLoader Create(
            ConfigPath path,
            OutputGroup outputGroup,
            string configFilePath
        ) {
            return new(
                path,
                outputGroup,
                new (configFilePath),
                new FilelistNotEmpty(arguments.EmptyFolderAction)
            );
        }
    }
}
