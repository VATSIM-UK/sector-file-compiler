using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Parser
{
    public class SidStarRouteParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "                           abc def ghi jkl"
            }}, // First item not a new segment
            new object[] { new List<string>{
                "Test                       a b def def"
            }}, // First point not valid
            new object[] { new List<string>{
                "Test                       abc abc d e"
            }}, // Second point not valid
            new object[] { new List<string>{
                "Test                       abc abc def def",
                "                           abc b def def",
            }}, // Second line first point not valid
            new object[] { new List<string>{
                "Test                       abc abc def def",
                "                           abc abc hhh def",
            }}, // Second line second point not valid
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            RunParserOnLines(lines);

            Assert.Empty(sectorElementCollection.StarRoutes);
            Assert.Empty(sectorElementCollection.SidRoutes);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsSingleRowElements()
        {
            RunParserOnLines(new List<string>(new[] { "Test                       abc abc def def ;comment" }));
            
            SidStarRoute result = sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("abc"),
                    new Point("def"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                result.InitialSegment
            );
            Assert.Empty(result.Segments);
            
            AssertExpectedMetadata(result);
            AssertExpectedMetadata(result.InitialSegment);
        }

        [Fact]
        public void TestItAddsMultiRowElements()
        {
            RunParserOnLines(new List<string>(
                new[] {
                    "Test                       abc abc def def ;comment",
                    "                           def def ghi ghi ;comment"
                }
            ));


            List<RouteSegment> expectedAdditionalSegments = new()
            {
                new RouteSegment(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
            };

            SidStarRoute result = sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("abc"),
                    new Point("def"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                result.InitialSegment
            );
            Assert.Equal(expectedAdditionalSegments, result.Segments);
            
            AssertExpectedMetadata(result);
            AssertExpectedMetadata(result.InitialSegment);
            AssertExpectedMetadata(result.Segments[0], 2);
        }

        [Fact]
        public void TestItAddsMultipleElements()
        {
            RunParserOnLines(new List<string>(
                new List<string>(
                    new[] {
                        "Test                       abc abc def def;comment",
                        "                           def def ghi ghi ;comment",
                        "Test 2                     jkl jkl mno mno;comment"
                    }
                )
            ));

            List<RouteSegment> expectedAdditionalSegments1 = new()
            {
                new RouteSegment(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            };

            SidStarRoute result = sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("abc"),
                    new Point("def"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                result.InitialSegment
            );
            Assert.Equal(expectedAdditionalSegments1, result.Segments);
            AssertExpectedMetadata(result);
            AssertExpectedMetadata(result.InitialSegment);
            AssertExpectedMetadata(result.Segments[0], 2);

            SidStarRoute result2 = sectorElementCollection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("jkl"),
                    new Point("mno"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
                result2.InitialSegment
            );
            Assert.Empty(result2.Segments);
            AssertExpectedMetadata(result2, 3);
            AssertExpectedMetadata(result2.InitialSegment, 3);
        }

        [Fact]
        public void TestItAddsMultipleElementsWithColour()
        {
            RunParserOnLines(new List<string>(
                new[] {
                    "Test                       abc abc def def Red",
                    "                           def def ghi ghi Yellow ;comment",
                    "Test 2                     jkl jkl mno mno Blue;comment"
                }
            ));
            
            List<RouteSegment> expectedAdditionalSegments1 = new()
            {
                new RouteSegment(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    "Yellow"
                )
            };
            

            SidStarRoute result = sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("abc"),
                    new Point("def"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    "Red"
                ),
                result.InitialSegment
            );
            Assert.Equal(expectedAdditionalSegments1, result.Segments);
            AssertExpectedMetadata(result, commentString: "");
            AssertExpectedMetadata(result.InitialSegment, commentString: "");
            AssertExpectedMetadata(result.Segments[0], 2);

            SidStarRoute result2 = sectorElementCollection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(
                new RouteSegment(
                    "Test",
                    new Point("jkl"),
                    new Point("mno"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    "Blue"
                ),
                result2.InitialSegment
            );
            Assert.Empty(result2.Segments);
            AssertExpectedMetadata(result2, 3);
            AssertExpectedMetadata(result2.InitialSegment, 3);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_SIDS;
        }
    }
}
