using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Output
{
    /*
     * An interface that collects compilable elements according to their output group
     * so that they can then be outputted.
     */
    public interface ICompilableElementCollector
    {
        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements();
    }
}
