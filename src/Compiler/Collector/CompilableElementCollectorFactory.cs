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
                OutputSectionKeys.FILE_HEADER => new HeaderCollector(sectorElements),
                // SCT Sections
                OutputSectionKeys.SCT_COLOUR_DEFS => new ColoursCollector(sectorElements),
                OutputSectionKeys.SCT_INFO => new InfoCollector(sectorElements),
                OutputSectionKeys.SCT_AIRPORT => new AirportsCollector(sectorElements),
                OutputSectionKeys.SCT_RUNWAY => new RunwaysCollector(sectorElements),
                OutputSectionKeys.SCT_VOR => new VorsCollector(sectorElements),
                OutputSectionKeys.SCT_NDB => new NdbsCollector(sectorElements),
                OutputSectionKeys.SCT_FIXES => new FixesCollector(sectorElements),
                OutputSectionKeys.SCT_GEO => new GeoCollector(sectorElements, outputGroups),
                OutputSectionKeys.SCT_LOW_AIRWAY => new LowAirwaysCollector(sectorElements),
                OutputSectionKeys.SCT_HIGH_AIRWAY => new HighAirwaysCollector(sectorElements),
                OutputSectionKeys.SCT_ARTCC => new ArtccCollector(sectorElements),
                OutputSectionKeys.SCT_ARTCC_LOW => new LowArtccCollector(sectorElements),
                OutputSectionKeys.SCT_ARTCC_HIGH => new HighArtccCollector(sectorElements),
                OutputSectionKeys.SCT_SID => new SidsCollector(sectorElements, outputGroups),
                OutputSectionKeys.SCT_STAR => new StarsCollector(sectorElements, outputGroups),
                OutputSectionKeys.SCT_LABELS => new LabelsCollector(sectorElements, outputGroups),
                OutputSectionKeys.SCT_REGIONS => new RegionsCollector(sectorElements),
                // ESE sections.
                OutputSectionKeys.ESE_POSITIONS => new PositionsCollector(sectorElements, outputGroups),
                OutputSectionKeys.ESE_FREETEXT => new FreetextCollector(sectorElements, outputGroups),
                OutputSectionKeys.ESE_SIDSSTARS => new SidStarsCollector(sectorElements),
                OutputSectionKeys.ESE_AIRSPACE => new AirspaceCollector(sectorElements, outputGroups),
                OutputSectionKeys.ESE_GROUND_NETWORK => new GroundNetworkCollector(sectorElements),
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS => new ActiveRunwaysCollector(sectorElements),
                OutputSectionKeys.ESE_RADAR => new RadarCollector(sectorElements),
                _ => throw new ArgumentException("No element collector for section " + section)
            };
        }
    }
}
