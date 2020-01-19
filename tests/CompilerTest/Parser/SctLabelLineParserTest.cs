using System.Collections.Generic;
using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class SctLabelLineParserTest
    {
        private readonly SctLabelLineParser parser;

        public SctLabelLineParserTest()
        {
            this.parser = new SctLabelLineParser();
        }

        [Fact]
        public void TestItParsesLinesWithNoComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "\"test label\" abc def ghi",
                new List<string>(new string[] { "test label", "abc", "def", "ghi" }),
                null
            );
            Assert.True(expected.Equals(this.parser.ParseLine("\"test label\" abc def ghi")));
        }

        [Fact]
        public void TestItParsesLinesWithComments()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "\"test label\" abc def ghi",
                new List<string>(new string[] { "test label", "abc", "def", "ghi" }),
                "comment"
            );

            Assert.True(expected.Equals(this.parser.ParseLine("\"test label\" abc def ghi ;comment")));
        }

        [Fact]
        public void TestItHandlesDoubleSpaces()
        {
            SectorFormatLine expected = new SectorFormatLine(
                "\"test label\" abc def    ghi",
                new List<string>(new string[] { "test label", "abc", "def", "ghi" }),
                "comment"
            );

            Assert.True(expected.Equals(this.parser.ParseLine("\"test label\" abc def    ghi ;comment")));
        }

        [Fact]
        public void TestItReturnsBlankOnNoStartingDoubleQuote()
        {
            SectorFormatLine expected = new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("test label abc def    ghi ;comment"))
            );
        }

        [Fact]
        public void TestItReturnsBlankOnCharacterBeforeFirstDoubleQuote()
        {
            SectorFormatLine expected = new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("a\"test label abc def    ghi ;comment"))
            );
        }

        [Fact]
        public void TestItReturnsBlankOnNoEndingDoubleQuote()
        {
            SectorFormatLine expected = new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
            );
            Assert.True(
                expected.Equals(this.parser.ParseLine("\"test label abc def    ghi ;comment"))
            );
        }
    }
}
