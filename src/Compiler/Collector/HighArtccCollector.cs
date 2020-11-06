using Compiler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Compiler.Output
{
    public class HighArtccCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public HighArtccCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.HighArtccs.GroupBy(
                artcc => this.repository.GetForDefinitionFile(artcc.GetDefinition()),
                artcc => artcc
            );
        }
    }
}
