using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Output;

namespace Compiler.Input
{
    public class ConfigFileSectionsMapper
    {
        public readonly Dictionary<OutputSections, string> sectionMap = new Dictionary<OutputSections, string>
        {
            { OutputSections.ESE_PREAMBLE, "ese_preamble" },
            { OutputSections.ESE_POSITIONS,  "positions" },
            { OutputSections.ESE_FREETEXT, "freetext" },
            { OutputSections.ESE_SIDSSTARS, "sidsstars" },
            { OutputSections.ESE_AIRSPACE, "airspace" },
        };
    }
}
