using System;
using Compiler.Argument;

namespace Compiler.Event
{
    public class ComplilationStartedEvent : ICompilerEvent
    {
        public string GetMessage()
        {
            return String.Format(
                "Sector File Compiler version {0}: Starting compilation",
                CompilerArguments.COMPILER_VERISON
            );
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
