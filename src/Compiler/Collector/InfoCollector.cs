using Compiler.Model;
using System.Collections.Generic;

namespace Compiler.Output
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
