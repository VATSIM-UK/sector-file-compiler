using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class RegionsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public RegionsCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return sectorElements.Regions.OrderBy(region => region.Name);
        }
    }
}
