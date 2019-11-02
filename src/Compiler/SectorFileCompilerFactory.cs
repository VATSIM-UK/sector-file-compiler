using Compiler.Argument;
using Compiler.Event;
using System.Collections.Generic;

namespace Compiler
{
    public class SectorFileCompilerFactory
    {
        public static SectorFileCompiler Create(CompilerArguments arguments, List<IEventObserver> eventObservers)
        {
            EventTracker events = new EventTracker();
            foreach (IEventObserver observer in eventObservers)
            {
                events.AddObserver(observer);
            }

            return new SectorFileCompiler(
                arguments,
                events
            );
        }
    }
}
