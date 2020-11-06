using Compiler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Compiler.Output
{
    public class GeoCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public GeoCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            return this.sectorElements.GeoElements.GroupBy(
                geo => this.repository.GetForDefinitionFile(geo.GetDefinition()),
                geo => geo
            );
        }
    }
}
