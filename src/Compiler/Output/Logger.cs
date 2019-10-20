using Compiler.Argument;
using System;
using System.IO;

namespace Compiler.Output
{
    /**
     * A class that handles logging output from the compiler.
     */
    public class Logger: ILoggerInterface
    {
        private readonly IOutputInterface outputStream;

        private readonly CompilerArguments arguments;

        public Logger(IOutputInterface outputStream, CompilerArguments arguments)
        {
            this.outputStream = outputStream;
            this.arguments = arguments;
        }

        public void Debug(string message)
        {
            if (!this.ShouldLog(OutputVerbosity.Debug))
            {
                return;
            }

            this.WriteToLog(message);
        }

        public void Info(string message)
        {
            if (!this.ShouldLog(OutputVerbosity.Info))
            {
                return;
            }

            this.WriteToLog(message);
        }

        public void Warning(string message)
        {
            if (!this.ShouldLog(OutputVerbosity.Warning))
            {
                return;
            }

            this.WriteToLog(message);
        }

        public void Error(string message)
        {
            if (!this.ShouldLog(OutputVerbosity.Error))
            {
                return;
            }

            this.WriteToLog(message);
        }

        private void WriteToLog(string message)
        {
            this.outputStream.WriteLine(message);
        }

        private bool ShouldLog(OutputVerbosity verbosity)
        {
            return this.arguments.Verbosity <= verbosity;
        }
    }
}
