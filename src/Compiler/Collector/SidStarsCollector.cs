using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class SidStarsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public SidStarsCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.SidStars.OrderBy(sidStar => sidStar.Type)
                .ThenBy(sidstar => sidstar.Airport);
        }
    }
}
