using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
{
    public class SidsCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public SidsCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {
            return this.sectorElements.SidRoutes.OrderBy(sid => this.repository.GetForDefinitionFile(sid.GetDefinition()));
        }
    }
}
