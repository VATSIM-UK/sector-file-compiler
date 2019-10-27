using System;
using System.Collections.Generic;
using Compiler.Argument;
using System.IO;

namespace CompilerCli.Input
{
    public class OutputFileParser : IInputParserInterface
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Output file path should have only one argument");
            }

            StreamWriter writer = new StreamWriter(values[0], false);
            writer.AutoFlush = true;
            compilerSettings.OutFile = writer;
            return compilerSettings;
        }
    }
}
