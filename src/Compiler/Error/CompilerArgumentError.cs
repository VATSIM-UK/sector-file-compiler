using System;
using Compiler.Event;

namespace Compiler.Error
{
    public class CompilerArgumentError : ICompilerEvent
    {
        private readonly string message;
        public CompilerArgumentError(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return String.Format(
                "Argument error: {0}",
                this.message
            );
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
