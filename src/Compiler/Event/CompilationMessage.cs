using System;

namespace Compiler.Event
{
    public class CompilationMessage : ICompilerEvent
    {
        private readonly string message;

        public CompilationMessage(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return String.Format("INFO: {0}", this.message);
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
