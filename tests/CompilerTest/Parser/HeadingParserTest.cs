using Compiler.Parser;
using Xunit;

namespace CompilerTest.Parser
{
    public class HeadingParserTest
    {
        [Theory]
        [InlineData("360", 360, true)]
        [InlineData("180", 180, true)]
        [InlineData("001", 1, true)]
        [InlineData("01", 1, true)]
        [InlineData("1", 1, true)]
        [InlineData("361", -1, false)]
        [InlineData("000", -1, false)]
        [InlineData("00", -1, false)]
        [InlineData("0", -1, false)]
        [InlineData("abc", -1, false)]
        public void TestItTriesToParseHeadings(string heading, int expectedParsed, bool expectedReturn)
        {
            bool tryParseResult = HeadingParser.TryParse(heading, out int parsedHeadingResult);
            Assert.Equal(expectedReturn, tryParseResult);
            Assert.Equal(expectedParsed, parsedHeadingResult);
        }
    }
}