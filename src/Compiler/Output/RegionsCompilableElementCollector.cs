using Compiler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Compiler.Output
{
    public class RegionsCompilableElementCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public RegionsCompilableElementCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements(OutputSections section)
        {
            return this.sectorElements.Regions.GroupBy(
                region => this.repository.GetForDefinitionFile(region.GetDefinition()),
                region => region
            );
        }
    }
}
