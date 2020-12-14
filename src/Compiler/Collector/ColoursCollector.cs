using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class ColoursCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public ColoursCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Colours.OrderBy(colour => colour.Name);
        }
    }
}
