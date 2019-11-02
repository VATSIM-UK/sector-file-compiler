using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    interface IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings);
    }
}
