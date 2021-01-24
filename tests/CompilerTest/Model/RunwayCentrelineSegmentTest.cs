using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class RunwayCentrelineSegmentTest
    {
        private RunwayCentrelineSegment segment;

        public RunwayCentrelineSegmentTest()
        {
            segment = new RunwayCentrelineSegment(
                new("abc", "def"),
                new("ghi", "jkl")
            );
        }

        [Fact]
        public void TestItSetsFirstCoordinate()
        {
            Assert.Equal(new Coordinate("abc", "def"), segment.FirstCoordinate);
        }
        
        [Fact]
        public void TestItSetsSecondCoordinate()
        {
            Assert.Equal(new Coordinate("ghi", "jkl"), segment.SecondCoordinate);
        }

        [Fact]
        public void TestStringRepresentation()
        {
            Assert.Equal("abc def ghi jkl", segment.ToString());
        }
    }
}