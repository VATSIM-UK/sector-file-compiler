using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class StripCommentsParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.StripComments = true;
            return compilerSettings;
        }
    }
}
