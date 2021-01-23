using System.Collections.Generic;

namespace Compiler.Event
{
    public class EventTracker: IEventLogger
    {
        private readonly List<ICompilerEvent> events = new();

        private readonly List<IEventObserver> observers = new();

        public void AddObserver(IEventObserver observer)
        {
            this.observers.Add(observer);
        }

        public void AddEvent(ICompilerEvent compilerEvent)
        {
            this.events.Add(compilerEvent);
            foreach (IEventObserver observer in this.observers)
            {
                observer.NewEvent(compilerEvent);
            }
        }

        public ICompilerEvent GetLastEvent()
        {
            return this.events[^1];
        }

        public int CountEvents()
        {
            return this.events.Count;
        }

        public bool HasFatalError()
        {
            foreach(ICompilerEvent compilerEevent in this.events)
            {
                if (compilerEevent.IsFatal())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
