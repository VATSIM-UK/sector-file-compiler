using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class AirportsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public AirportsCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Airports.OrderBy(airport => airport.Icao);
        }
    }
}
