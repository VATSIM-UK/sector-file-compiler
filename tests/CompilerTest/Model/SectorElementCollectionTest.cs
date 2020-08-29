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
        public void TestItAddsRunways()
        {
            Runway runway = new Runway(
                "09",
                90,
                new Coordinate("abc", "def"),
                "27",
                270,
                new Coordinate("ghi", "jkl"),
                "EGGD - Bristol",
                "comment"
            );
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.Runways[0]);
        }

        [Fact]
        public void TestItAddsRunwaysToCompilableSection()
        {
            Runway runway = new Runway(
                "09",
                90,
                new Coordinate("abc", "def"),
                "27",
                270,
                new Coordinate("ghi", "jkl"),
                "EGGD - Bristol",
                "comment"
            );
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.Compilables[OutputSections.SCT_RUNWAY][0]);
        }

        [Fact]
        public void TestItAddsActiveRunways()
        {
            ActiveRunway runway = new ActiveRunway(
                "33",
                "EGBB",
                1,
                "comment"
            );
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.ActiveRunways[0]);
        }

        [Fact]
        public void TestItAddsActiveRunwaysToCompilableSection()
        {
            ActiveRunway runway = new ActiveRunway(
                "33",
                "EGBB",
                1,
                "comment"
            );
            this.collection.Add(runway);

            Assert.Equal(runway, this.collection.Compilables[OutputSections.RWY_ACTIVE_RUNWAYS][0]);
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
        public void TestItAddsGeo()
        {
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("abc"), new Point("def"), "red", null),
                    new GeoSegment(new Point("ghi"), new Point("jkl"), "blue", null)
                }
            );
            this.collection.Add(geo);

            Assert.Equal(geo, this.collection.GeoElements[0]);
        }

        [Fact]
        public void TestItAddsGeoToCompilableSection()
        {
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("abc"), new Point("def"), "red", null),
                    new GeoSegment(new Point("ghi"), new Point("jkl"), "blue", null)
                }
            );
            this.collection.Add(geo);

            Assert.Equal(geo, this.collection.Compilables[OutputSections.SCT_GEO][0]);
        }

        [Fact]
        public void TestItAddsLabel()
        {
            Label lable = new Label("label1", new Coordinate("abc", "def"), "red", null);
            this.collection.Add(lable);

            Assert.Equal(lable, this.collection.Labels[0]);
        }

        [Fact]
        public void TestItAddsLabelsToCompilableSection()
        {
            Label lable = new Label("label1", new Coordinate("abc", "def"), "red", null);
            this.collection.Add(lable);

            Assert.Equal(lable, this.collection.Compilables[OutputSections.SCT_LABELS][0]);
        }

        [Fact]
        public void TestItAddsRegions()
        {
            Region region = new Region(
                "RegionName",
                "Red",
                new List<Point>(),
                null
            );
            this.collection.Add(region);

            Assert.Equal(region, this.collection.Regions[0]);
        }

        [Fact]
        public void TestItAddsRegionsToCompilableSection()
        {
            Region region = new Region(
                "RegionName",
                "Red",
                new List<Point>(),
                null
            );
            this.collection.Add(region);

            Assert.Equal(region, this.collection.Compilables[OutputSections.SCT_REGIONS][0]);
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
        public void TestItAddsInfo()
        {
            Info info = new Info(
                "Super Cool Sector",
                "LON_CTR",
                "EGLL",
                new Coordinate("123", "456"),
                60,
                40,
                2,
                1
            );
            this.collection.Add(info);

            Assert.Equal(info, this.collection.Info);
        }

        [Fact]
        public void TestItAddsInfoToCompilableSection()
        {
            Info info = new Info(
                "Super Cool Sector",
                "LON_CTR",
                "EGLL",
                new Coordinate("123", "456"),
                60,
                40,
                2,
                1
            );
            this.collection.Add(info);

            Assert.Equal(info, this.collection.Compilables[OutputSections.SCT_INFO][0]);
        }

        [Fact]
        public void TestItAddsFreetext()
        {
            Freetext freetext = new Freetext("a", "b", new Coordinate("abc", "def"), "test");
            this.collection.Add(freetext);

            Assert.Equal(freetext, this.collection.Freetext[0]);
        }

        [Fact]
        public void TestItAddsFreetextToCompilableSection()
        {
            Freetext freetext = new Freetext("a", "b", new Coordinate("abc", "def"), "test");
            this.collection.Add(freetext);

            Assert.Equal(freetext, this.collection.Compilables[OutputSections.ESE_FREETEXT][0]);
        }

        [Fact]
        public void TestItAddsEsePositions()
        {
            ControllerPosition esePosition = new ControllerPosition(
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
                "comment"
            );
            this.collection.Add(esePosition);

            Assert.Equal(esePosition, this.collection.EsePositions[0]);
        }

        [Fact]
        public void TestItAddsEsePositionsToCompilableSection()
        {
            ControllerPosition esePosition = new ControllerPosition(
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
                "comment"
            );
            this.collection.Add(esePosition);

            Assert.Equal(esePosition, this.collection.Compilables[OutputSections.ESE_POSITIONS][0]);
        }

        [Fact]
        public void TestItAddsSidRoutes()
        {
            List<RouteSegment> segments = new List<RouteSegment>()
            {
                new RouteSegment(new Point("BIG"), new Point("LAM"), null),
                new RouteSegment(new Point("LAM"), new Point("BNN"), null),
            };

            SidStarRoute route = new SidStarRoute(SidStarType.SID, "TEST", segments);
            this.collection.Add(route);
            Assert.Equal(route, this.collection.SidRoutes[0]);
        }

        [Fact]
        public void TestItAddsSidRoutesToCompilablesSection()
        {
            List<RouteSegment> segments = new List<RouteSegment>()
            {
                new RouteSegment(new Point("BIG"), new Point("LAM"), null),
                new RouteSegment(new Point("LAM"), new Point("BNN"), null),
            };

            SidStarRoute route = new SidStarRoute(SidStarType.SID, "TEST", segments);
            this.collection.Add(route);
            Assert.Equal(route, this.collection.Compilables[OutputSections.SCT_SID][0]);
        }

        [Fact]
        public void TestItAddsStarRoutes()
        {
            List<RouteSegment> segments = new List<RouteSegment>()
            {
                new RouteSegment(new Point("BIG"), new Point("LAM"), null),
                new RouteSegment(new Point("LAM"), new Point("BNN"), null),
            };

            SidStarRoute route = new SidStarRoute(SidStarType.STAR, "TEST", segments);
            this.collection.Add(route);
            Assert.Equal(route, this.collection.StarRoutes[0]);
        }

        [Fact]
        public void TestItAddsStarRoutesToCompilablesSection()
        {
            List<RouteSegment> segments = new List<RouteSegment>()
            {
                new RouteSegment(new Point("BIG"), new Point("LAM"), null),
                new RouteSegment(new Point("LAM"), new Point("BNN"), null),
            };

            SidStarRoute route = new SidStarRoute(SidStarType.STAR, "TEST", segments);
            this.collection.Add(route);
            Assert.Equal(route, this.collection.Compilables[OutputSections.SCT_STAR][0]);
        }

        [Fact]
        public void TestItAddsSectorlines()
        {
            Sectorline sectorline = new Sectorline(
                "Test Sectorline",
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                new List<SectorlineCoordinate>
                {
                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                },
                "commentname"
            );

            this.collection.Add(sectorline);
            Assert.Equal(sectorline, this.collection.SectorLines[0]);
        }

        [Fact]
        public void TestItAddsSectorlinesToCompilablesSection()
        {
            Sectorline sectorline = new Sectorline(
                "Test Sectorline",
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                new List<SectorlineCoordinate>
                {
                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                },
                "commentname"
            );

            this.collection.Add(sectorline);
            Assert.Equal(
                sectorline,
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]
            );
        }

        [Fact]
        public void TestItAddsCircleSectorlines()
        {
            CircleSectorline sectorline = new CircleSectorline(
                "Test Sectorline",
                "EGGD",
                5.5,
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                "commentname"
            );

            this.collection.Add(sectorline);
            Assert.Equal(sectorline, this.collection.CircleSectorLines[0]);
        }

        [Fact]
        public void TestItAddsCircleSectorlinesToCompilablesSection()
        {
            CircleSectorline sectorline = new CircleSectorline(
                "Test Sectorline",
                "EGGD",
                5.5,
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                "commentname"
            );

            this.collection.Add(sectorline);
            Assert.Equal(
                sectorline,
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]
            );
        }

        [Fact]
        public void TestItAddsSectors()
        {
            Sector sector = new Sector(
                "COOL",
                5000,
                66000,
                new SectorOwnerHierarchy(
                    new List<string>()
                    {
                        "LLN", "LLS"
                    },
                    ""
                ),
                new List<SectorAlternateOwnerHierarchy>()
                {
                    new SectorAlternateOwnerHierarchy("ALT1", new List<string>(){"LON1", "LON2"}, ""),
                    new SectorAlternateOwnerHierarchy("ALT2", new List<string>(){"LON3", "LON4"}, ""),
                },
                new List<SectorActive>()
                {
                    new SectorActive("EGLL", "09R", ""),
                    new SectorActive("EGWU", "05", ""),
                },
                new List<SectorGuest>()
                {
                    new SectorGuest("LLN", "EGLL", "EGWU", ""),
                    new SectorGuest("LLS", "EGLL", "*", ""),
                },
                new SectorBorder(
                new List<string>()
                    {
                        "LINE1",
                        "LINE2",
                    },
                    ""
                ),
                new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                "comment"
            );

            this.collection.Add(sector);
            Assert.Equal(sector, this.collection.Sectors[0]);
        }

        [Fact]
        public void TestItAddsSectorsToCompilablesSection()
        {
            Sector sector = new Sector(
                "COOL",
                5000,
                66000,
                new SectorOwnerHierarchy(
                    new List<string>()
                    {
                        "LLN", "LLS"
                    },
                    ""
                ),
                new List<SectorAlternateOwnerHierarchy>()
                {
                    new SectorAlternateOwnerHierarchy("ALT1", new List<string>(){"LON1", "LON2"}, ""),
                    new SectorAlternateOwnerHierarchy("ALT2", new List<string>(){"LON3", "LON4"}, ""),
                },
                new List<SectorActive>()
                {
                    new SectorActive("EGLL", "09R", ""),
                    new SectorActive("EGWU", "05", ""),
                },
                new List<SectorGuest>()
                {
                    new SectorGuest("LLN", "EGLL", "EGWU", ""),
                    new SectorGuest("LLS", "EGLL", "*", ""),
                },
                new SectorBorder(
                new List<string>()
                    {
                        "LINE1",
                        "LINE2",
                    },
                    ""
                ),
                new SectorArrivalAirports(new List<string>() { "EGSS", "EGGW" }, ""),
                new SectorDepartureAirports(new List<string>() { "EGLL", "EGWU" }, ""),
                "comment"
            );

            this.collection.Add(sector);
            Assert.Equal(
                sector,
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]
            );
        }

        [Fact]
        public void TestItAddsCoordinationPoints()
        {
            CoordinationPoint coordinationPoint = new CoordinationPoint(
                false,
                "*",
                "*",
                "ABTUM",
                "EGKK",
                "26L",
                "TCE",
                "TCSW",
                "*",
                "14000",
                "ABTUMDES",
                "comment"
            );

            this.collection.Add(coordinationPoint);
            Assert.Equal(coordinationPoint, this.collection.CoordinationPoints[0]);
        }

        [Fact]
        public void TestItAddsCoordinationPointsToCompilablesSection()
        {
            CoordinationPoint coordinationPoint = new CoordinationPoint(
                false,
                "*",
                "*",
                "ABTUM",
                "EGKK",
                "26L",
                "TCE",
                "TCSW",
                "*",
                "14000",
                "ABTUMDES",
                "comment"
            );

            this.collection.Add(coordinationPoint);
            Assert.Equal(
                coordinationPoint,
                this.collection.Compilables[OutputSections.ESE_AIRSPACE][0]
            );
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
