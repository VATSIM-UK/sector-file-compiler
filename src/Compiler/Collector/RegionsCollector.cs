using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
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

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.Regions.GroupBy(
                region => this.repository.GetForDefinitionFile(region.GetDefinition()),
                region => region
            );
        }
    }
}
