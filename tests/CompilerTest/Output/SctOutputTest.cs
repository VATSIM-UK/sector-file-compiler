using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Output;
using Moq;
using Xunit;

namespace CompilerTest.Output
{
    public class SctOutputTest
    {
        [Fact]
        public void TestItHasOutputSections()
        {
            OutputSectionKeys[] expected = {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.SCT_COLOUR_DEFS,
                OutputSectionKeys.SCT_INFO,
                OutputSectionKeys.SCT_AIRPORT,
                OutputSectionKeys.SCT_RUNWAY,
                OutputSectionKeys.SCT_VOR,
                OutputSectionKeys.SCT_NDB,
                OutputSectionKeys.SCT_FIXES,
                OutputSectionKeys.SCT_GEO,
                OutputSectionKeys.SCT_LOW_AIRWAY,
                OutputSectionKeys.SCT_HIGH_AIRWAY,
                OutputSectionKeys.SCT_ARTCC,
                OutputSectionKeys.SCT_ARTCC_HIGH,
                OutputSectionKeys.SCT_ARTCC_LOW,
                OutputSectionKeys.SCT_SID,
                OutputSectionKeys.SCT_STAR,
                OutputSectionKeys.SCT_LABELS,
                OutputSectionKeys.SCT_REGIONS
            };
            
            Assert.Equal(expected, new SctOutput(new Mock<TextWriter>().Object).GetOutputSections());
        }
    }
}
