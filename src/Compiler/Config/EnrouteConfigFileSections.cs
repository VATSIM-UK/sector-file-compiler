using System.Collections.Generic;
using Compiler.Input;

namespace Compiler.Config
{
    public class EnrouteConfigFileSections
    {
        public static readonly List<ConfigFileSection> configFileSections = new List<ConfigFileSection>
        {
            new("sector_lines", InputDataType.ESE_SECTORLINES, "Sector Lines"),
            new("ownership", InputDataType.ESE_OWNERSHIP, "Ownership"),
            new("positions", InputDataType.ESE_POSITIONS, "Positions"),
        };

    }
}
