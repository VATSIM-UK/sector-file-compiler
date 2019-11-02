using System;
using System.Collections.Generic;

namespace Compiler.Output
{
    class SectionHeaders
    {
        public static readonly Dictionary<OutputSections, string> headers = new Dictionary<OutputSections, string>
        {
            { OutputSections.SCT_HEADER, null },
            { OutputSections.SCT_COLOUR_DEFS, null },
            { OutputSections.SCT_INFO, "[INFO]" },
            { OutputSections.SCT_AIRPORT, "[AIRPORT]" },
            { OutputSections.SCT_RUNWAY, "[RUNWAY]" },
            { OutputSections.SCT_VOR, "[VOR]" },
            { OutputSections.SCT_NDB, "[NDB]" },
            { OutputSections.SCT_FIXES, "[FIXES]" },
            { OutputSections.SCT_GEO, "[GEO]" },
            { OutputSections.SCT_LOW_AIRWAY, "[LOW AIRWAY]" },
            { OutputSections.SCT_HIGH_AIRWAY, "[HIGH AIRWAY]" },
            { OutputSections.SCT_ARTCC, "[ARTCC]" },
            { OutputSections.SCT_ARTCC_HIGH, "[ARTCC HIGH]" },
            { OutputSections.SCT_ARTCC_LOW, "[ARTCC LOW]" },
            { OutputSections.SCT_SID, "[SID]" },
            { OutputSections.SCT_STAR, "[STAR]" },
            { OutputSections.SCT_LABELS, "[LABELS]" },
            { OutputSections.SCT_REGIONS, "[REGIONS]" },
            { OutputSections.ESE_HEADER, null },
            { OutputSections.ESE_PREAMBLE, null },
            { OutputSections.ESE_POSITIONS, "[POSITIONS]" },
            { OutputSections.ESE_FREETEXT, "[FREETEXT]" },
            { OutputSections.ESE_SIDSSTARS, "[SIDSSTARS]" },
            { OutputSections.ESE_AIRSPACE, "[AIRSPACE]" },
        };
    }
}
