using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class ArtccTest
    {
        private readonly Artcc artcc;

        public ArtccTest()
        {
            this.artcc = new Artcc(
                "EGTT",
                ArtccType.HIGH,
                new Point("ABCDE"),
                new Point("FGHIJ"),
                "comment"
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
            Assert.Equal(new Point("ABCDE"), this.artcc.StartPoint);
        }

        [Fact]
        public void TestItSetsEndCoordinate()
        {
            Assert.Equal(new Point("FGHIJ"), this.artcc.EndPoint);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("EGTT ABCDE ABCDE FGHIJ FGHIJ ;comment\r\n", this.artcc.Compile());
        }
    }
}
