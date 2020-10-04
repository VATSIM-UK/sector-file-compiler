using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Config
{
    public class MiscConfigFileSections
    {
        public static readonly List<ConfigFileSection> configFileSections = new List<ConfigFileSection>
        {
            new ConfigFileSection("agreements", InputDataType.ESE_AGREEMENTS),
            new ConfigFileSection("freetext", InputDataType.ESE_FREETEXT),
            new ConfigFileSection("colours", InputDataType.SCT_COLOUR_DEFINITIONS),
            new ConfigFileSection("info", InputDataType.SCT_INFO),
            new ConfigFileSection("file_headers", InputDataType.FILE_HEADERS),
            new ConfigFileSection("pre_positions", InputDataType.ESE_PRE_POSITIONS),
            new ConfigFileSection("fixes", InputDataType.SCT_FIXES),
            new ConfigFileSection("ndbs", InputDataType.SCT_NDBS),
            new ConfigFileSection("vors", InputDataType.SCT_VORS),
            new ConfigFileSection("danger_areas", InputDataType.SCT_ARTCC),
            new ConfigFileSection("artcc_low", InputDataType.SCT_ARTCC_LOW),
            new ConfigFileSection("artcc_high", InputDataType.SCT_ARTCC_HIGH),
            new ConfigFileSection("lower_airways", InputDataType.SCT_LOWER_AIRWAYS),
            new ConfigFileSection("upper_airways", InputDataType.SCT_UPPER_AIRWAYS),
            new ConfigFileSection("sid_airspace", InputDataType.SCT_SIDS),
            new ConfigFileSection("star_airspace", InputDataType.SCT_STARS),
            new ConfigFileSection("geo", InputDataType.SCT_GEO),
            new ConfigFileSection("regions", InputDataType.SCT_REGIONS),
        };

    }
}
