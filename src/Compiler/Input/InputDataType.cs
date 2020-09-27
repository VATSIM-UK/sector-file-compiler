using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    /*
     * All the different types of input data we can expect to see throughout
     * the files.
     */
    public enum InputDataType
    {
        // SCT2 format data
        SCT_AIRPORT_BASIC,
        SCT_LOWER_AIRWAYS,
        SCT_UPPER_AIRWAYS,
        SCT_ARTCC,
        SCT_ARTCC_LOW,
        SCT_ARTCC_HIGH,
        SCT_FIXES,
        SCT_GEO,
        SCT_NDBS,
        SCT_REGIONS,
        SCT_RUNWAYS,
        SCT_SIDS,
        SCT_STARS,
        SCT_VORS,

        // EuroScope ESE file
        ESE_AGREEMENTS,
        ESE_FREETEXT,
        ESE_OWNERSHIP,
        ESE_POSITIONS,
        ESE_SECTORLINES,
        ESE_SIDS,
        ESE_STARS,

        // EuroScope RWY file
        RWY_ACTIVE_RUNWAY,
    }
}
