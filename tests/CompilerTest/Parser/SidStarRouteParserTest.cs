using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public class SidStarRouteParserTest
    {
        private readonly SidStarRouteParser parser;
        
        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public SidStarRouteParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (SidStarRouteParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_SID);
        }

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
            this.parser.ParseData(
                new MockSectorDataFile(
                    "test.txt",
                    lines
                )
            );

            Assert.Empty(this.collection.StarRoutes);
            Assert.Empty(this.collection.SidRoutes);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSectionKeys.SCT_SID][0]);
        }

        [Fact]
        public void TestItAddsSingleRowElements()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "Test                       abc abc def def" })
            );

            this.parser.ParseData(data);

            List<RouteSegment> expectedSegments = new List<RouteSegment>
            {
                new RouteSegment(new Point("abc"), new Point("def"), null, null)
            };

            SidStarRoute result = this.collection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(expectedSegments, result.Segments);
        }

        [Fact]
        public void TestItAddsMultiRowElements()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(
                    new string[] {
                        "Test                       abc abc def def",
                        "                           def def ghi ghi ;comment"
                    }
                )
            );

            this.parser.ParseData(data);

            List<RouteSegment> expectedSegments = new List<RouteSegment>
            {
                new RouteSegment(new Point("abc"), new Point("def"), null, null),
                new RouteSegment(new Point("def"), new Point("ghi"), null, "comment")
            };

            SidStarRoute result = this.collection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(expectedSegments, result.Segments);
        }

        [Fact]
        public void TestItAddsMultipleElements()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(
                    new string[] {
                        "Test                       abc abc def def",
                        "                           def def ghi ghi ;comment",
                        "Test 2                     jkl jkl mno mno;comment"
                    }
                )
            );

            this.parser.ParseData(data);

            List<RouteSegment> expectedSegments1 = new List<RouteSegment>
            {
                new RouteSegment(new Point("abc"), new Point("def"), null, null),
                new RouteSegment(new Point("def"), new Point("ghi"), null, "comment")
            };

            List<RouteSegment> expectedSegments2 = new List<RouteSegment>
            {
                new RouteSegment(new Point("jkl"), new Point("mno"), null, "comment"),
            };

            SidStarRoute result = this.collection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(expectedSegments1, result.Segments);

            SidStarRoute result2 = this.collection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(expectedSegments2, result2.Segments);
        }

        [Fact]
        public void TestItAddsMultipleElementsWithColour()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(
                    new string[] {
                        "Test                       abc abc def def Red",
                        "                           def def ghi ghi Yellow ;comment",
                        "Test 2                     jkl jkl mno mno Blue;comment"
                    }
                )
            );

            this.parser.ParseData(data);

            List<RouteSegment> expectedSegments1 = new List<RouteSegment>
            {
                new RouteSegment(new Point("abc"), new Point("def"), "Red", null),
                new RouteSegment(new Point("def"), new Point("ghi"), "Yellow", "comment")
            };

            List<RouteSegment> expectedSegments2 = new List<RouteSegment>
            {
                new RouteSegment(new Point("jkl"), new Point("mno"), "Blue", "comment"),
            };

            SidStarRoute result = this.collection.SidRoutes[0];
            Assert.Equal("Test", result.Identifier);
            Assert.Equal(expectedSegments1, result.Segments);

            SidStarRoute result2 = this.collection.SidRoutes[1];
            Assert.Equal("Test 2", result2.Identifier);
            Assert.Equal(expectedSegments2, result2.Segments);
        }
    }
}
