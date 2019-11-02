using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class StripNewlinesParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.RemoveBlankLines = true;
            return compilerSettings;
        }
    }
}
