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
    public class LabelParserTest
    {
        private readonly LabelParser parser;

        private readonly SectorElementCollection collection;

        private readonly Mock<IEventLogger> log;

        public LabelParserTest()
        {
            this.log = new Mock<IEventLogger>();
            this.collection = new SectorElementCollection();
            this.parser = (LabelParser)(new DataParserFactory(this.collection, this.log.Object))
                .GetParserForSection(OutputSectionKeys.SCT_LABELS);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "\"test label\" N050.57.00.000 W001.21.24.490" })
            );
            this.parser.ParseData(data);

            this.log.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfCoordinateNotvalid()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "\"test label\" N050.57.00.000 N001.21.24.490 red" })
            );
            this.parser.ParseData(data);

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
            Assert.IsType<BlankLine>(this.collection.Compilables[OutputSectionKeys.SCT_LABELS][0]);
        }

        [Fact]
        public void TestItAddsLabelData()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "\"test label\" N050.57.00.000 W001.21.24.490 red ;comment" })
            );
            this.parser.ParseData(data);

            Label result = this.collection.Labels[0];
            Assert.Equal("test label", result.Text);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Position);
            Assert.Equal("red", result.Colour);
            Assert.Equal("comment", result.Comment);
        }
    }
}
