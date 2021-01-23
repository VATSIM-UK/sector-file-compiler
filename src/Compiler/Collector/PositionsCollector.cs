using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
{
    public class PositionsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public PositionsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.EsePositions
                .OrderBy(position => position.PositionOrder)
                .ThenBy(position => this.repository.GetForDefinitionFile(position.GetDefinition()));
        }
    }
}
