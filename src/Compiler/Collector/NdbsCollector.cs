using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class NdbsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public NdbsCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Ndbs.OrderBy(ndb => ndb.Identifier);
        }
    }
}
