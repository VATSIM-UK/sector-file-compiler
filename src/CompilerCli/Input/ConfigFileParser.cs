using System;
using Compiler.Argument;

namespace CompilerCli.Input
{
    public class ConfigFileParser : InputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings)
        {
            compilerSettings.ConfigFilePath = argument;
            return compilerSettings;
        }
    }
}
