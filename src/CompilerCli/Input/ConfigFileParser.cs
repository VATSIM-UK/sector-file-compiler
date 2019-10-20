using System;
using Compiler.Argument;
using Compiler.Input;

namespace CompilerCli.Input
{
    public class ConfigFileParser : IInputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings)
        {
            compilerSettings.ConfigFile = new InputFile(argument);
            return compilerSettings;
        }
    }
}
