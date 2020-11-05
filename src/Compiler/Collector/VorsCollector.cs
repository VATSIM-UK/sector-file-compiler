using Compiler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Compiler.Output
{
    public class VorsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public VorsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.Vors.GroupBy(
                vor => this.repository.GetForDefinitionFile(vor.GetDefinition()),
                vor => vor
            );
        }
    }
}
