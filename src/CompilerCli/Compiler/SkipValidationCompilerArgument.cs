using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Compiler
{
    public class SkipValidationCompilerArgument : AbstractCompilerArgument
    {
        public override void Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.ValidateOutput = false;
        }

        public override string GetSpecifier()
        {
            return "--skip-validation";
        }
    }
}
