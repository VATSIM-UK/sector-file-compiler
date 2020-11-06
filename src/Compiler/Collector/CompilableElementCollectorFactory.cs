using System;
using System.Collections.Generic;
using System.Text;
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

        public ICompilableElementCollector GetCollectorForOutputSection(OutputSections section)
        {
            switch (section)
            {
                case OutputSections.SCT_INFO:
                    return new InfoCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_AIRPORT:
                    return new AirportsCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_RUNWAY:
                    return new RunwaysCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_VOR:
                    return new VorsCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_NDB:
                    return new NdbsCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_FIXES:
                    return new FixesCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_GEO:
                    return new GeoCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_LOW_AIRWAY:
                    return new HighAirwaysCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_HIGH_AIRWAY:
                    return new HighAirwaysCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_ARTCC:
                    return new ArtccCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_ARTCC_LOW:
                    return new LowArtccCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_ARTCC_HIGH:
                    return new HighArtccCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_SID:
                    return new SidsCollector(this.sectorElements, this.outputGroups);
                case OutputSections.SCT_REGIONS:
                    return new RegionsCollector(this.sectorElements, this.outputGroups);
                default:
                    break;
            }

            throw new ArgumentException("No element collector for section " + section.ToString());
        }
    }
}
