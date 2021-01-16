using System.Collections.Generic;
using Compiler.Model;

namespace Compiler.Collector
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
