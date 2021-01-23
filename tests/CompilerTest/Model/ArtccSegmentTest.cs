using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class ArtccSegmentTest
    {
        private readonly ArtccSegment artcc;

        public ArtccSegmentTest()
        {
            this.artcc = new ArtccSegment(
                "EGTT",
                ArtccType.HIGH,
                new Point("abc"),
                new Point("def"),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("EGTT", this.artcc.Identifier);
        }

        [Fact]
        public void TestItSetsType()
        {
            Assert.Equal(ArtccType.HIGH, this.artcc.Type);
        }

        [Fact]
        public void TestItSetsStartPoint()
        {
            Assert.Equal(new Point("abc"), this.artcc.StartPoint);
        }

        [Fact]
        public void TestItSetsEndCoordinate()
        {
            Assert.Equal(new Point("def"), this.artcc.EndPoint);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "EGTT abc abc def def", 
                this.artcc.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
