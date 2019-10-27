using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    class TestArgumentParser : IInputParserInterface
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            return compilerSettings;
        }
    }
}
