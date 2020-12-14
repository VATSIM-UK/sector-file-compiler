using Compiler.Model;
using System.Collections.Generic;

namespace Compiler.Output
{
    public class HeaderCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public HeaderCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.FileHeaders;
        }
    }
}
