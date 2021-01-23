using System;
using System.IO;
using Compiler;
using Compiler.Argument;
using CompilerCli.Output;
using Compiler.Event;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CompilerCli.Argument;
using CompilerCli.Cli;

namespace CompilerCli
{
    class CompilerCli
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
            CompilerArguments compilerArguments = CompilerArgumentsFactory.Make();
            CliArguments cliArguments = CliArgumentsFactory.Make();
            try
            {
                ArgumentParserFactory.Make().CreateFromCommandLine(compilerArguments, cliArguments, args);
            } catch (ArgumentException exception)
            {
                output.Write(exception.Message);
                return 1;
            }

            int returnCode = SectorFileCompilerFactory.Create(
                compilerArguments,
                new List<IEventObserver>() { new ConsoleOutput(output) }
            ).Compile();

            if (cliArguments.PauseOnFinish)
            {
                output.Write("Press any key to exit");
                Console.ReadKey();
            }
            return returnCode;
        }
    }
}
