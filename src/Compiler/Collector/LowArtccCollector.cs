using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class LowArtccCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public LowArtccCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.LowArtccs.OrderBy(artcc => artcc.Identifier);
        }
    }
}
