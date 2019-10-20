using System;
using System.IO;
using Compiler.Output;

namespace CompilerCli.Output
{
    public class ConsoleOutput : IOutputInterface
    {
        // The console
        private StreamWriter console;

        public ConsoleOutput(StreamWriter console)
        {
            this.console = console;
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
