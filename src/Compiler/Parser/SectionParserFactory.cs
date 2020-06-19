using Compiler.Output;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class SectionParserFactory
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger logger;

        public SectionParserFactory(SectorElementCollection sectorElements, IEventLogger logger)
        {
            this.sectorElements = sectorElements;
            this.logger = logger;
        }

        public AbstractSectorElementParser GetParserForSection(
            OutputSections section,
            Subsections subsection = Subsections.DEFAULT
        ) {
            switch (section)
            {
                case OutputSections.SCT_COLOUR_DEFS:
                    return new ColourParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_AIRPORT:
                    return new AirportParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_FIXES:
                    return new FixParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_VOR:
                    return new VorParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        new FrequencyParser(108, 117, 50),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_NDB:
                    return new NdbParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        new FrequencyParser(108, 950, 500),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_ARTCC:
                    return new ArtccParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        ArtccType.REGULAR,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_ARTCC_LOW:
                    return new ArtccParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        ArtccType.LOW,
                        this.sectorElements,
                        this.logger
                    );

                case OutputSections.SCT_ARTCC_HIGH:
                    return new ArtccParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        ArtccType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_LOW_AIRWAY:
                    return new AirwayParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        AirwayType.LOW,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_HIGH_AIRWAY:
                    return new AirwayParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        AirwayType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_SID:
                    return new SidStarRouteParser(
                        this.GetMetadataParser(section, subsection),
                        new RouteSegmentsLineParser(),
                        this.sectorElements,
                        this.logger,
                        SidStarType.SID
                    );
                case OutputSections.SCT_STAR:
                    return new SidStarRouteParser(
                        this.GetMetadataParser(section, subsection),
                        new RouteSegmentsLineParser(),
                        this.sectorElements,
                        this.logger,
                        SidStarType.STAR
                    );
                case OutputSections.SCT_GEO:
                    return new GeoParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_LABELS:
                    return new LabelParser(
                        this.GetMetadataParser(section, subsection),
                        new SctLabelLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_REGIONS:
                    return new RegionParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_INFO:
                    return new InfoParser(
                        this.GetMetadataParser(section, subsection),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_PREAMBLE:
                    break;
                case OutputSections.ESE_POSITIONS:
                    return new EsePositionParser(
                        this.GetMetadataParser(section, subsection),
                        new EseLineParser(),
                        new VatsimRtfFrequencyParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_FREETEXT:
                    return new FreetextParser(
                        this.GetMetadataParser(section, subsection),
                        new EseLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_SIDSSTARS:
                    return new SidStarParser(
                        this.GetMetadataParser(section, subsection),
                        new EseLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_AIRSPACE:
                    switch (subsection)
                    {
                        case Subsections.ESE_AIRSPACE_COORDINATION:
                            return new CoordinationPointParser(
                                this.GetMetadataParser(section, subsection),
                                new EseLineParser(),
                                this.sectorElements,
                                this.logger
                            );
                        case Subsections.ESE_AIRSPACE_SECTOR:
                            break;
                        case Subsections.ESE_AIRSPACE_SECTORLINE:
                            break;
                    }

                    break;
            }

            return new DefaultParser(this.GetMetadataParser(section, subsection));
        }

        private MetadataParser GetMetadataParser(OutputSections section, Subsections subsection)
        {
            return new MetadataParser(this.sectorElements, section, subsection);
        }
    }
}
