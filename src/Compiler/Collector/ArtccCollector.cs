using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class ArtccCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public ArtccCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.Artccs.GroupBy(
                artcc => this.repository.GetForDefinitionFile(artcc.GetDefinition()),
                artcc => artcc
            );
        }
    }
}
