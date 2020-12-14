using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class FixesCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public FixesCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Fixes.OrderBy(fix => fix.Identifier);
        }
    }
}
