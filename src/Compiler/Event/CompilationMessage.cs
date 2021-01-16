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
            return $"INFO: {this.message}";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
