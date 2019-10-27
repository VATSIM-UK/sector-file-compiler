using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    interface IInputParserInterface
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings);
    }
}
