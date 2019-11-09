using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Model;
using Compiler.Output;

namespace CompilerTest.Model
{
    public class SectionHeaderFactoryTest
    {
        [Fact]
        public void TestItCreatesNullHeaderForSctHeader()
        {
            Assert.IsType<NullElement>(SectionHeaderFactory.Create(OutputSections.SCT_HEADER));
        }

        [Fact]
        public void TestItCreatesNullHeaderForSctColourDefs()
        {
            Assert.IsType<NullElement>(SectionHeaderFactory.Create(OutputSections.SCT_COLOUR_DEFS));
        }

        [Fact]
        public void TestItCreatesHeaderForSctInfo()
        {
            Assert.Equal("[INFO]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_INFO).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctAirport()
        {
            Assert.Equal("[AIRPORT]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_AIRPORT).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctRunway()
        {
            Assert.Equal("[RUNWAY]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_RUNWAY).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctVor()
        {
            Assert.Equal("[VOR]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_VOR).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctNdb()
        {
            Assert.Equal("[NDB]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_NDB).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctFixes()
        {
            Assert.Equal("[FIXES]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_FIXES).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctGeo()
        {
            Assert.Equal("[GEO]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_GEO).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctLowAirway()
        {
            Assert.Equal("[LOW AIRWAY]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_LOW_AIRWAY).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctHighAirway()
        {
            Assert.Equal("[HIGH AIRWAY]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_HIGH_AIRWAY).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctArtcc()
        {
            Assert.Equal("[ARTCC]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_ARTCC).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctHighArtcc()
        {
            Assert.Equal("[ARTCC HIGH]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_ARTCC_HIGH).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctLowArtcc()
        {
            Assert.Equal("[ARTCC LOW]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_ARTCC_LOW).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctSid()
        {
            Assert.Equal("[SID]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_SID).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctStar()
        {
            Assert.Equal("[STAR]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_STAR).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctLabels()
        {
            Assert.Equal("[LABELS]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_LABELS).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForSctRegions()
        {
            Assert.Equal("[REGIONS]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.SCT_REGIONS).Compile());
        }

        [Fact]
        public void TestItCreatesNullHeaderForEseHeader()
        {
            Assert.IsType<NullElement>(SectionHeaderFactory.Create(OutputSections.ESE_HEADER));
        }

        [Fact]
        public void TestItCreatesNullHeaderForEsePreamble()
        {
            Assert.IsType<NullElement>(SectionHeaderFactory.Create(OutputSections.ESE_PREAMBLE));
        }

        [Fact]
        public void TestItCreatesHeaderForEsePositions()
        {
            Assert.Equal("[POSITIONS]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.ESE_POSITIONS).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForEseFreetext()
        {
            Assert.Equal("[FREETEXT]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.ESE_FREETEXT).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForEseSidsStars()
        {
            Assert.Equal("[SIDSSTARS]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.ESE_SIDSSTARS).Compile());
        }

        [Fact]
        public void TestItCreatesHeaderForEseAirspace()
        {
            Assert.Equal("[AIRSPACE]\r\n\r\n", SectionHeaderFactory.Create(OutputSections.ESE_AIRSPACE).Compile());
        }
    }
}
