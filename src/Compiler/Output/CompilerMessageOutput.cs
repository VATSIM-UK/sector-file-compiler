using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Compiler.Output
{
    public class CompilerMessageOutput
    {
        private StreamWriter outputStream;

        public CompilerMessageOutput(StreamWriter outputStream)
        {
            this.outputStream = outputStream;
        }

        public void WriteLine(string message)
        {
            this.outputStream.Write(message + Environment.NewLine);
        }
    }
}
