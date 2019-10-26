namespace Compiler.Event
{
    public interface ICompilerEvent
    {
        public string GetMessage();

        public bool IsFatal();
    }
}
