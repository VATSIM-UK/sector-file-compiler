using System;
using Compiler.Collector;
using Compiler.Model;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Collector
{
    public class CompilableElementCollectorFactoryTest
    {
        private readonly CompilableElementCollectorFactory factory;

        public CompilableElementCollectorFactoryTest()
        {
            factory = new CompilableElementCollectorFactory(
                new SectorElementCollection(),
                new OutputGroupRepository()
            );
        }

        [Theory]
        [InlineData(OutputSectionKeys.FILE_HEADER, typeof(HeaderCollector))]
        [InlineData(OutputSectionKeys.SCT_COLOUR_DEFS, typeof(ColoursCollector))]
        [InlineData(OutputSectionKeys.SCT_INFO, typeof(InfoCollector))]
        [InlineData(OutputSectionKeys.SCT_AIRPORT, typeof(AirportsCollector))]
        [InlineData(OutputSectionKeys.SCT_RUNWAY, typeof(RunwaysCollector))]
        [InlineData(OutputSectionKeys.SCT_VOR, typeof(VorsCollector))]
        [InlineData(OutputSectionKeys.SCT_NDB, typeof(NdbsCollector))]
        [InlineData(OutputSectionKeys.SCT_FIXES, typeof(FixesCollector))]
        [InlineData(OutputSectionKeys.SCT_GEO, typeof(GeoCollector))]
        [InlineData(OutputSectionKeys.SCT_HIGH_AIRWAY, typeof(HighAirwaysCollector))]
        [InlineData(OutputSectionKeys.SCT_LOW_AIRWAY, typeof(LowAirwaysCollector))]
        [InlineData(OutputSectionKeys.SCT_ARTCC, typeof(ArtccCollector))]
        [InlineData(OutputSectionKeys.SCT_ARTCC_LOW, typeof(LowArtccCollector))]
        [InlineData(OutputSectionKeys.SCT_ARTCC_HIGH, typeof(HighArtccCollector))]
        [InlineData(OutputSectionKeys.SCT_SID, typeof(SidsCollector))]
        [InlineData(OutputSectionKeys.SCT_STAR, typeof(StarsCollector))]
        [InlineData(OutputSectionKeys.SCT_LABELS, typeof(LabelsCollector))]
        [InlineData(OutputSectionKeys.SCT_REGIONS, typeof(RegionsCollector))]
        [InlineData(OutputSectionKeys.ESE_POSITIONS, typeof(PositionsCollector))]
        [InlineData(OutputSectionKeys.ESE_FREETEXT, typeof(FreetextCollector))]
        [InlineData(OutputSectionKeys.ESE_SIDSSTARS, typeof(SidStarsCollector))]
        [InlineData(OutputSectionKeys.ESE_AIRSPACE, typeof(AirspaceCollector))]
        [InlineData(OutputSectionKeys.ESE_GROUND_NETWORK, typeof(GroundNetworkCollector))]
        [InlineData(OutputSectionKeys.RWY_ACTIVE_RUNWAYS, typeof(ActiveRunwaysCollector))]
        [InlineData(OutputSectionKeys.ESE_RADAR, typeof(RadarCollector))]
        public void TestItReturnsCorrectCollector(OutputSectionKeys outputType, Type expectedType)
        {
            Assert.Equal(
                expectedType,
                factory.GetCollectorForOutputSection(outputType).GetType()
            );
        }
    }
}
