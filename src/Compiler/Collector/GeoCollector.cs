using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
{
    public class GeoCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public GeoCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return new List<ICompilableElementProvider>(
                sectorElements.GeoElements.OrderBy(geo => repository.GetForDefinitionFile(geo.GetDefinition()))
                    .ToList()
            )
                .Concat(sectorElements.FixedColourRunwayCentrelines)
                .Concat(sectorElements.RunwayCentrelines);
        }
    }
}
