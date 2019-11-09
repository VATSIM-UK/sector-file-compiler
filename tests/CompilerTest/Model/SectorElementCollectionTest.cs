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
