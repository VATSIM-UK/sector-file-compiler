using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class RunwaysCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public RunwaysCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Runways.OrderBy(runway => runway.AirfieldIcao)
                .ThenBy(runway => runway.FirstIdentifier);
        }
    }
}
