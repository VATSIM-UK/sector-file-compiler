using Compiler.Argument;
using Compiler.Output;
using Compiler.Event;

namespace Compiler
{
    public class SectorFileCompilerFactory
    {
        public static SectorFileCompiler Create(CompilerArguments arguments, IOutputInterface output)
        {
            EventTracker events = new EventTracker();
            return new SectorFileCompiler(
                arguments,
                events
            );
        }
    }
}
