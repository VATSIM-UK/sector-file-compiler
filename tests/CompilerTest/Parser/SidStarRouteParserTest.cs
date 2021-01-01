using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using CompilerTest.Mock;

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
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.StarRoutes);
            Assert.Empty(this.sectorElementCollection.SidRoutes);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsSingleRowElements()
        {
            this.RunParserOnLines(new List<string>(new[] { "Test                       abc abc def def ;comment" }));
            
            SidStarRoute result = this.sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new(
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
            
            this.AssertExpectedMetadata(result);
            this.AssertExpectedMetadata(result.InitialSegment);
        }

        [Fact]
        public void TestItAddsMultiRowElements()
        {
            this.RunParserOnLines(new List<string>(
                new string[] {
                    "Test                       abc abc def def ;comment",
                    "                           def def ghi ghi ;comment"
                }
            ));


            List<RouteSegment> expectedAdditionalSegments = new List<RouteSegment>
            {
                new(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                ),
            };

            SidStarRoute result = this.sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new(
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
            
            this.AssertExpectedMetadata(result);
            this.AssertExpectedMetadata(result.InitialSegment);
            this.AssertExpectedMetadata(result.Segments[0], 2);
        }

        [Fact]
        public void TestItAddsMultipleElements()
        {
            this.RunParserOnLines(new List<string>(
                new List<string>(
                    new string[] {
                        "Test                       abc abc def def;comment",
                        "                           def def ghi ghi ;comment",
                        "Test 2                     jkl jkl mno mno;comment"
                    }
                )
            ));

            List<RouteSegment> expectedAdditionalSegments1 = new List<RouteSegment>
            {
                new(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            };

            SidStarRoute result = this.sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new(
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
            this.AssertExpectedMetadata(result);
            this.AssertExpectedMetadata(result.InitialSegment);
            this.AssertExpectedMetadata(result.Segments[0], 2);

            SidStarRoute result2 = this.sectorElementCollection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(
                new(
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
            this.AssertExpectedMetadata(result2, 3);
            this.AssertExpectedMetadata(result2.InitialSegment, 3);
        }

        [Fact]
        public void TestItAddsMultipleElementsWithColour()
        {
            this.RunParserOnLines(new List<string>(
                new string[] {
                    "Test                       abc abc def def Red",
                    "                           def def ghi ghi Yellow ;comment",
                    "Test 2                     jkl jkl mno mno Blue;comment"
                }
            ));
            
            List<RouteSegment> expectedAdditionalSegments1 = new List<RouteSegment>
            {
                new(
                    "Test",
                    new Point("def"),
                    new Point("ghi"),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make(),
                    "Yellow"
                )
            };
            

            SidStarRoute result = this.sectorElementCollection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(
                new(
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
            this.AssertExpectedMetadata(result, commentString: "");
            this.AssertExpectedMetadata(result.InitialSegment, commentString: "");
            this.AssertExpectedMetadata(result.Segments[0], 2);

            SidStarRoute result2 = this.sectorElementCollection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(
                new(
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
            this.AssertExpectedMetadata(result2, 3);
            this.AssertExpectedMetadata(result2.InitialSegment, 3);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_SIDS;
        }
    }
}
