using Compiler.Output;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputSectionsConfigTest
    {
        [Fact]
        public void TestItHasCorrectNumberOfSections()
        {
            Assert.Equal(25, OutputSectionsConfig.Sections.Length);
        }

        [Theory]
        [InlineData(0, OutputSectionKeys.FILE_HEADER, false, null)]
        [InlineData(1, OutputSectionKeys.SCT_COLOUR_DEFS, false, null)]
        [InlineData(2, OutputSectionKeys.SCT_INFO, false, "[INFO]")]
        [InlineData(3, OutputSectionKeys.SCT_AIRPORT, false, "[AIRPORT]")]
        [InlineData(4, OutputSectionKeys.SCT_RUNWAY, false, "[RUNWAY]")]
        [InlineData(5, OutputSectionKeys.SCT_VOR, false, "[VOR]")]
        [InlineData(6, OutputSectionKeys.SCT_NDB, false, "[NDB]")]
        [InlineData(7, OutputSectionKeys.SCT_FIXES, false, "[FIXES]")]
        [InlineData(8, OutputSectionKeys.SCT_GEO, true, "[GEO]")]
        [InlineData(9, OutputSectionKeys.SCT_LOW_AIRWAY, false, "[LOW AIRWAY]")]
        [InlineData(10, OutputSectionKeys.SCT_HIGH_AIRWAY, false, "[HIGH AIRWAY]")]
        [InlineData(11, OutputSectionKeys.SCT_ARTCC, false, "[ARTCC]")]
        [InlineData(12, OutputSectionKeys.SCT_ARTCC_HIGH, false, "[ARTCC HIGH]")]
        [InlineData(13, OutputSectionKeys.SCT_ARTCC_LOW, false, "[ARTCC LOW]")]
        [InlineData(14, OutputSectionKeys.SCT_SID, false, "[SID]")]
        [InlineData(15, OutputSectionKeys.SCT_STAR, false, "[STAR]")]
        [InlineData(16, OutputSectionKeys.SCT_LABELS, true, "[LABELS]")]
        [InlineData(17, OutputSectionKeys.SCT_REGIONS, true, "[REGIONS]")]
        [InlineData(18, OutputSectionKeys.ESE_POSITIONS, false, "[POSITIONS]")]
        [InlineData(19, OutputSectionKeys.ESE_FREETEXT, false, "[FREETEXT]")]
        [InlineData(20, OutputSectionKeys.ESE_SIDSSTARS, true, "[SIDSSTARS]")]
        [InlineData(21, OutputSectionKeys.ESE_AIRSPACE, true, "[AIRSPACE]")]
        [InlineData(22, OutputSectionKeys.ESE_GROUND_NETWORK, true, "[GROUND]")]
        [InlineData(23, OutputSectionKeys.ESE_RADAR, false, "[RADAR]")]
        [InlineData(24, OutputSectionKeys.RWY_ACTIVE_RUNWAYS, false, null)]
        public void TestConfig(int index, OutputSectionKeys expectedKey, bool expectedPrintGroupings, string expectedHeader)
        {
            Assert.Equal(
                new OutputSection(expectedKey, expectedPrintGroupings, expectedHeader),
                OutputSectionsConfig.Sections[index]
            );
        }
    }
}
