using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    class TestArgumentParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            return compilerSettings;
        }
    }
}
