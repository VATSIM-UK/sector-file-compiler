using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    class DefaultArgumentParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            return compilerSettings;
        }
    }
}
