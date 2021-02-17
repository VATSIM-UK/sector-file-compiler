using Compiler.Parser;
using Xunit;

namespace CompilerTest.Parser
{
    public class EuroScopeNoFrequencyParserTest
    {
        private readonly EuroscopeNoFrequencyParser parser;

        public EuroScopeNoFrequencyParserTest()
        {
            parser = new EuroscopeNoFrequencyParser();
        }

        [Fact]
        public void TestItReturnsNoFrequencyIfNoFrequency()
        {
            Assert.Equal("199.998", parser.ParseFrequency("199.998"));
        }

        [Fact]
        public void TestItReturnsNullIfIncorrectFrequency()
        {
            Assert.Null(parser.ParseFrequency("199.997"));
        }
    }
}
