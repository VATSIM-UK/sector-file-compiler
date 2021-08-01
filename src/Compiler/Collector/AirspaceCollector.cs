using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
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
            return sectorElements.SectorLines.Cast<AbstractCompilableElement>()
                .Concat(sectorElements.CircleSectorLines)
                .OrderBy(
                    sectorline => outputGroups.GetForDefinitionFile(sectorline.GetDefinition())
                )
                .Concat(sectorElements.Sectors)
                .Concat(
                    sectorElements.CoordinationPoints.OrderBy(coordinationPoint => coordinationPoint.IsFirCopx)
                );
        }
    }
}
