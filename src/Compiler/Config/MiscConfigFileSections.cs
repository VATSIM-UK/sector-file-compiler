using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class MiscConfigFileSections
    {
        public static readonly List<ConfigFileSection> configFileSections = new List<ConfigFileSection>
        {
            new("agreements", InputDataType.ESE_AGREEMENTS, "Agreements"),
            new("freetext", InputDataType.ESE_FREETEXT, "Freetext"),
            new("colours", InputDataType.SCT_COLOUR_DEFINITIONS, "Colour Definitions"),
            new("info", InputDataType.SCT_INFO),
            new("file_headers", InputDataType.FILE_HEADERS),
            new ("pre_positions", InputDataType.ESE_PRE_POSITIONS),
            new("fixes", InputDataType.SCT_FIXES, "Fixes"),
            new("ndbs", InputDataType.SCT_NDBS, "NDBs"),
            new("vors", InputDataType.SCT_VORS, "VORs"),
            new("danger_areas", InputDataType.SCT_ARTCC, "Danger Areas"),
            new("artcc_low", InputDataType.SCT_ARTCC_LOW, "Low ARTCCs"),
            new("artcc_high", InputDataType.SCT_ARTCC_HIGH, "High ARTCCs"),
            new("lower_airways", InputDataType.SCT_LOWER_AIRWAYS, "Lower Airways"),
            new("upper_airways", InputDataType.SCT_UPPER_AIRWAYS, "Upper Airways"),
            new("sid_airspace", InputDataType.SCT_SIDS, "SID Airspace"),
            new("star_airspace", InputDataType.SCT_STARS, "STAR Airspace"),
            new("geo", InputDataType.SCT_GEO, "Geo"),
            new("regions", InputDataType.SCT_REGIONS, "Regions"),
        };

    }
}
