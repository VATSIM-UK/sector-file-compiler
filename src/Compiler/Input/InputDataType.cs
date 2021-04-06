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
        SCT_ARTCC,
        SCT_ARTCC_LOW,
        SCT_ARTCC_HIGH,
        SCT_COLOUR_DEFINITIONS,
        SCT_RUNWAY_CENTRELINES,
        SCT_FIXES,
        SCT_GEO,
        SCT_INFO,
        SCT_LABELS,
        SCT_LOWER_AIRWAYS,
        SCT_NDBS,
        SCT_REGIONS,
        SCT_RUNWAYS,
        SCT_SIDS,
        SCT_STARS,
        SCT_UPPER_AIRWAYS,
        SCT_VORS,

        // EuroScope ESE file
        ESE_AGREEMENTS,
        ESE_FREETEXT,
        ESE_OWNERSHIP,
        ESE_PRE_POSITIONS,
        ESE_POSITIONS,
        ESE_POSITIONS_MENTOR,
        ESE_SECTORLINES,
        ESE_SIDS,
        ESE_STARS,
        ESE_VRPS,
        ESE_GROUND_NETWORK,
        ESE_RADAR2,
        ESE_RADAR_HOLE,

        // EuroScope RWY file
        RWY_ACTIVE_RUNWAY,

        // Generic
        FILE_HEADERS,
    }
}
