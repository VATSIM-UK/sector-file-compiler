using System;
using Compiler.Event;

namespace Compiler.Error
{
    public class FileNotFoundError : ICompilerEvent
    {
        private readonly string filename;
        public FileNotFoundError(string filename)
        {
            this.filename = filename;
        }

        public string GetMessage()
        {
            return String.Format(
                "File not found: {0}",
                this.filename
            );
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
