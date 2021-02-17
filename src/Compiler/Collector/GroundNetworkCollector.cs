using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Collector
{
    public class GroundNetworkCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;

        public GroundNetworkCollector(SectorElementCollection sectorElements)
        {
            this.sectorElements = sectorElements;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return sectorElements.GroundNetworks.OrderBy(network => network.Airport);
        }
    }
}