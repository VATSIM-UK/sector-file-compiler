using Xunit;
using Compiler.Parser;

namespace CompilerTest.Parser
{
    public class FrequencyParserTest
    {
        private readonly FrequencyParser parser;

        public FrequencyParserTest()
        {
            this.parser = new FrequencyParser(117, 137, 50);
        }

        [Theory]
        [InlineData("a", null)] // Not a frequency
        [InlineData("123.456.789", null)] // Too many dots
        [InlineData("123", null)] // Not enough dots
        [InlineData("abc.500", null)] // First part not an integer
        [InlineData("116.500", null)] // First part too small
        [InlineData("138.500", null)] // First part too big
        [InlineData("135.abc", null)] // Second part not an integer
        [InlineData("135.155", null)] // Second part doesn't divide
        [InlineData("135.150", "135.150")] // Everything ok
        public void TestItParsesFrequencies(string frequency, string expected)
        {
            Assert.Equal(expected, this.parser.ParseFrequency(frequency));
        }
    }
}
