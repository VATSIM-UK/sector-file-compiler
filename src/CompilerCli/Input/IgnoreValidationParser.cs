using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class IgnoreValidationParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.ValidateOutput = false;
            return compilerSettings;
        }
    }
}
