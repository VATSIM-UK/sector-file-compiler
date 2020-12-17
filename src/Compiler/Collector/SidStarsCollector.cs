using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
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
                .ThenBy(sidstar => sidstar.Airport)
                .ThenBy(sidstar => sidstar.Identifier);
        }
    }
}
