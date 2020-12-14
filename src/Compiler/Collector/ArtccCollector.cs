using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class ArtccCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public ArtccCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Artccs.OrderBy(artcc => artcc.Identifier);
        }
    }
}
