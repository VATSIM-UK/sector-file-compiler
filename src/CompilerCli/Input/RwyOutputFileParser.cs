using System;
using System.Collections.Generic;
using Compiler.Argument;
using System.IO;

namespace CompilerCli.Input
{
    public class RwyOutputFileParser : IInputParser
    {
        public CompilerArguments Parse(List<string> values, CompilerArguments compilerSettings)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException("Rwy output file path should have only one argument");
            }

            StreamWriter writer = new StreamWriter(values[0], false);
            writer.AutoFlush = true;
            compilerSettings.OutFileRwy = writer;
            return compilerSettings;
        }
    }
}
