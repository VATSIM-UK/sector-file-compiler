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
                InputDataType.SCT_COLOUR_DEFINITIONS => new ColourParser(sectorElements, logger),
                InputDataType.SCT_AIRPORT_BASIC => new AirportParser(sectorElements, logger),
                InputDataType.SCT_FIXES => new FixParser(sectorElements, logger),
                InputDataType.SCT_VORS => new VorParser(
                    new FrequencyParser(108, 117, 50),
                    sectorElements,
                    logger
                ),
                InputDataType.SCT_NDBS => new NdbParser(new FrequencyParser(108, 950, 500), sectorElements, logger),
                InputDataType.SCT_ARTCC => new ArtccParser(ArtccType.REGULAR, sectorElements, logger),
                InputDataType.SCT_ARTCC_LOW => new ArtccParser(ArtccType.LOW, sectorElements, logger),
                InputDataType.SCT_ARTCC_HIGH => new ArtccParser(ArtccType.HIGH, sectorElements, logger),
                InputDataType.SCT_LOWER_AIRWAYS => new AirwayParser(AirwayType.LOW, sectorElements, logger),
                InputDataType.SCT_UPPER_AIRWAYS => new AirwayParser(AirwayType.HIGH, sectorElements, logger),
                InputDataType.SCT_SIDS => new SidStarRouteParser(sectorElements, logger, SidStarType.SID),
                InputDataType.SCT_STARS => new SidStarRouteParser(sectorElements, logger, SidStarType.STAR),
                InputDataType.SCT_GEO => new GeoParser(sectorElements, logger),
                InputDataType.SCT_LABELS => new LabelParser(sectorElements, logger),
                InputDataType.SCT_REGIONS => new RegionParser(sectorElements, logger),
                InputDataType.SCT_INFO => new InfoParser(sectorElements, logger),
                InputDataType.SCT_RUNWAYS => new RunwayParser(sectorElements, logger),
                InputDataType.SCT_RUNWAY_CENTRELINES => new RunwayCentrelineParser(sectorElements, logger),
                InputDataType.ESE_POSITIONS => new EsePositionParser(
                    new VatsimRtfFrequencyParser(),
                    sectorElements,
                    logger,
                    PositionOrder.CONTROLLER_POSITION
                ),
                InputDataType.ESE_POSITIONS_MENTOR => new EsePositionParser(
                    new VatsimRtfFrequencyParser(),
                    sectorElements,
                    logger,
                    PositionOrder.MENTOR_POSITION
                ),
                InputDataType.ESE_FREETEXT => new FreetextParser(sectorElements, logger),
                InputDataType.ESE_SIDS => new SidStarParser(sectorElements, logger),
                InputDataType.ESE_STARS => new SidStarParser(sectorElements, logger),
                InputDataType.ESE_SECTORLINES => new SectorlineParser(sectorElements, logger),
                InputDataType.ESE_AGREEMENTS => new CoordinationPointParser(sectorElements, logger),
                InputDataType.ESE_OWNERSHIP => new SectorParser(sectorElements, logger),
                InputDataType.RWY_ACTIVE_RUNWAY => new ActiveRunwayParser(sectorElements, logger),
                InputDataType.FILE_HEADERS => new HeaderParser(sectorElements, logger),
                InputDataType.ESE_PRE_POSITIONS => new EsePositionParser(
                    new EuroscopeNoFrequencyParser(),
                    sectorElements,
                    logger,
                    PositionOrder.PRE_POSITION
                ),
                InputDataType.ESE_VRPS => new VrpParser(sectorElements, logger),
                InputDataType.ESE_GROUND_NETWORK => new GroundNetworkParser(sectorElements, logger),
                InputDataType.ESE_RADAR2 => new RadarParser(sectorElements, logger),
                InputDataType.ESE_RADAR_HOLE => new RadarHoleParser(sectorElements, logger),
                _ => throw new NotImplementedException(
                    $"Parser not not implemented for input data type {file.DataType.ToString()}")
            };
        }
    }
}
