using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class EnrouteConfigFileSections
    {
        public static readonly List<ConfigFileSection> ConfigFileSections = new()
        {
            new ConfigFileSection("sector_lines", InputDataType.ESE_SECTORLINES, "Sector Lines"),
            new ConfigFileSection("ownership", InputDataType.ESE_OWNERSHIP, "Ownership"),
            new ConfigFileSection("positions", InputDataType.ESE_POSITIONS, "Positions"),
        };
    }
}
