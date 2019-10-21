using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Output;

namespace Compiler.Output
{
    class SectionHeaders
    {
        public readonly Dictionary<OutputSections, string> headers = new Dictionary<OutputSections, string>
        {
            { OutputSections.ESE_PREAMBLE, null },
            { OutputSections.ESE_POSITIONS, "[POSITIONS]" },
            { OutputSections.ESE_FREETEXT, "[FREETEXT]" },
            { OutputSections.ESE_SIDSSTARS, "[SIDSSTARS]" },
            { OutputSections.ESE_AIRSPACE, "[AIRSPACE]" },
        };
    }
}
