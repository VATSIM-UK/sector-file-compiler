using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ContiguousRouteParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            compilerSettings.EnforceContiguousRouteSegments = true;
            return compilerSettings;
        }
    }
}
