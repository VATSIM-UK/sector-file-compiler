using System;
using Compiler.Argument;
using Compiler.Input;

namespace CompilerCli.Input
{
    public class OutputFileParser : IInputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings)
        {
            compilerSettings.OutFile = new InputFile(argument);
            return compilerSettings;
        }
    }
}
