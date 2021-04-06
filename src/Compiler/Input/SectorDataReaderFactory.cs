using System;

namespace Compiler.Input
{
    public class SectorDataReaderFactory
    {
        public static AbstractSectorDataReader Create(InputDataType dataType)
        {
            return dataType switch
            {
                InputDataType.ESE_AGREEMENTS => new EseSectorDataReader(),
                InputDataType.ESE_FREETEXT => new EseSectorDataReader(),
                InputDataType.ESE_GROUND_NETWORK => new EseSectorDataReader(),
                InputDataType.ESE_POSITIONS_MENTOR => new EseSectorDataReader(),
                InputDataType.ESE_OWNERSHIP => new EseSectorDataReader(),
                InputDataType.ESE_POSITIONS => new EseSectorDataReader(),
                InputDataType.ESE_PRE_POSITIONS => new EseSectorDataReader(),
                InputDataType.ESE_SECTORLINES => new EseSectorDataReader(),
                InputDataType.ESE_SIDS => new EseSectorDataReader(),
                InputDataType.ESE_STARS => new EseSectorDataReader(),
                InputDataType.ESE_VRPS => new EseSectorDataReader(),
                InputDataType.ESE_RADAR2 => new EseSectorDataReader(),
                InputDataType.ESE_RADAR_HOLE => new EseSectorDataReader(),
                InputDataType.RWY_ACTIVE_RUNWAY => new EseSectorDataReader(),
                InputDataType.SCT_AIRPORT_BASIC => new SctSectorDataReader(),
                InputDataType.SCT_ARTCC => new SctSectorDataReader(),
                InputDataType.SCT_ARTCC_HIGH => new SctSectorDataReader(),
                InputDataType.SCT_ARTCC_LOW => new SctSectorDataReader(),
                InputDataType.SCT_COLOUR_DEFINITIONS => new SctSectorDataReader(),
                InputDataType.SCT_RUNWAY_CENTRELINES => new SctSectorDataReader(),
                InputDataType.SCT_FIXES => new SctSectorDataReader(),
                InputDataType.SCT_GEO => new SctSectorDataReader(),
                InputDataType.SCT_INFO => new SctSectorDataReader(),
                InputDataType.SCT_LABELS => new SctSectorDataReader(),
                InputDataType.SCT_NDBS => new SctSectorDataReader(),
                InputDataType.SCT_REGIONS => new SctSectorDataReader(),
                InputDataType.SCT_RUNWAYS => new SctSectorDataReader(),
                InputDataType.SCT_SIDS => new SctSectorDataReader(),
                InputDataType.SCT_STARS => new SctSectorDataReader(),
                InputDataType.SCT_LOWER_AIRWAYS => new SctSectorDataReader(),
                InputDataType.SCT_UPPER_AIRWAYS => new SctSectorDataReader(),
                InputDataType.SCT_VORS => new SctSectorDataReader(),
                InputDataType.FILE_HEADERS => new FileHeaderDataReader(),
                _ => throw new ArgumentException("Unknown data type for SectorDataReaderFactory")
            };
        }
    }
}
