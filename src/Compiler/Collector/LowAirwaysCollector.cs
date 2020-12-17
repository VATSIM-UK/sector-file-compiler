using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
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
