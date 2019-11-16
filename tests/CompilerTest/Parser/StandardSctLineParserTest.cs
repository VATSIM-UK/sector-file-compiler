using System.Collections.Generic;
using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class StandardSctLineParserTest
    {
        private readonly StandardSctLineParser parser;

        public StandardSctLineParserTest()
        {
            this.parser = new StandardSctLineParser();
        }

        [Fact]
        public void TestItParsesLinesWithNoComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1 item2 item3 item4",
                new List<string>(new string[] { "item1", "item2", "item3", "item4" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1 item2 item3 item4"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1 item2 item3 item4",
                new List<string>(new string[] { "item1", "item2", "item3", "item4" }),
                "comment"
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1 item2 item3 item4 ;comment"))
            );
        }

        [Fact]
        public void TestItHandlesDoubleSpaces()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1 item2   item3 item4",
                new List<string>(new string[] { "item1", "item2", "item3", "item4" }),
                "comment"
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1 item2   item3 item4 ;comment"))
            );
        }
    }
}
