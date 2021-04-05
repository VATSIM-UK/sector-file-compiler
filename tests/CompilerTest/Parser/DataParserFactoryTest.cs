using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;
using Compiler.Parser;
using CompilerTest.Bogus.Factory;
using Moq;
using Xunit;

namespace CompilerTest.Parser
{
    public class DataParserFactoryTest
    {
        private readonly DataParserFactory factory;

        public DataParserFactoryTest()
        {
            factory = new DataParserFactory(new SectorElementCollection(), new Mock<IEventLogger>().Object);
        }

        [Theory]
        [InlineData(InputDataType.SCT_COLOUR_DEFINITIONS, typeof(ColourParser))]
        [InlineData(InputDataType.SCT_AIRPORT_BASIC, typeof(AirportParser))]
        [InlineData(InputDataType.SCT_FIXES, typeof(FixParser))]
        [InlineData(InputDataType.SCT_VORS, typeof(VorParser))]
        [InlineData(InputDataType.SCT_NDBS, typeof(NdbParser))]
        [InlineData(InputDataType.SCT_ARTCC, typeof(ArtccParser))]
        [InlineData(InputDataType.SCT_ARTCC_LOW, typeof(ArtccParser))]
        [InlineData(InputDataType.SCT_ARTCC_HIGH, typeof(ArtccParser))]
        [InlineData(InputDataType.SCT_LOWER_AIRWAYS, typeof(AirwayParser))]
        [InlineData(InputDataType.SCT_UPPER_AIRWAYS, typeof(AirwayParser))]
        [InlineData(InputDataType.SCT_SIDS, typeof(SidStarRouteParser))]
        [InlineData(InputDataType.SCT_STARS, typeof(SidStarRouteParser))]
        [InlineData(InputDataType.SCT_GEO, typeof(GeoParser))]
        [InlineData(InputDataType.SCT_LABELS, typeof(LabelParser))]
        [InlineData(InputDataType.SCT_REGIONS, typeof(RegionParser))]
        [InlineData(InputDataType.SCT_INFO, typeof(InfoParser))]
        [InlineData(InputDataType.SCT_RUNWAYS, typeof(RunwayParser))]
        [InlineData(InputDataType.ESE_POSITIONS, typeof(EsePositionParser))]
        [InlineData(InputDataType.ESE_POSITIONS_MENTOR, typeof(EsePositionParser))]
        [InlineData(InputDataType.ESE_FREETEXT, typeof(FreetextParser))]
        [InlineData(InputDataType.ESE_GROUND_NETWORK, typeof(GroundNetworkParser))]
        [InlineData(InputDataType.ESE_SIDS, typeof(SidStarParser))]
        [InlineData(InputDataType.ESE_STARS, typeof(SidStarParser))]
        [InlineData(InputDataType.ESE_SECTORLINES, typeof(SectorlineParser))]
        [InlineData(InputDataType.ESE_AGREEMENTS, typeof(CoordinationPointParser))]
        [InlineData(InputDataType.ESE_OWNERSHIP, typeof(SectorParser))]
        [InlineData(InputDataType.RWY_ACTIVE_RUNWAY, typeof(ActiveRunwayParser))]
        [InlineData(InputDataType.FILE_HEADERS, typeof(HeaderParser))]
        [InlineData(InputDataType.ESE_PRE_POSITIONS, typeof(EsePositionParser))]
        [InlineData(InputDataType.ESE_VRPS, typeof(VrpParser))]
        [InlineData(InputDataType.ESE_RADAR2, typeof(RadarParser))]
        [InlineData(InputDataType.ESE_RADAR_HOLE, typeof(RadarHoleParser))]
        public void TestItReturnsCorrectParser(InputDataType dataType, Type expectedParserType)
        {
            Assert.Equal(
                expectedParserType,
                factory.GetParserForFile(
                    SectorDataFileFactoryFactory.Make(new List<string>()).Create("Test", dataType)
                ).GetType()
            );
        }
    }
}
