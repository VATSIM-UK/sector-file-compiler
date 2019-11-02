using System.Collections.Generic;
using Compiler.Output;

namespace Compiler.Input
{
    public class ConfigFileSectionsMapper
    {
        public static readonly Dictionary<OutputSections, string> sectionMap = new Dictionary<OutputSections, string>
        {
            { OutputSections.SCT_HEADER, "sct_header" },
            { OutputSections.SCT_COLOUR_DEFS, "sct_colour_defs" },
            { OutputSections.SCT_INFO, "sct_info" },
            { OutputSections.SCT_AIRPORT, "sct_airport" },
            { OutputSections.SCT_RUNWAY, "sct_runway" },
            { OutputSections.SCT_VOR, "sct_vor" },
            { OutputSections.SCT_NDB, "sct_ndb" },
            { OutputSections.SCT_FIXES, "sct_fixes" },
            { OutputSections.SCT_GEO, "sct_geo" },
            { OutputSections.SCT_LOW_AIRWAY, "sct_low_airway" },
            { OutputSections.SCT_HIGH_AIRWAY, "sct_high_airway" },
            { OutputSections.SCT_ARTCC, "sct_artcc" },
            { OutputSections.SCT_ARTCC_HIGH, "sct_artcc_high" },
            { OutputSections.SCT_ARTCC_LOW, "sct_artcc_low" },
            { OutputSections.SCT_SID, "sct_sid" },
            { OutputSections.SCT_STAR, "sct_star" },
            { OutputSections.SCT_LABELS, "sct_labels" },
            { OutputSections.SCT_REGIONS, "sct_regions" },
            { OutputSections.ESE_HEADER, "ese_header" },
            { OutputSections.ESE_PREAMBLE, "ese_preamble" },
            { OutputSections.ESE_POSITIONS,  "positions" },
            { OutputSections.ESE_FREETEXT, "freetext" },
            { OutputSections.ESE_SIDSSTARS, "sidsstars" },
            { OutputSections.ESE_AIRSPACE, "airspace" },
        };
    }
}
