using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class EsePositionParserTest: AbstractParserTestCase
    {
        [Fact]
        public void TestItAddsPositionData()
        {
            this.RunParserOnLines(new List<string>() {"LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:N051.32.26.871:W002.43.29.831:N051.32.26.872:W002.43.29.832:N051.32.26.873:W002.43.29.833 ;comment"});

            List<Coordinate> coordinateList = new();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            coordinateList.Add(new Coordinate("N051.32.26.871", "W002.43.29.831"));
            coordinateList.Add(new Coordinate("N051.32.26.872", "W002.43.29.832"));
            coordinateList.Add(new Coordinate("N051.32.26.873", "W002.43.29.833"));
            ControllerPosition position = this.sectorElementCollection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
            this.AssertExpectedMetadata(position);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedCenters()
        {
            this.RunParserOnLines(new List<string>() {"LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::: ;comment"});

            List<Coordinate> coordinateList = new();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.sectorElementCollection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("0301", position.SquawkRangeStart);
            Assert.Equal("0377", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
            this.AssertExpectedMetadata(position);
        }

        [Fact]
        public void TestItAddsPositionDataSkippedSquawks()
        {
            this.RunParserOnLines(new List<string>() {"LON_CTR:London Control:127.820:L:9:LON:CTR:-:-::-:N051.32.26.870:W002.43.29.830 ;comment"});

            List<Coordinate> coordinateList = new();
            coordinateList.Add(new Coordinate("N051.32.26.870", "W002.43.29.830"));
            ControllerPosition position = this.sectorElementCollection.EsePositions[0];
            Assert.Equal("LON_CTR", position.Callsign);
            Assert.Equal("London Control", position.RtfCallsign);
            Assert.Equal("127.820", position.Frequency);
            Assert.Equal("L", position.Identifier);
            Assert.Equal("9", position.MiddleLetter);
            Assert.Equal("LON", position.Prefix);
            Assert.Equal("CTR", position.Suffix);
            Assert.Equal("", position.SquawkRangeStart);
            Assert.Equal("", position.SquawkRangeEnd);
            Assert.Equal(coordinateList, position.VisCentres);
        }

        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301 ;comment"
                }
            }, // Incorrect number of segments
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.821:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Invalid RTF frequency
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:LOL:-:-:0301:0377:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Invalid suffix
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0381:0377:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Invalid squawk range start
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0371xxxx:0377:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Squawk range start has extra characters
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0378:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Invalid squawk range end
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377xxxxx:N051.32.26.870:W002.43.29.830 ;comment"
                }
            }, // Squawk range end has extra characters
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830:::::::: ;comment"
                }
            }, // Too many vis centers
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:W002.43.29.830: ;comment"
                }
            }, // Vis center missing longitude
            new object[]
            {
                new List<string>
                {
                    "LON_CTR:London Control:127.820:L:9:LON:CTR:-:-:0301:0377:N051.32.26.870:N002.43.29.830 ;comment"
                }
            }, // Invalid vis center coordinate
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.EsePositions);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_POSITIONS;
        }
    }
}
