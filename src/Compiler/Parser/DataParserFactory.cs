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

        public ISectorDataParser GetParserForFile(AbstractSectorDataFile file)
        {
            return file.DataType switch
            {
                InputDataType.SCT_COLOUR_DEFINITIONS => new ColourParser(this.sectorElements, this.logger),
                InputDataType.SCT_AIRPORT_BASIC => new AirportParser(this.sectorElements, this.logger),
                InputDataType.SCT_FIXES => new FixParser(this.sectorElements, this.logger),
                InputDataType.SCT_VORS => new VorParser(
                    new FrequencyParser(108, 117, 50),
                    this.sectorElements,
                    this.logger
                    ),
                InputDataType.SCT_NDBS => new NdbParser(new FrequencyParser(108, 950, 500), this.sectorElements,
                    this.logger),
                InputDataType.SCT_ARTCC => new ArtccParser(ArtccType.REGULAR, this.sectorElements, this.logger),
                InputDataType.SCT_ARTCC_LOW => new ArtccParser(ArtccType.LOW, this.sectorElements, this.logger),
                InputDataType.SCT_ARTCC_HIGH => new ArtccParser(ArtccType.HIGH, this.sectorElements, this.logger),
                InputDataType.SCT_LOWER_AIRWAYS => new AirwayParser(AirwayType.LOW, this.sectorElements, this.logger),
                InputDataType.SCT_UPPER_AIRWAYS => new AirwayParser(AirwayType.HIGH, this.sectorElements, this.logger),
                InputDataType.SCT_SIDS => new SidStarRouteParser(this.sectorElements, this.logger, SidStarType.SID),
                InputDataType.SCT_STARS => new SidStarRouteParser(this.sectorElements, this.logger, SidStarType.STAR),
                InputDataType.SCT_GEO => new GeoParser(this.sectorElements, this.logger),
                InputDataType.SCT_LABELS => new LabelParser(this.sectorElements, this.logger),
                InputDataType.SCT_REGIONS => new RegionParser(this.sectorElements, this.logger),
                InputDataType.SCT_INFO => new InfoParser(this.sectorElements, this.logger),
                InputDataType.SCT_RUNWAYS => new RunwayParser(this.sectorElements, this.logger),
                InputDataType.ESE_POSITIONS => new EsePositionParser(
                    new VatsimRtfFrequencyParser(),
                    this.sectorElements,
                    this.logger,
                    PositionOrder.CONTROLLER_POSITION
                ),
                InputDataType.ESE_POSITIONS_MENTOR => new EsePositionParser(
                    new VatsimRtfFrequencyParser(),
                    this.sectorElements,
                    this.logger,
                    PositionOrder.MENTOR_POSITION
                ),
                InputDataType.ESE_FREETEXT => new FreetextParser(this.sectorElements, this.logger),
                InputDataType.ESE_SIDS => new SidStarParser(this.sectorElements, this.logger),
                InputDataType.ESE_STARS => new SidStarParser(this.sectorElements, this.logger),
                InputDataType.ESE_SECTORLINES => new SectorlineParser(this.sectorElements, this.logger),
                InputDataType.ESE_AGREEMENTS => new CoordinationPointParser(this.sectorElements, this.logger),
                InputDataType.ESE_OWNERSHIP => new SectorParser(this.sectorElements, this.logger),
                InputDataType.RWY_ACTIVE_RUNWAY => new ActiveRunwayParser(this.sectorElements, this.logger),
                InputDataType.FILE_HEADERS => new HeaderParser(this.sectorElements, this.logger),
                InputDataType.ESE_PRE_POSITIONS => new EsePositionParser(
                    new EuroscopeNoFrequencyParser(),
                    this.sectorElements,
                    this.logger,
                    PositionOrder.PRE_POSITION
                ),
                InputDataType.ESE_VRPS => new VrpParser(this.sectorElements, this.logger),
                _ => throw new NotImplementedException(
                    string.Format("Parser not not implemented for input data type {0}", file.DataType.ToString()))
            };
        }
    }
}
