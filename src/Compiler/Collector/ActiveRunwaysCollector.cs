using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class ActiveRunwaysCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public ActiveRunwaysCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.ActiveRunways.OrderBy(runway => runway.Airfield)
                .ThenBy(runway => runway.Identifier)
                .ThenBy(runway => runway.Mode);
        }
    }
}
