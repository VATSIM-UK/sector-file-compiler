using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Config
{
    public class AirfieldConfigFileSections
    {
        public static readonly List<ConfigFileSection> configFileSections = new List<ConfigFileSection>
        {
            new ConfigFileSection("active_runways", InputDataType.RWY_ACTIVE_RUNWAY),
            new ConfigFileSection("airspace", InputDataType.SCT_ARTCC_LOW),
            new ConfigFileSection("basic", InputDataType.SCT_AIRPORT_BASIC),
            new ConfigFileSection("centrelines", InputDataType.SCT_EXTENDED_CENTRELINES),
            new ConfigFileSection("fixes", InputDataType.SCT_FIXES),
            new ConfigFileSection("freetext", InputDataType.ESE_FREETEXT),
            new ConfigFileSection("ownership", InputDataType.ESE_OWNERSHIP),
            new ConfigFileSection("positions", InputDataType.ESE_POSITIONS),
            new ConfigFileSection("mentor_positions", InputDataType.ESE_POSITIONS),
            new ConfigFileSection("sid_airspace", InputDataType.SCT_SIDS),
            new ConfigFileSection("runways", InputDataType.SCT_RUNWAYS),
            new ConfigFileSection("sectors", InputDataType.ESE_SECTORLINES),
            new ConfigFileSection("sids", InputDataType.ESE_SIDS),
            new ConfigFileSection("stars", InputDataType.ESE_STARS),
            new ConfigFileSection("smr.geo", InputDataType.SCT_GEO),
            new ConfigFileSection("smr.regions", InputDataType.SCT_REGIONS),
            new ConfigFileSection("smr.labels", InputDataType.SCT_LABELS),
            new ConfigFileSection("ground_map.geo", InputDataType.SCT_GEO),
            new ConfigFileSection("ground_map.regions", InputDataType.SCT_REGIONS),
            new ConfigFileSection("ground_map.labels", InputDataType.SCT_LABELS),
        };

    }
}
