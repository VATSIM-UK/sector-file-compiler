using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class InfoCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public InfoCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return new List<Info>() {
                this.sectorElements.Info
            }.GroupBy(
                info => new OutputGroup("sct.info"),
                info => info
            );
        }
    }
}
