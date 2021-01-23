using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class HighAirwaysCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public HighAirwaysCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.HighAirways.OrderBy(airway => airway.Identifier);
        }
    }
}
