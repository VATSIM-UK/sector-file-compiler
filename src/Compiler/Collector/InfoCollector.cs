using System.Collections.Generic;
using Compiler.Model;

namespace Compiler.Collector
{
    public class InfoCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public InfoCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return new List<Info> {this.sectorElements.Info};
        }
    }
}
