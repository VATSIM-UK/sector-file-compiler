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

        public IFileParser GetParserForSection(
            OutputSections section
        ) {
            switch (section)
            {
                case OutputSections.SCT_COLOUR_DEFS:
                    return new ColourParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_AIRPORT:
                    return new AirportParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_FIXES:
                    return new FixParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_VOR:
                    return new VorParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        new FrequencyParser(108, 117, 50),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_NDB:
                    return new NdbParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        new FrequencyParser(108, 950, 500),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_ARTCC:
                    return new ArtccParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        ArtccType.REGULAR,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_ARTCC_LOW:
                    return new ArtccParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        ArtccType.LOW,
                        this.sectorElements,
                        this.logger
                    );

                case OutputSections.SCT_ARTCC_HIGH:
                    return new ArtccParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        ArtccType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_LOW_AIRWAY:
                    return new AirwayParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        AirwayType.LOW,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_HIGH_AIRWAY:
                    return new AirwayParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        AirwayType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_SID:
                    return new SidStarRouteParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger,
                        SidStarType.SID
                    );
                case OutputSections.SCT_STAR:
                    return new SidStarRouteParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger,
                        SidStarType.STAR
                    );
                case OutputSections.SCT_GEO:
                    return new GeoParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_LABELS:
                    return new LabelParser(
                        this.GetMetadataParser(section),
                        new SctLabelLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_REGIONS:
                    return new RegionParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_INFO:
                    return new InfoParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.SCT_RUNWAY:
                    return new RunwayParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_PREAMBLE:
                    break;
                case OutputSections.ESE_POSITIONS:
                    return new EsePositionParser(
                        this.GetMetadataParser(section),
                        new EseLineParser(),
                        new VatsimRtfFrequencyParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_FREETEXT:
                    return new FreetextParser(
                        this.GetMetadataParser(section),
                        new EseLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_SIDSSTARS:
                    return new SidStarParser(
                        this.GetMetadataParser(section),
                        new EseLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case OutputSections.ESE_AIRSPACE:
                    return new AirspaceParser(
                        this.GetMetadataParser(section),
                        new SectorParser(
                                this.GetMetadataParser(section),
                                new EseLineParser(),
                                this.sectorElements,
                                this.logger
                        ),
                        new SectorlineParser(
                                this.GetMetadataParser(section),
                                new EseLineParser(),
                                this.sectorElements,
                                this.logger
                        ),
                        new CoordinationPointParser(
                                this.GetMetadataParser(section),
                                new EseLineParser(),
                                this.sectorElements,
                                this.logger
                        ),
                        new EseLineParser(),
                        this.logger
                    );
                case OutputSections.RWY_ACTIVE_RUNWAYS:
                    return new ActiveRunwayParser(
                        this.GetMetadataParser(section),
                        new EseLineParser(),
                        this.sectorElements,
                        this.logger
                    );
            }

            return new DefaultParser(this.GetMetadataParser(section));
        }

        private MetadataParser GetMetadataParser(OutputSections section)
        {
            return new MetadataParser(this.sectorElements, section);
        }
    }
}
