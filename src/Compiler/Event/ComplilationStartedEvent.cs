using Compiler.Argument;

namespace Compiler.Event
{
    public class ComplilationStartedEvent : ICompilerEvent
    {
        public string GetMessage()
        {
            return $"Sector File Compiler version {CompilerArguments.CompilerVersion}: Starting compilation";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
