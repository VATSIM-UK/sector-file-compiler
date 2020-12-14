using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class HighArtccCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public HighArtccCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.HighArtccs.OrderBy(artcc => artcc.Identifier);
        }
    }
}
