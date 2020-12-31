using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class ControllerPositionTest
    {
        private readonly ControllerPosition model;
        private List<Coordinate> coordlist;

        public ControllerPositionTest()
        {
            this.coordlist = new List<Coordinate>();
            this.coordlist.Add(new Coordinate("abc", "def"));
            this.coordlist.Add(new Coordinate("ghi", "jkl"));
            this.coordlist.Add(new Coordinate("mno", "pqr"));
            this.model = new ControllerPosition(
                "EGBB_APP",
                "Birmingham Radar",
                "123.970",
                "BBR",
                "B",
                "EGBB",
                "APP",
                "0401",
                "0407",
                this.coordlist,
                PositionOrder.PRE_POSITION,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItSetsCallsign()
        {
            Assert.Equal("EGBB_APP", this.model.Callsign);
        }

        [Fact]
        public void TestItSetsRtfCallsign()
        {
            Assert.Equal("Birmingham Radar", this.model.RtfCallsign);
        }

        [Fact]
        public void TestItSetsFrequency()
        {
            Assert.Equal("123.970", this.model.Frequency);
        }

        [Fact]
        public void TestItSetsIdentifier()
        {
            Assert.Equal("BBR", this.model.Identifier);
        }

        [Fact]
        public void TestItSetsMiddleLetter()
        {
            Assert.Equal("B", this.model.MiddleLetter);
        }

        [Fact]
        public void TestItSetsSuffix()
        {
            Assert.Equal("APP", this.model.Suffix);
        }

        [Fact]
        public void TestItSetsSquawkRangeStart()
        {
            Assert.Equal("0401", this.model.SquawkRangeStart);
        }

        [Fact]
        public void TestItSetsSquawkRangeEnd()
        {
            Assert.Equal("0407", this.model.SquawkRangeEnd);
        }
        
        [Fact]
        public void TestItSetsPositionOrder()
        {
            Assert.Equal(PositionOrder.PRE_POSITION, this.model.PositionOrder);
        }


        [Fact]
        public void TestItSetsVisCenters()
        {
            Assert.Equal(this.coordlist, this.model.VisCentres);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "EGBB_APP:Birmingham Radar:123.970:BBR:B:EGBB:APP:-:-:0401:0407:abc:def:ghi:jkl:mno:pqr",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }

        [Fact]
        public void TestItCompilesNoVisCenters()
        {
            ControllerPosition newModel = new ControllerPosition(
                "EGBB_APP",
                "Birmingham Radar",
                "123.970",
                "BBR",
                "B",
                "EGBB",
                "APP",
                "0401",
                "0407",
                new List<Coordinate>(),
                PositionOrder.PRE_POSITION,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            Assert.Equal(
                "EGBB_APP:Birmingham Radar:123.970:BBR:B:EGBB:APP:-:-:0401:0407",
                newModel.GetCompileData(new SectorElementCollection())
            );
        }
    }
}
