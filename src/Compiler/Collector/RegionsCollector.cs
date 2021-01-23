using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
{
    public class RegionsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public RegionsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Regions.OrderBy(region => this.repository.GetForDefinitionFile(region.GetDefinition()));
        }
    }
}
