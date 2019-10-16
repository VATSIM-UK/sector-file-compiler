using System;
using System.IO;
using Compiler;
using Compiler.Input;
using System.Collections.Generic;
using Compiler.Output;

namespace CompilerCli
{
    class Cli
    {
        static void Main(string[] args)
        {
            SectorFileCompiler compiler = new SectorFileCompiler();

            StreamWriter output = new StreamWriter(Console.OpenStandardOutput());
            output.AutoFlush = true;
            Console.SetOut(output);

            compiler.Compile(new List<Argument>(), new CompilerMessageOutput(output));
            output.Write("Press any key to exit");
            Console.ReadKey();
        }
    }
}
