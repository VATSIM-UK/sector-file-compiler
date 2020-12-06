using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Compiler.Output
{
    public class SctOutput : AbstractOutputFile
    {
        public SctOutput(TextWriter outputFile)
            : base(outputFile)
        {

        }

        public override OutputSections[] GetOutputSections()
        {
            return new OutputSections[] {
                OutputSections.SCT_HEADER,
                OutputSections.SCT_COLOUR_DEFS,
                OutputSections.SCT_INFO,
                OutputSections.SCT_AIRPORT,
                OutputSections.SCT_RUNWAY,
                OutputSections.SCT_VOR,
                OutputSections.SCT_NDB,
                OutputSections.SCT_FIXES,
                OutputSections.SCT_GEO,
                OutputSections.SCT_LOW_AIRWAY,
                OutputSections.SCT_HIGH_AIRWAY,
                OutputSections.SCT_ARTCC,
                OutputSections.SCT_ARTCC_HIGH,
                OutputSections.SCT_ARTCC_LOW,
                OutputSections.SCT_SID,
                OutputSections.SCT_STAR,
                OutputSections.SCT_LABELS,
                OutputSections.SCT_REGIONS
            };
        }
    }
}
