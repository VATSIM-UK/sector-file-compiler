using System.Collections.Generic;
using Compiler.Model;
using Xunit;
using Compiler.Output;

namespace CompilerTest.Model
{
    public class SectorElementCollectionTest
    {
        private readonly SectorElementCollection collection;

        public SectorElementCollectionTest()
        {
            this.collection = new SectorElementCollection();
        }

        [Fact]
        public void TestItAddsSids()
        {
            SidStar sidStar = new SidStar("a", "b", "c", "d", new List<string>(), "test");
            this.collection.Add(sidStar);

            Assert.Equal(sidStar, this.collection.SidStars[0]);
        }

        [Fact]
        public void TestItAddsSidsToCompilableSection()
        {
            SidStar sidStar = new SidStar("a", "b", "c", "d", new List<string>(), "test");
            this.collection.Add(sidStar);

            Assert.Equal(sidStar, this.collection.Compilables[OutputSections.ESE_SIDSSTARS][0]);
        }

        [Fact]
        public void TestItAddsColours()
        {
            Colour colour = new Colour("test", 123, "test");
            this.collection.Add(colour);

            Assert.Equal(colour, this.collection.Colours[0]);
        }

        [Fact]
        public void TestItAddsColoursToCompilableSection()
        {
            Colour colour = new Colour("test", 123, "test");
            this.collection.Add(colour);

            Assert.Equal(colour, this.collection.Compilables[OutputSections.SCT_COLOUR_DEFS][0]);
        }

        [Fact]
        public void TestItAddsAirports()
        {
            Airport airport = new Airport("a", "b", new Coordinate("abc", "def"), "123.456", "test");
            this.collection.Add(airport);

            Assert.Equal(airport, this.collection.Airports[0]);
        }

        [Fact]
        public void TestItAddsAirportsToCompilableSection()
        {
            Airport airport = new Airport("a", "b", new Coordinate("abc", "def"), "123.456", "test");
            this.collection.Add(airport);

            Assert.Equal(airport, this.collection.Compilables[OutputSections.SCT_AIRPORT][0]);
        }

        [Fact]
        public void TestItAddsArtccs()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.REGULAR,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.Artccs[0]);
        }

        [Fact]
        public void TestItAddsArtccsToCompilableSection()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.REGULAR,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.Compilables[OutputSections.SCT_ARTCC][0]);
        }

        [Fact]
        public void TestItAddsLowArtccs()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.LOW,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.LowArtccs[0]);
        }

        [Fact]
        public void TestItAddsLowArtccsToCompilableSection()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.LOW,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.Compilables[OutputSections.SCT_ARTCC_LOW][0]);
        }

        [Fact]
        public void TestItAddsHighArtccs()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.HIGH,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.HighArtccs[0]);
        }

        [Fact]
        public void TestItAddsHighArtccsToCompilableSection()
        {
            Artcc artcc = new Artcc(
                "EGTT",
                ArtccType.HIGH,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.Compilables[OutputSections.SCT_ARTCC_HIGH][0]);
        }

        [Fact]
        public void TestItAddsLowAirways()
        {
            Airway airway = new Airway(
                "UN864",
                AirwayType.LOW,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.LowAirways[0]);
        }

        [Fact]
        public void TestItAddsLowAirwaysToCompilableSection()
        {
            Airway airway = new Airway(
                "UN864",
                AirwayType.LOW,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.Compilables[OutputSections.SCT_LOW_AIRWAY][0]);
        }

        [Fact]
        public void TestItAddsHighAirways()
        {
            Airway airway = new Airway(
                "UN864",
                AirwayType.HIGH,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.HighAirways[0]);
        }

        [Fact]
        public void TestItAddsHighAirwaysToCompilableSection()
        {
            Airway airway = new Airway(
                "UN864",
                AirwayType.HIGH,
                new Point(new Coordinate("abc", "def")),
                new Point(new Coordinate("ghi", "jkl")),
                "test"
            );
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.Compilables[OutputSections.SCT_HIGH_AIRWAY][0]);
        }

        [Fact]
        public void TestItAddsFixes()
        {
            Fix fix = new Fix("a", new Coordinate("abc", "def"), "test");
            this.collection.Add(fix);

            Assert.Equal(fix, this.collection.Fixes[0]);
        }

        [Fact]
        public void TestItAddsFixesToCompilableSection()
        {
            Fix fix = new Fix("a", new Coordinate("abc", "def"), "test");
            this.collection.Add(fix);

            Assert.Equal(fix, this.collection.Compilables[OutputSections.SCT_FIXES][0]);
        }

        [Fact]
        public void TestItAddsVors()
        {
            Vor vor = new Vor("a", "123.456", new Coordinate("abc", "def"), "test");
            this.collection.Add(vor);

            Assert.Equal(vor, this.collection.Vors[0]);
        }

        [Fact]
        public void TestItAddsVorsToCompilableSection()
        {
            Vor vor = new Vor("a", "123.456", new Coordinate("abc", "def"), "test");
            this.collection.Add(vor);

            Assert.Equal(vor, this.collection.Compilables[OutputSections.SCT_VOR][0]);
        }

        [Fact]
        public void TestItAddsNdbs()
        {
            Ndb ndb = new Ndb("a", "123.456", new Coordinate("abc", "def"), "test");
            this.collection.Add(ndb);

            Assert.Equal(ndb, this.collection.Ndbs[0]);
        }

        [Fact]
        public void TestItAddsNdbsToCompilableSection()
        {
            Ndb ndb = new Ndb("a", "123.456", new Coordinate("abc", "def"), "test");
            this.collection.Add(ndb);

            Assert.Equal(ndb, this.collection.Compilables[OutputSections.SCT_NDB][0]);
        }

        [Fact]
        public void TestItAddsBlankLines()
        {
            BlankLine blank = new BlankLine();
            this.collection.Add(blank, OutputSections.SCT_COLOUR_DEFS);

            Assert.Equal(blank, this.collection.Compilables[OutputSections.SCT_COLOUR_DEFS][0]);
        }

        [Fact]
        public void TestItAddsCommentLines()
        {
            CommentLine comment = new CommentLine("test");
            this.collection.Add(comment, OutputSections.SCT_COLOUR_DEFS);

            Assert.Equal(comment, this.collection.Compilables[OutputSections.SCT_COLOUR_DEFS][0]);
        }
    }
}
