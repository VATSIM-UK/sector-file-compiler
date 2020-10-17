using Compiler.Output;
using Compiler.Event;
using Compiler.Model;
using Compiler.Input;
using System;

namespace Compiler.Parser
{
    public class DataParserFactory
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger logger;

        public DataParserFactory(SectorElementCollection sectorElements, IEventLogger logger)
        {
            this.sectorElements = sectorElements;
            this.logger = logger;
        }

        public ISectorDataParser GetParserForFile(AbstractSectorDataFile file) {
            switch (file.DataType)
            {
                case InputDataType.SCT_COLOUR_DEFINITIONS:
                    return new ColourParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_AIRPORT_BASIC:
                    return new AirportParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_FIXES:
                    return new FixParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_VORS:
                    return new VorParser(
                        new FrequencyParser(108, 117, 50),
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_NDBS:
                    return new NdbParser(
                        new FrequencyParser(108, 950, 500),
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_ARTCC:
                    return new ArtccParser(
                        ArtccType.REGULAR,
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_ARTCC_LOW:
                    return new ArtccParser(
                        ArtccType.LOW,
                        this.sectorElements,
                        this.logger
                    );

                case InputDataType.SCT_ARTCC_HIGH:
                    return new ArtccParser(
                        ArtccType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_LOWER_AIRWAYS:
                    return new AirwayParser(
                        AirwayType.LOW,
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_UPPER_AIRWAYS:
                    return new AirwayParser(
                        AirwayType.HIGH,
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_SIDS:
                    return new SidStarRouteParser(
                        this.sectorElements,
                        this.logger,
                        SidStarType.SID
                    );
                case InputDataType.SCT_STARS:
                    return new SidStarRouteParser(
                        this.sectorElements,
                        this.logger,
                        SidStarType.STAR
                    );
                case InputDataType.SCT_GEO:
                    return new GeoParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_LABELS:
                    return new LabelParser(
                        this.GetMetadataParser(section),
                        new SctLabelLineParser(),
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_REGIONS:
                    return new RegionParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_INFO:
                    return new InfoParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.SCT_RUNWAYS:
                    return new RunwayParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_POSITIONS:
                    return new EsePositionParser(
                        new VatsimRtfFrequencyParser(),
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_FREETEXT:
                    return new FreetextParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_SIDS:
                    return new SidStarParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_SIDS:
                    return new SidStarParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_SECTORLINES:
                    return new SectorlineParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_AGREEMENTS:
                    return new CoordinationPointParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.ESE_OWNERSHIP:
                    return new SectorParser(
                        this.sectorElements,
                        this.logger
                    );
                case InputDataType.RWY_ACTIVE_RUNWAY:
                    return new ActiveRunwayParser(
                        this.sectorElements,
                        this.logger
                    );
            }

            throw new NotImplementedException(
                string.Format(
                    "Parser not not implented for input data type {0}",
                    file.DataType.ToString()
                )
            );
        }
    }
}
