using System;
using Compiler.Argument;
using Compiler.Input;
using System.IO;

namespace CompilerCli.Input
{
    public class OutputFileParser : IInputParserInterface
    {
        public CompilerArguments Parse(string argument, CompilerArguments compilerSettings)
        {
            StreamWriter writer = new StreamWriter(argument, false);
            writer.AutoFlush = true;
            compilerSettings.OutFile = writer;
            return compilerSettings;
        }
    }
}
