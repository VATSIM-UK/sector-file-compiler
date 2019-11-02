using System;
using System.Collections.Generic;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class BuildVersionParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Build verson should have only one argument");
            }

            compilerSettings.BuildVersion = values[0];
            return compilerSettings;
        }
    }
}
