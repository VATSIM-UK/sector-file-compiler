using System;
using Compiler.Model;
using Compiler.Output;

namespace Compiler.Collector
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
            return section switch
            {
                // Headers
                OutputSectionKeys.FILE_HEADER => new HeaderCollector(this.sectorElements),
                // SCT Sections
                OutputSectionKeys.SCT_COLOUR_DEFS => new ColoursCollector(this.sectorElements),
                OutputSectionKeys.SCT_INFO => new InfoCollector(this.sectorElements),
                OutputSectionKeys.SCT_AIRPORT => new AirportsCollector(this.sectorElements),
                OutputSectionKeys.SCT_RUNWAY => new RunwaysCollector(this.sectorElements),
                OutputSectionKeys.SCT_VOR => new VorsCollector(this.sectorElements),
                OutputSectionKeys.SCT_NDB => new NdbsCollector(this.sectorElements),
                OutputSectionKeys.SCT_FIXES => new FixesCollector(this.sectorElements),
                OutputSectionKeys.SCT_GEO => new GeoCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.SCT_LOW_AIRWAY => new LowAirwaysCollector(this.sectorElements),
                OutputSectionKeys.SCT_HIGH_AIRWAY => new HighAirwaysCollector(this.sectorElements),
                OutputSectionKeys.SCT_ARTCC => new ArtccCollector(this.sectorElements),
                OutputSectionKeys.SCT_ARTCC_LOW => new LowArtccCollector(this.sectorElements),
                OutputSectionKeys.SCT_ARTCC_HIGH => new HighArtccCollector(this.sectorElements),
                OutputSectionKeys.SCT_SID => new SidsCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.SCT_STAR => new StarsCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.SCT_LABELS => new LabelsCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.SCT_REGIONS => new RegionsCollector(this.sectorElements, this.outputGroups),
                // ESE sections.
                OutputSectionKeys.ESE_POSITIONS => new PositionsCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.ESE_FREETEXT => new FreetextCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.ESE_SIDSSTARS => new SidStarsCollector(this.sectorElements),
                OutputSectionKeys.ESE_AIRSPACE => new AirspaceCollector(this.sectorElements, this.outputGroups),
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS => new ActiveRunwaysCollector(this.sectorElements),
                _ => throw new ArgumentException("No element collector for section " + section)
            };
        }
    }
}
