using System;
using System.Collections.Generic;
using System.Text;
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
