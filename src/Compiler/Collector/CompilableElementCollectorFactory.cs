using System;
using Compiler.Model;

namespace Compiler.Output
{
    public class CompilableElementCollectorFactory
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository outputGroups;

        public CompilableElementCollectorFactory(
            SectorElementCollection sectorElements,
            OutputGroupRepository outputGroups
        ) {
            this.sectorElements = sectorElements;
            this.outputGroups = outputGroups;
        }

        public ICompilableElementCollector GetCollectorForOutputSection(OutputSectionKeys section)
        {
            switch (section)
            {
                // Headers
                case OutputSectionKeys.FILE_HEADER:
                    return new HeaderCollector(this.sectorElements);

                // SCT Sections
                case OutputSectionKeys.SCT_COLOUR_DEFS:
                    return new ColoursCollector(this.sectorElements);
                case OutputSectionKeys.SCT_INFO:
                    return new InfoCollector(this.sectorElements);
                case OutputSectionKeys.SCT_AIRPORT:
                    return new AirportsCollector(this.sectorElements);
                case OutputSectionKeys.SCT_RUNWAY:
                    return new RunwaysCollector(this.sectorElements);
                case OutputSectionKeys.SCT_VOR:
                    return new VorsCollector(this.sectorElements);
                case OutputSectionKeys.SCT_NDB:
                    return new NdbsCollector(this.sectorElements);
                case OutputSectionKeys.SCT_FIXES:
                    return new FixesCollector(this.sectorElements);
                case OutputSectionKeys.SCT_GEO:
                    return new GeoCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.SCT_LOW_AIRWAY:
                    return new LowAirwaysCollector(this.sectorElements);
                case OutputSectionKeys.SCT_HIGH_AIRWAY:
                    return new HighAirwaysCollector(this.sectorElements);
                case OutputSectionKeys.SCT_ARTCC:
                    return new ArtccCollector(this.sectorElements);
                case OutputSectionKeys.SCT_ARTCC_LOW:
                    return new LowArtccCollector(this.sectorElements);
                case OutputSectionKeys.SCT_ARTCC_HIGH:
                    return new HighArtccCollector(this.sectorElements);
                case OutputSectionKeys.SCT_SID:
                    return new SidsCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.SCT_STAR:
                    return new StarsCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.SCT_LABELS:
                    return new LabelsCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.SCT_REGIONS:
                    return new RegionsCollector(this.sectorElements, this.outputGroups);

                // ESE sections.
                case OutputSectionKeys.ESE_POSITIONS:
                    return new PositionsCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.ESE_FREETEXT:
                    return new FreetextCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.ESE_SIDSSTARS:
                    return new SidStarsCollector(this.sectorElements);
                case OutputSectionKeys.ESE_AIRSPACE:
                    return new AirspaceCollector(this.sectorElements, this.outputGroups);
                case OutputSectionKeys.RWY_ACTIVE_RUNWAYS:
                    return new ActiveRunwaysCollector(this.sectorElements);
                default:
                    break;
            }

            throw new ArgumentException("No element collector for section " + section.ToString());
        }
    }
}
