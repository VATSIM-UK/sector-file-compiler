using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class VatsimRtfFrequencyParserTest
    {
        private readonly VatsimRtfFrequencyParser parser;

        public VatsimRtfFrequencyParserTest()
        {
            this.parser = new VatsimRtfFrequencyParser();
        }

        [Theory]
        [InlineData("a", null)] // Not a frequency
        [InlineData("123.456.789", null)] // Too many dots
        [InlineData("123", null)] // Not enough dots
        [InlineData("abc.500", null)] // First part not an integer
        [InlineData("106.500", null)] // First part too small
        [InlineData("138.500", null)] // First part too big
        [InlineData("135.abc", null)] // Second part not an integer
        [InlineData("135.155", null)] // Second part doesn't divide
        [InlineData("135.150", "135.150")] // Everything ok
        [InlineData("108.150", "108.150")] // Everything ok
        [InlineData("199.998", "199.998")] // Everything ok - no frequency
        [InlineData("119.720", "119.720")] // Everything ok - round up
        public void TestItParsesFrequencies(string frequency, string expected)
        {
            Assert.Equal(expected, this.parser.ParseFrequency(frequency));
        }
    }
}
