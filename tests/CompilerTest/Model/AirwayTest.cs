using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class AirwayTest
    {
        private readonly Airway airway;

        public AirwayTest()
        {
            this.airway = new Airway(
                "UN864",
                AirwayType.HIGH,
                new Point("ABCDE"),
                new Point("FGHIJ"),
                "comment"
            );
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("UN864", this.airway.Identifier);
        }

        [Fact]
        public void TestItSetsType()
        {
            Assert.Equal(AirwayType.HIGH, this.airway.Type);
        }

        [Fact]
        public void TestItSetsStartPoint()
        {
            Assert.Equal(new Point("ABCDE"), this.airway.StartPoint);
        }

        [Fact]
        public void TestItSetsEndCoordinate()
        {
            Assert.Equal(new Point("FGHIJ"), this.airway.EndPoint);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("UN864 ABCDE ABCDE FGHIJ FGHIJ ;comment\r\n", this.airway.Compile());
        }
    }
}
