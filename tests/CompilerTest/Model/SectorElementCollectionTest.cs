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
            collection = new SectorElementCollection();
        }

        [Fact]
        public void TestItAddsSidStars()
        {
            SidStar sidStar = SidStarFactory.Make();
            collection.Add(sidStar);

            Assert.Equal(sidStar, collection.SidStars[0]);
        }

        [Fact]
        public void TestItAddsColours()
        {
            Colour colour = ColourFactory.Make();
            collection.Add(colour);

            Assert.Equal(colour, collection.Colours[0]);
        }

        [Fact]
        public void TestItAddsAirports()
        {
            Airport airport = AirportFactory.Make();
            collection.Add(airport);

            Assert.Equal(airport, collection.Airports[0]);
        }

        [Fact]
        public void TestItAddsRunways()
        {
            Runway runway = RunwayFactory.Make();
            collection.Add(runway);

            Assert.Equal(runway, collection.Runways[0]);
        }

        [Fact]
        public void TestItAddsActiveRunways()
        {
            ActiveRunway runway = ActiveRunwayFactory.Make();
            collection.Add(runway);

            Assert.Equal(runway, collection.ActiveRunways[0]);
        }

        [Fact]
        public void TestItAddsArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make();
            collection.Add(artcc);

            Assert.Equal(artcc, collection.Artccs[0]);
        }

        [Fact]
        public void TestItAddsLowArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make(ArtccType.LOW);
            collection.Add(artcc);

            Assert.Equal(artcc, collection.LowArtccs[0]);
        }

        [Fact]
        public void TestItAddsHighArtccs()
        {
            ArtccSegment artcc = ArtccSegmentFactory.Make(ArtccType.HIGH);
            collection.Add(artcc);

            Assert.Equal(artcc, collection.HighArtccs[0]);
        }

        [Fact]
        public void TestItAddsLowAirways()
        {
            AirwaySegment airway = AirwaySegmentFactory.Make();
            collection.Add(airway);

            Assert.Equal(airway, collection.LowAirways[0]);
        }

        [Fact]
        public void TestItAddsHighAirways()
        {
            AirwaySegment airway = AirwaySegmentFactory.Make(AirwayType.HIGH);
            collection.Add(airway);

            Assert.Equal(airway, collection.HighAirways[0]);
        }

        [Fact]
        public void TestItAddsFixes()
        {
            Fix fix = FixFactory.Make();
            collection.Add(fix);

            Assert.Equal(fix, collection.Fixes[0]);
        }

        [Fact]
        public void TestItAddsGeo()
        {
            Geo geo = GeoFactory.Make();
            collection.Add(geo);

            Assert.Equal(geo, collection.GeoElements[0]);
        }

        [Fact]
        public void TestItAddsLabel()
        {
            Label label = LabelFactory.Make();
            collection.Add(label);

            Assert.Equal(label, collection.Labels[0]);
        }

        [Fact]
        public void TestItAddsRegions()
        {
            Region region = RegionFactory.Make();
            collection.Add(region);

            Assert.Equal(region, collection.Regions[0]);
        }

        [Fact]
        public void TestItAddsVors()
        {
            Vor vor = VorFactory.Make();
            collection.Add(vor);

            Assert.Equal(vor, collection.Vors[0]);
        }

        [Fact]
        public void TestItAddsNdbs()
        {
            Ndb ndb = NdbFactory.Make();
            collection.Add(ndb);

            Assert.Equal(ndb, collection.Ndbs[0]);
        }

        [Fact]
        public void TestItAddsInfo()
        {
            Info info = InfoFactory.Make();
            collection.Add(info);

            Assert.Equal(info, collection.Info);
        }

        [Fact]
        public void TestItAddsFreetext()
        {
            Freetext freetext = FreetextFactory.Make();
            collection.Add(freetext);

            Assert.Equal(freetext, collection.Freetext[0]);
        }

        [Fact]
        public void TestItAddsEsePositions()
        {
            ControllerPosition esePosition = ControllerPositionFactory.Make();
            collection.Add(esePosition);

            Assert.Equal(esePosition, collection.EsePositions[0]);
        }

        [Fact]
        public void TestItAddsSidRoutes()
        {
            SidStarRoute route = SidStarRouteFactory.Make();
            collection.Add(route);
            Assert.Equal(route, collection.SidRoutes[0]);
        }

        [Fact]
        public void TestItAddsStarRoutes()
        {
            SidStarRoute route = SidStarRouteFactory.Make(SidStarType.STAR);
            collection.Add(route);
            Assert.Equal(route, collection.StarRoutes[0]);
        }

        [Fact]
        public void TestItAddsSectorlines()
        {
            Sectorline sectorline = SectorlineFactory.Make();

            collection.Add(sectorline);
            Assert.Equal(sectorline, collection.SectorLines[0]);
        }

        [Fact]
        public void TestItAddsCircleSectorlines()
        {
            CircleSectorline sectorline = CircleSectorlineFactory.Make();

            collection.Add(sectorline);
            Assert.Equal(sectorline, collection.CircleSectorLines[0]);
        }

        [Fact]
        public void TestItAddsSectors()
        {
            Sector sector = SectorFactory.Make();

            collection.Add(sector);
            Assert.Equal(sector, collection.Sectors[0]);
        }

        [Fact]
        public void TestItAddsCoordinationPoints()
        {
            CoordinationPoint coordinationPoint = CoordinationPointFactory.Make();

            collection.Add(coordinationPoint);
            Assert.Equal(coordinationPoint, collection.CoordinationPoints[0]);
        }
        
        [Fact]
        public void TestItAddsRunwayCentrelines()
        {
            RunwayCentreline centreline = RunwayCentrelineFactory.Make();

            collection.Add(centreline);
            Assert.Equal(centreline, collection.RunwayCentrelines[0]);
        }
        
        [Fact]
        public void TestItAddsExtendedRunwayCentrelines()
        {
            FixedColourRunwayCentreline centreline = FixedColourRunwayCentrelineFactory.Make();

            collection.Add(centreline);
            Assert.Equal(centreline, collection.FixedColourRunwayCentrelines[0]);
        }

        [Fact]
        public void TestItAddsGroundNetworks()
        {
            GroundNetwork network = GroundNetworkFactory.Make();
            collection.Add(network);
            Assert.Single(collection.GroundNetworks);
            Assert.Equal(network, collection.GroundNetworks[0]);
        }
        
        [Fact]
        public void TestItAddsRadars()
        {
            Radar radar = RadarFactory.Make();
            collection.Add(radar);
            Assert.Single(collection.Radars);
            Assert.Equal(radar, collection.Radars[0]);
        }
        
        [Fact]
        public void TestItAddsRadarHoles()
        {
            RadarHole hole = RadarHoleFactory.Make();
            collection.Add(hole);
            Assert.Single(collection.RadarHoles);
            Assert.Equal(hole, collection.RadarHoles[0]);
        }
    }
}
