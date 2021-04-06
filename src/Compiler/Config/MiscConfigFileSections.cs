using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class MiscConfigFileSections
    {
        public static readonly List<ConfigFileSection> ConfigFileSections = new()
        {
            new ConfigFileSection("agreements", InputDataType.ESE_AGREEMENTS, "Agreements"),
            new ConfigFileSection("freetext", InputDataType.ESE_FREETEXT, "Freetext"),
            new ConfigFileSection("colours", InputDataType.SCT_COLOUR_DEFINITIONS, "Colour Definitions"),
            new ConfigFileSection("info", InputDataType.SCT_INFO),
            new ConfigFileSection("file_headers", InputDataType.FILE_HEADERS),
            new ConfigFileSection("pre_positions", InputDataType.ESE_PRE_POSITIONS),
            new ConfigFileSection("fixes", InputDataType.SCT_FIXES, "Fixes"),
            new ConfigFileSection("ndbs", InputDataType.SCT_NDBS, "NDBs"),
            new ConfigFileSection("vors", InputDataType.SCT_VORS, "VORs"),
            new ConfigFileSection("danger_areas", InputDataType.SCT_ARTCC, "Danger Areas"),
            new ConfigFileSection("artcc_low", InputDataType.SCT_ARTCC_LOW, "Low ARTCCs"),
            new ConfigFileSection("artcc_high", InputDataType.SCT_ARTCC_HIGH, "High ARTCCs"),
            new ConfigFileSection("lower_airways", InputDataType.SCT_LOWER_AIRWAYS, "Lower Airways"),
            new ConfigFileSection("upper_airways", InputDataType.SCT_UPPER_AIRWAYS, "Upper Airways"),
            new ConfigFileSection("sid_airspace", InputDataType.SCT_SIDS, "SID Airspace"),
            new ConfigFileSection("star_airspace", InputDataType.SCT_STARS, "STAR Airspace"),
            new ConfigFileSection("geo", InputDataType.SCT_GEO, "Geo"),
            new ConfigFileSection("regions", InputDataType.SCT_REGIONS, "Regions"),
            new ConfigFileSection("active_runways", InputDataType.RWY_ACTIVE_RUNWAY, "Active Runways"),
            new ConfigFileSection("radars", InputDataType.ESE_RADAR2, "Radars"),
            new ConfigFileSection("radar_holes", InputDataType.ESE_RADAR_HOLE, "Radar Holes"),
        };
    }
}
