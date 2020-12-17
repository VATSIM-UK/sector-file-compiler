using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Output
{
    public class AirspaceCollector : ICompilableElementCollector
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository outputGroups;

        public AirspaceCollector(SectorElementCollection sectorElements, OutputGroupRepository outputGroups)
        {
            this.sectorElements = sectorElements;
            this.outputGroups = outputGroups;
        }

        /*
         * The airspace section is a mashup of Sectorlines, Sectors and Agreements.
         *
         * Sectorlines and CircleSectorlines should be sorted together, followed by sectors, followed by agreements.
         */
        public IEnumerable<ICompilableElementProvider> GetCompilableElements()
        {

            return this.sectorElements.SectorLines.Cast<AbstractCompilableElement>()
                .Concat(this.sectorElements.Sectors)
                .OrderBy(
                    sectorline => this.outputGroups.GetForDefinitionFile(sectorline.GetDefinition())
                )
                .Concat(
                    this.sectorElements.Sectors.OrderBy(sector => this.outputGroups.GetForDefinitionFile(sector.GetDefinition()))
                )
                .Concat(
                    this.sectorElements.CoordinationPoints.OrderBy(coordinationPoint => coordinationPoint.IsFirCopx)
                );
        }
    }
}
