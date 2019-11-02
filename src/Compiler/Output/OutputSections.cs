namespace Compiler.Output
{
    public enum OutputSections
    {
        // SCT
        SCT_HEADER,
        SCT_COLOUR_DEFS,
        SCT_INFO,
        SCT_AIRPORT,
        SCT_RUNWAY,
        SCT_VOR,
        SCT_NDB,
        SCT_FIXES,
        SCT_GEO,
        SCT_LOW_AIRWAY,
        SCT_HIGH_AIRWAY,
        SCT_ARTCC,
        SCT_ARTCC_HIGH,
        SCT_ARTCC_LOW,
        SCT_SID,
        SCT_STAR,
        SCT_LABELS,
        SCT_REGIONS,

        // ESE
        ESE_HEADER,
        ESE_PREAMBLE,
        ESE_POSITIONS,
        ESE_FREETEXT,
        ESE_SIDSSTARS,
        ESE_AIRSPACE,
    }
}
