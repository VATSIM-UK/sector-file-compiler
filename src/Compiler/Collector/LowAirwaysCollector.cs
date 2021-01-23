using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class LowAirwaysCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public LowAirwaysCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.LowAirways.OrderBy(airway => airway.Identifier);
        }
    }
}
