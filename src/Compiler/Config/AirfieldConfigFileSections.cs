using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class AirfieldConfigFileSections
    {
        public static readonly List<ConfigFileSection> configFileSections = new List<ConfigFileSection>
        {
            new("active_runways", InputDataType.RWY_ACTIVE_RUNWAY, "Active Runways"),
            new ("airspace", InputDataType.SCT_ARTCC_LOW, "Airspace"),
            new ("basic", InputDataType.SCT_AIRPORT_BASIC, "Basic"),
            new("centrelines", InputDataType.SCT_EXTENDED_CENTRELINES, "Centrelines"),
            new("fixes", InputDataType.SCT_FIXES, "Fixes"),
            new("freetext", InputDataType.ESE_FREETEXT, "Freetext"),
            new("ownership", InputDataType.ESE_OWNERSHIP, "Ownership"),
            new("positions", InputDataType.ESE_POSITIONS, "Positions"),
            new("positions_mentor", InputDataType.ESE_POSITIONS_MENTOR, "Mentoring Positions"),
            new("sid_airspace", InputDataType.SCT_SIDS, "SID Airspace"),
            new("runways", InputDataType.SCT_RUNWAYS, "Runways`"),
            new("sectors", InputDataType.ESE_SECTORLINES, "Sectors"),
            new("sids", InputDataType.ESE_SIDS, "SIDs"),
            new("stars", InputDataType.ESE_STARS, "STARs"),
            new("smr.geo", InputDataType.SCT_GEO, "SMR Geo"),
            new("smr.regions", InputDataType.SCT_REGIONS, "SMR Regions"),
            new("smr.labels", InputDataType.SCT_LABELS, "SMR Labels"),
            new("ground_map.geo", InputDataType.SCT_GEO, "Group Map Geo"),
            new("ground_map.regions", InputDataType.SCT_REGIONS, "Ground Map Regions"),
            new("ground_map.labels", InputDataType.SCT_LABELS, "Ground Map Labels"),
            new("vrps", InputDataType.ESE_VRPS, "VRPs"),
        };
    }
}
