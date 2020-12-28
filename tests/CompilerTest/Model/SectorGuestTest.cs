using Xunit;
using Compiler.Model;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorGuestTest
    {
        private readonly SectorGuest model;

        public SectorGuestTest()
        {
            this.model = new SectorGuest(
                "MWAL",
                "EGBB",
                "*",
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsController()
        {
            Assert.Equal("MWAL", this.model.Controller);
        }

        [Fact]
        public void TestItSetsDepartureAirport()
        {
            Assert.Equal("EGBB", this.model.DepartureAirport);
        }

        [Fact]
        public void TestItSetsArrivalAirport()
        {
            Assert.Equal("*", this.model.ArrivalAirport);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "GUEST:MWAL:EGBB:*",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}