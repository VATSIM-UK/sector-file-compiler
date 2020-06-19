using Xunit;
using Compiler.Output;
using System.Collections.Generic;

namespace CompilerTest.Output
{
    public class SubsectionMapperTest
    {
        [Fact]
        public void TestItReturnsDefaultIfNoConfiguredSubsections()
        {
            Assert.Equal(
                new List<Subsections> { Subsections.DEFAULT },
                SubsectionMapper.GetSubsectionsForSection(OutputSections.ESE_FREETEXT)
            );
        }

        [Fact]
        public void TestItReturnsConfiguredSections()
        {
            Assert.Equal(
                new List<Subsections> {
                    Subsections.ESE_AIRSPACE_SECTORLINE,
                    Subsections.ESE_AIRSPACE_SECTOR,
                    Subsections.ESE_AIRSPACE_COORDINATION 
                },
                SubsectionMapper.GetSubsectionsForSection(OutputSections.ESE_AIRSPACE)
            );
        }
    }
}