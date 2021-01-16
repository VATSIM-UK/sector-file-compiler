using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
{
    public class LabelsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public LabelsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.Labels.OrderBy(label => this.repository.GetForDefinitionFile(label.GetDefinition()));
        }
    }
}
