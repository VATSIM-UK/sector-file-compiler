using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Config;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigFileSectionsMapperTest
    {
        [Fact]
        public void ItReturnsTrueIfConfigSectionIsValid()
        {
            Assert.True(ConfigFileSectionsMapper.ConfigSectionValid("sct_header"));
        }

        [Fact]
        public void ItReturnsFalseIfConfigSectionInvalid()
        {
            Assert.False(ConfigFileSectionsMapper.ConfigSectionValid("not_sct_header"));
        }

        [Theory]
            [InlineData(OutputSections.SCT_HEADER, "sct_header")]
            [InlineData(OutputSections.SCT_COLOUR_DEFS, "sct_colour_defs")]
            [InlineData(OutputSections.SCT_INFO, "sct_info")]
            [InlineData(OutputSections.SCT_AIRPORT, "sct_airport")]
            [InlineData(OutputSections.SCT_RUNWAY, "sct_runway")]
            [InlineData(OutputSections.SCT_VOR, "sct_vor")]
            [InlineData(OutputSections.SCT_NDB, "sct_ndb")]
            [InlineData(OutputSections.SCT_FIXES, "sct_fixes")]
            [InlineData(OutputSections.SCT_GEO, "sct_geo")]
            [InlineData(OutputSections.SCT_LOW_AIRWAY, "sct_low_airway")]
            [InlineData(OutputSections.SCT_HIGH_AIRWAY, "sct_high_airway")]
            [InlineData(OutputSections.SCT_ARTCC, "sct_artcc")]
            [InlineData(OutputSections.SCT_ARTCC_HIGH, "sct_artcc_high")]
            [InlineData(OutputSections.SCT_ARTCC_LOW, "sct_artcc_low")]
            [InlineData(OutputSections.SCT_SID, "sct_sid")]
            [InlineData(OutputSections.SCT_STAR, "sct_star")]
            [InlineData(OutputSections.SCT_LABELS, "sct_labels")]
            [InlineData(OutputSections.SCT_REGIONS, "sct_regions")]
            [InlineData(OutputSections.ESE_HEADER, "ese_header")]
            [InlineData(OutputSections.ESE_PREAMBLE, "ese_preamble")]
            [InlineData(OutputSections.ESE_POSITIONS,  "ese_positions")]
            [InlineData(OutputSections.ESE_FREETEXT, "ese_freetext")]
            [InlineData(OutputSections.ESE_SIDSSTARS, "ese_sidsstars")]
            [InlineData(OutputSections.ESE_AIRSPACE, "ese_airspace")]
        public void ItReturnsConfigSectionForOutputSection(OutputSections section, string expectedSection)
        {
            Assert.Equal(
                expectedSection,
                ConfigFileSectionsMapper.GetConfigSectionForOutputSection(section)
            );
        }
    }
}
