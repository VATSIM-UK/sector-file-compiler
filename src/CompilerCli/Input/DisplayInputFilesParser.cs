using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class DisplayInputFilesParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.DisplayInputFiles = true;
            return compilerSettings;
        }
    }
}
