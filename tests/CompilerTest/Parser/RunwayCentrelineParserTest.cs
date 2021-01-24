using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class RunwayCentrelineParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490 N050.57.00.000 W001.21.24.490"
            }}, // Too many sections
            new object[] { new List<string>{
                "N050.57.00.000 W001.21.24.490"
            }}, // Too few sections
            new object[] { new List<string>{
                "abc def N050.57.00.000 W001.21.24.490"
            }}, // Invalid first coordinates
            new object[] { new List<string>{
                "N050.57.00.000 W001.21.24.490 abc def"
            }}, // Invalid second coordinates
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.RunwayCentrelines);
            Assert.Empty(sectorElementCollection.FixedColourRunwayCentrelines);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsCentrelineData()
        {
            RunParserOnLines(new List<string>() {"N050.57.00.000 W001.21.24.490 N051.57.00.000 W002.21.24.490 ;comment"});

            Assert.Single(sectorElementCollection.RunwayCentrelines);
            RunwayCentreline firstResult = sectorElementCollection.RunwayCentrelines[0];
            Assert.Equal(
                new Coordinate("N050.57.00.000", "W001.21.24.490"),
                firstResult.CentrelineSegment.FirstCoordinate
            );
            
            Assert.Equal(
                new Coordinate("N051.57.00.000", "W002.21.24.490"),
                firstResult.CentrelineSegment.SecondCoordinate
            );
            
            AssertExpectedMetadata(firstResult);
            
            // The fixed colour centreline should have the same base segment as the main one
            Assert.Single(sectorElementCollection.FixedColourRunwayCentrelines);
            RunwayCentreline secondResult = sectorElementCollection.FixedColourRunwayCentrelines[0];
            Assert.IsType<FixedColourRunwayCentreline>(secondResult);
            Assert.Same(firstResult.CentrelineSegment, secondResult.CentrelineSegment);
            AssertExpectedMetadata(secondResult);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_RUNWAY_CENTRELINES;
        }
    }
}
