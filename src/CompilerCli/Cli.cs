using System;
using System.Data;
using System.IO;
using Compiler;
using Compiler.Argument;
using CompilerCli.Output;
using CompilerCli.Input;

namespace CompilerCli
{
    class Cli
    {
        static int Main(string[] args)
        {
            // Setup the console output stream.
            StreamWriter output = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(output);

            // Parse the sectorfile
            CompilerArguments arguments;
            try
            {
                arguments = ArgumentParser.CreateFromCommandLine(args);
            } catch (ArgumentException exception)
            {
                output.Write(exception.Message);
                return 1;
            }

            SectorFileCompilerFactory.Create(
                arguments,
                new ConsoleOutput(output)
            ).Compile();

            output.Write("Press any key to exit");
            Console.ReadKey();
            return 0;
        }
    }
}
