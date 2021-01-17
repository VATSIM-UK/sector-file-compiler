using System;
using System.IO;
using Compiler;
using Compiler.Argument;
using CompilerCli.Output;
using CompilerCli.Input;
using Compiler.Event;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CompilerCli
{
    class Cli
    {
        [ExcludeFromCodeCoverage]
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
                arguments = ArgumentParserFactory.Make().CreateFromCommandLine(args);
            } catch (ArgumentException exception)
            {
                output.Write(exception.Message);
                return 1;
            }

            int returnCode = SectorFileCompilerFactory.Create(
                arguments,
                new List<IEventObserver>() { new ConsoleOutput(output) }
            ).Compile();

            output.Write("Press any key to exit");
            Console.ReadKey();
            return returnCode;
        }
    }
}
