using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class VorsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public VorsCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Vors.OrderBy(vor => vor.Identifier);
        }
    }
}
