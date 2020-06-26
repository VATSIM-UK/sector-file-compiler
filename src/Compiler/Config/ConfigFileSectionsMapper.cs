using System.Collections.Generic;
using Compiler.Output;

namespace Compiler.Config
{
    public class ConfigFileSectionsMapper
    {
        private static readonly Dictionary<OutputSections, string> sectionMap = new Dictionary<OutputSections, string>
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
            { OutputSections.ESE_POSITIONS,  "ese_positions" },
            { OutputSections.ESE_FREETEXT, "ese_freetext" },
            { OutputSections.ESE_SIDSSTARS, "ese_sidsstars" },
            { OutputSections.ESE_AIRSPACE, "ese_airspace" },
        };

        private static readonly Dictionary<Subsections, string> subsectionMap = new Dictionary<Subsections, string>
        {
            { Subsections.ESE_AIRSPACE_SECTOR, "sector" },
            { Subsections.ESE_AIRSPACE_SECTORLINE, "sectorline" },
            { Subsections.ESE_AIRSPACE_COORDINATION, "coordination" },
        };

        public static readonly string invalidSection = "";

        public static bool ConfigSectionValid(string section)
        {
            foreach (KeyValuePair<OutputSections, string> outputSection in sectionMap)
            {
                if (outputSection.Value == section)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ConfigSubsectionValid(string subsection)
        {
            foreach (KeyValuePair<Subsections, string> subsections in subsectionMap)
            {
                if (subsections.Value == subsection)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetConfigSectionForOutputSection(OutputSections section)
        {
            return sectionMap.ContainsKey(section)
                ? sectionMap[section]
                : ConfigFileSectionsMapper.invalidSection;
        }

        public static Subsections GetSubsectionForConfigSubsection(string subsection)
        {
            foreach (KeyValuePair<Subsections, string> subsections in subsectionMap)
            {
                if (subsections.Value == subsection)
                {
                    return subsections.Key;
                }
            }

            return Subsections.DEFAULT;
        }
    }
}
