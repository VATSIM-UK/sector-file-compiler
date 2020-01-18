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

        public AbstractSectorElementParser GetParserForSection(OutputSections section)
        {
            switch (section)
            {
                case OutputSections.SCT_COLOUR_DEFS:
                    return new ColourParser(
                        this.GetMetadataParser(section),
                        new StandardSctLineParser(),
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
                        new RouteSegmentsLineParser(),
                        this.sectorElements,
                        this.logger,
                        SidStarType.SID
                    );
                case OutputSections.SCT_STAR:
                    return new SidStarRouteParser(
                        this.GetMetadataParser(section),
                        new RouteSegmentsLineParser(),
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
                case OutputSections.ESE_PREAMBLE:
                    break;
                case OutputSections.ESE_POSITIONS:
                    break;
                case OutputSections.ESE_FREETEXT:
                    break;
                case OutputSections.ESE_AIRSPACE:
                    break;
            }

            return new DefaultParser(this.GetMetadataParser(section));
        }

        private MetadataParser GetMetadataParser(OutputSections section)
        {
            return new MetadataParser(this.sectorElements, section);
        }
    }
}
