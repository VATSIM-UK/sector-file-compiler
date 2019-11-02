namespace Compiler.Event
{
    public class CompilationFinishedEvent : ICompilerEvent
    {
        private readonly bool success;

        public CompilationFinishedEvent(bool success)
        {
            this.success = success;
        }

        public string GetMessage()
        {
            return this.success ? "Compilation completed successfully" : "Compilation failed";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
