using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class AirfieldConfigFileSections
    {
        public static readonly List<ConfigFileSection> ConfigFileSections = new()
        {
            new ConfigFileSection("active_runways", InputDataType.RWY_ACTIVE_RUNWAY, "Active Runways"),
            new ConfigFileSection("airspace", InputDataType.SCT_ARTCC_LOW, "Airspace"),
            new ConfigFileSection("basic", InputDataType.SCT_AIRPORT_BASIC, "Basic"),
            new ConfigFileSection("centrelines", InputDataType.SCT_RUNWAY_CENTRELINES, "Centrelines"),
            new ConfigFileSection("geo", InputDataType.SCT_GEO, "Geo"),
            new ConfigFileSection("fixes", InputDataType.SCT_FIXES, "Fixes"),
            new ConfigFileSection("freetext", InputDataType.ESE_FREETEXT, "Freetext"),
            new ConfigFileSection("ownership", InputDataType.ESE_OWNERSHIP, "Ownership"),
            new ConfigFileSection("positions", InputDataType.ESE_POSITIONS, "Positions"),
            new ConfigFileSection("positions_mentor", InputDataType.ESE_POSITIONS_MENTOR, "Mentoring Positions"),
            new ConfigFileSection("sid_airspace", InputDataType.SCT_SIDS, "SID Airspace"),
            new ConfigFileSection("star_airspace", InputDataType.SCT_STARS, "STAR Airspace"),
            new ConfigFileSection("runways", InputDataType.SCT_RUNWAYS, "Runways`"),
            new ConfigFileSection("sectors", InputDataType.ESE_SECTORLINES, "Sectors"),
            new ConfigFileSection("sids", InputDataType.ESE_SIDS, "SIDs"),
            new ConfigFileSection("stars", InputDataType.ESE_STARS, "STARs"),
            new ConfigFileSection("smr.geo", InputDataType.SCT_GEO, "SMR Geo"),
            new ConfigFileSection("smr.regions", InputDataType.SCT_REGIONS, "SMR Regions"),
            new ConfigFileSection("smr.labels", InputDataType.SCT_LABELS, "SMR Labels"),
            new ConfigFileSection("ground_map.geo", InputDataType.SCT_GEO, "Group Map Geo"),
            new ConfigFileSection("ground_map.regions", InputDataType.SCT_REGIONS, "Ground Map Regions"),
            new ConfigFileSection("ground_map.labels", InputDataType.SCT_LABELS, "Ground Map Labels"),
            new ConfigFileSection("vrps", InputDataType.ESE_VRPS, "VRPs"),
            new ConfigFileSection("ground_network", InputDataType.ESE_GROUND_NETWORK, "Ground Network"),
        };
    }
}
