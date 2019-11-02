namespace Compiler.Event
{
    public interface IEventObserver
    {
        public void NewEvent(ICompilerEvent log);
    }
}
