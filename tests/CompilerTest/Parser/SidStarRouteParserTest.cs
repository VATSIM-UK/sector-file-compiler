using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Output;

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
            this.parser = (SidStarRouteParser)(new SectionParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSections.SCT_SID);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfFirstItemIsNotANewSegment()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "                           abc def ghi jkl" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfFirstPointNotValid()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "Test                       a b def def" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfSecondPointNotValid()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "Test                       abc abc d e" })
            );

            this.parser.ParseData(data);
            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItHandlesMetadata()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSections.SCT_SID][Subsections.DEFAULT][0]);
        }

        [Fact]
        public void TestItAddsSingleRowElements()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
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
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
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
