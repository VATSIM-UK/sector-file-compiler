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
            return $"Argument error: {this.message}";
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
