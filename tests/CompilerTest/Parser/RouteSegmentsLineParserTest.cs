using System.Collections.Generic;
using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class RouteSegmentsLineParserTest
    {
        private readonly RouteSegmentsLineParser parser;

        public RouteSegmentsLineParserTest()
        {
            this.parser = new RouteSegmentsLineParser();
        }

        [Fact]
        public void TestItHandlesLinesThatAreTooShort()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "",
                new List<string>(),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("ite"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithNoComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "TEST - 1                  item1 item2 item3 item4",
                new List<string>(new string[] { "TEST - 1", "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("TEST - 1                  item1 item2 item3 item4"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithNoNameSegment()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "                          item1 item2 item3 item4",
                new List<string>(new string[] { "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("                          item1 item2 item3 item4"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithOptionalSpaces()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "TEST - 1                      item1 item2 item3 item4",
                new List<string>(new string[] { "TEST - 1", "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("TEST - 1                      item1 item2 item3 item4"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithTabs()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "TEST - 1                  item1\titem2 item3 item4",
                new List<string>(new string[] { "TEST - 1", "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("TEST - 1                  item1\titem2 item3 item4"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "TEST - 1                  item1\titem2 item3 item4",
                new List<string>(new string[] { "TEST - 1", "item1", "item2", "item3", "item4" }),
                "comment"
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("TEST - 1                  item1\titem2 item3 item4 ;comment"))
            );
        }

        [Fact]
        public void TestItHandlesDoubleSpaces()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "TEST - 1                  item1   item2 item3 item4",
                new List<string>(new string[] { "TEST - 1", "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("TEST - 1                  item1   item2 item3 item4"))
            );
        }
    }
}
