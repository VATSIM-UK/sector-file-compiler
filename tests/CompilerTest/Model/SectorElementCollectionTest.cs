using System.Collections.Generic;
using Compiler.Model;
using Xunit;
using CompilerTest.Bogus.Factory;

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
        public void TestItAddsSidStars()
        {
            SidStar sidStar = SidStarFactory.Make();
            this.collection.Add(sidStar);

            Assert.Equal(sidStar, this.collection.SidStars[0]);
        }

        [Fact]
        public void TestItAddsColours()
        {
            Colour colour = ColourFactory.Make();
            this.collection.Add(colour);

            Assert.Equal(colour, this.collection.Colours[0]);
        }

        [Fact]
        public void TestItAddsAirports()
        {
            Airport airport = AirportFactory.Make();
            this.collection.Add(airport);

            Assert.Equal(airport, this.collection.Airports[0]);
        }

        [Fact]
        public void TestItAddsRunways()
        {
            Runway runway = RunwayFactory.Make();
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.Runways[0]);
        }

        [Fact]
        public void TestItAddsActiveRunways()
        {
            ActiveRunway runway = ActiveRunwayFactory.Make();
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.ActiveRunways[0]);
        }

        [Fact]
        public void TestItAddsArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make();
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.Artccs[0]);
        }

        [Fact]
        public void TestItAddsLowArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make(ArtccType.LOW);
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.LowArtccs[0]);
        }

        [Fact]
        public void TestItAddsHighArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make(ArtccType.HIGH);
            this.collection.Add(artcc);

            Assert.Equal(artcc, this.collection.HighArtccs[0]);
        }

        [Fact]
        public void TestItAddsLowAirways()
        {
            AirwaySegment airway = AirwaySegmentFactory.Make();
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.LowAirways[0]);
        }

        [Fact]
        public void TestItAddsHighAirways()
        {
            AirwaySegment airway = AirwaySegmentFactory.Make(AirwayType.HIGH);
            this.collection.Add(airway);

            Assert.Equal(airway, this.collection.HighAirways[0]);
        }

        [Fact]
        public void TestItAddsFixes()
        {
            Fix fix = FixFactory.Make();
            this.collection.Add(fix);

            Assert.Equal(fix, this.collection.Fixes[0]);
        }

        [Fact]
        public void TestItAddsGeo()
        {
            Geo geo = GeoFactory.Make();
            this.collection.Add(geo);

            Assert.Equal(geo, this.collection.GeoElements[0]);
        }

        [Fact]
        public void TestItAddsLabel()
        {
            Label label = LabelFactory.Make();
            this.collection.Add(label);

            Assert.Equal(label, this.collection.Labels[0]);
        }

        [Fact]
        public void TestItAddsRegions()
        {
            Region region = RegionFactory.Make();
            this.collection.Add(region);

            Assert.Equal(region, this.collection.Regions[0]);
        }

        [Fact]
        public void TestItAddsVors()
        {
            Vor vor = VorFactory.Make();
            this.collection.Add(vor);

            Assert.Equal(vor, this.collection.Vors[0]);
        }

        [Fact]
        public void TestItAddsNdbs()
        {
            Ndb ndb = NdbFactory.Make();
            this.collection.Add(ndb);

            Assert.Equal(ndb, this.collection.Ndbs[0]);
        }

        [Fact]
        public void TestItAddsInfo()
        {
            Info info = InfoFactory.Make();
            this.collection.Add(info);

            Assert.Equal(info, this.collection.Info);
        }

        [Fact]
        public void TestItAddsFreetext()
        {
            Freetext freetext = FreetextFactory.Make();
            this.collection.Add(freetext);

            Assert.Equal(freetext, this.collection.Freetext[0]);
        }

        [Fact]
        public void TestItAddsEsePositions()
        {
            ControllerPosition esePosition = ControllerPositionFactory.Make();
            this.collection.Add(esePosition);

            Assert.Equal(esePosition, this.collection.EsePositions[0]);
        }

        [Fact]
        public void TestItAddsSidRoutes()
        {
            SidStarRoute route = SidStarRouteFactory.Make();
            this.collection.Add(route);
            Assert.Equal(route, this.collection.SidRoutes[0]);
        }

        [Fact]
        public void TestItAddsStarRoutes()
        {
            SidStarRoute route = SidStarRouteFactory.Make(SidStarType.STAR);
            this.collection.Add(route);
            Assert.Equal(route, this.collection.StarRoutes[0]);
        }

        [Fact]
        public void TestItAddsSectorlines()
        {
            Sectorline sectorline = SectorlineFactory.Make();

            this.collection.Add(sectorline);
            Assert.Equal(sectorline, this.collection.SectorLines[0]);
        }

        [Fact]
        public void TestItAddsCircleSectorlines()
        {
            CircleSectorline sectorline = CircleSectorlineFactory.Make();

            this.collection.Add(sectorline);
            Assert.Equal(sectorline, this.collection.CircleSectorLines[0]);
        }

        [Fact]
        public void TestItAddsSectors()
        {
            Sector sector = SectorFactory.Make();

            this.collection.Add(sector);
            Assert.Equal(sector, this.collection.Sectors[0]);
        }

        [Fact]
        public void TestItAddsCoordinationPoints()
        {
            CoordinationPoint coordinationPoint = CoordinationPointFactory.Make();

            this.collection.Add(coordinationPoint);
            Assert.Equal(coordinationPoint, this.collection.CoordinationPoints[0]);
        }
    }
}
