using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public class SectorDataReaderFactory
    {
        public static AbstractSectorDataReader Create(InputDataType dataType)
        {
            switch (dataType)
            {
                case InputDataType.ESE_AGREEMENTS:
                case InputDataType.ESE_FREETEXT:
                case InputDataType.ESE_OWNERSHIP:
                case InputDataType.ESE_POSITIONS:
                case InputDataType.ESE_PRE_POSITIONS:
                case InputDataType.ESE_SECTORLINES:
                case InputDataType.ESE_SIDS:
                case InputDataType.ESE_STARS:
                case InputDataType.RWY_ACTIVE_RUNWAY:
                    return new EseSectorDataReader();
                case InputDataType.SCT_AIRPORT_BASIC:
                case InputDataType.SCT_ARTCC:
                case InputDataType.SCT_ARTCC_HIGH:
                case InputDataType.SCT_ARTCC_LOW:
                case InputDataType.SCT_COLOUR_DEFINITIONS:
                case InputDataType.SCT_EXTENDED_CENTRELINES:
                case InputDataType.SCT_FIXES:
                case InputDataType.SCT_GEO:
                case InputDataType.SCT_INFO:
                case InputDataType.SCT_LABELS:
                case InputDataType.SCT_NDBS:
                case InputDataType.SCT_REGIONS:
                case InputDataType.SCT_RUNWAYS:
                case InputDataType.SCT_SIDS:
                case InputDataType.SCT_STARS:
                case InputDataType.SCT_LOWER_AIRWAYS:
                case InputDataType.SCT_UPPER_AIRWAYS:
                case InputDataType.SCT_VORS:
                    return new SctSectorDataReader();
                default:
                    throw new ArgumentException("Unknown data type for SectorDataReaderFactory");
            }
        }
    }
}
