using System.IO;
using Compiler.Event;

namespace CompilerCli.Output
{
    public class ConsoleOutput : IEventObserver
    {
        // The console
        private readonly TextWriter console;

        public ConsoleOutput(TextWriter console)
        {
            this.console = console;
        }

        public void NewEvent(ICompilerEvent log)
        {
            console.WriteLine(log.GetMessage());
        }
    }
}
