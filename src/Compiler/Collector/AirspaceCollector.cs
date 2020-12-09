using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class AirspaceCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository repository;

        public AirspaceCollector(SectorElementCollection sectorElements, OutputGroupRepository repository)
        {
            this.sectorElements = sectorElements;
            this.repository = repository;
        }

        /*
         * The airspace section is a mashup of Sectorlines, Sectors and Agreements
         */
        public IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> GetCompilableElements()
        {
            List<IGrouping<OutputGroup, ICompilableElementProvider>> elements = new List<IGrouping<OutputGroup, ICompilableElementProvider>>();

            // Sectorlines and circle sectorlines live together, so handle them first.
            List<AbstractCompilableElement> sectorlines = new List<AbstractCompilableElement>();
            sectorlines.Concat(this.sectorElements.SectorLines);
            sectorlines.Concat(this.sectorElements.CircleSectorLines);
            elements.Concat(
                sectorlines.GroupBy(
                    sectorline => this.repository.GetForDefinitionFile(sectorline.GetDefinition()),
                    sectorline => sectorline
                )
            );

            // Sectors rely on sectorline definitions, so add them next.
            elements.Concat(
                this.sectorElements.Sectors.GroupBy(
                    sector => this.repository.GetForDefinitionFile(sector.GetDefinition()),
                    sector => sector
                )
            );

            // Finally, agreements / coordination points rely on sectors, so add them last
            elements.Concat(
                this.sectorElements.CoordinationPoints.GroupBy(
                    coordinationPoint => this.repository.GetForDefinitionFile(coordinationPoint.GetDefinition()),
                    coordinationPoint => coordinationPoint
                )
            );

            return elements;
        }
    }
}
