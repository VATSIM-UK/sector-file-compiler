using System.Collections.Generic;
using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class EseLineParserTest
    {
        private readonly EseLineParser parser;

        public EseLineParserTest()
        {
            this.parser = new EseLineParser();
        }

        [Fact]
        public void TestItParsesLinesWithNoComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1:item2:item3:item4a item4b",
                new List<string>(new string[] { "item1", "item2", "item3", "item4a item4b" }),
                null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1:item2:item3:item4a item4b"))
            );
        }

        [Fact]
        public void TestItParsesLinesWithComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1:item2:item3:item4a item4b",
                new List<string>(new string[] { "item1", "item2", "item3", "item4a item4b" }),
                "comment"
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1:item2:item3:item4a item4b   ;comment"))
            );
        }

        [Fact]
        public void TestItHandlesBlankItems()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "item1:item2::item3:item4a item4b",
                new List<string>(new string[] { "item1", "item2", "", "item3", "item4a item4b" }),
                "comment"
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("item1:item2::item3:item4a item4b   ;comment"))
            );
        }
    }
}
