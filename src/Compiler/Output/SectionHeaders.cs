using System;
using System.Collections.Generic;

namespace Compiler.Output
{
    class SectionHeaders
    {
        public static readonly Dictionary<OutputSections, string> headers = new Dictionary<OutputSections, string>
        {
            { OutputSections.ESE_HEADER, null },
            { OutputSections.ESE_PREAMBLE, null },
            { OutputSections.ESE_POSITIONS, "[POSITIONS]" },
            { OutputSections.ESE_FREETEXT, "[FREETEXT]" },
            { OutputSections.ESE_SIDSSTARS, "[SIDSSTARS]" },
            { OutputSections.ESE_AIRSPACE, "[AIRSPACE]" },
        };
    }
}
