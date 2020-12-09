using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class SidStarsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public SidStarsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.SidStars.GroupBy(
                sidStar => this.repository.GetForDefinitionFile(sidStar.GetDefinition()),
                sidStar => sidStar
            );
        }
    }
}
