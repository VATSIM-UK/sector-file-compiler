namespace Compiler.Event
{
    public interface IEventLogger
    {
        void AddEvent(ICompilerEvent compilerEvent);
    }
}
