using Moq;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Parser
{
    public class AbstractEseAirspaceParserTest
    {
        private readonly AbstractEseAirspaceParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public AbstractEseAirspaceParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (AirspaceParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.ESE_AIRSPACE);
        }

        [Theory]
        [InlineData("SECTORLINE", true)]
        [InlineData("CIRCLE_SECTORLINE", true)]
        [InlineData("SECTOR", true)]
        [InlineData("COPX", true)]
        [InlineData("FIR_COPX", true)]
        [InlineData("NOPE", false)]
        public void TestItReturnsStartOfSection(string line, bool expected)
        {
            EseLineParser lineParser = new EseLineParser();
            Assert.Equal(expected, this.parser.IsNewDeclaration(lineParser.ParseLine(line)));
        }
    }
}
