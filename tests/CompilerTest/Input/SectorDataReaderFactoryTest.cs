using System;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataReaderFactoryTest
    {
        [Theory]
        [InlineData(InputDataType.ESE_AGREEMENTS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_FREETEXT, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_GROUND_NETWORK, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_POSITIONS_MENTOR, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_OWNERSHIP, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_POSITIONS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_PRE_POSITIONS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_SECTORLINES, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_SIDS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_STARS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_VRPS, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_RADAR2, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.ESE_RADAR_HOLE, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.RWY_ACTIVE_RUNWAY, typeof(EseSectorDataReader))]
        [InlineData(InputDataType.SCT_AIRPORT_BASIC, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_ARTCC, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_ARTCC_LOW, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_ARTCC_HIGH, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_COLOUR_DEFINITIONS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_RUNWAY_CENTRELINES, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_FIXES, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_GEO, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_INFO, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_LABELS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_NDBS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_REGIONS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_RUNWAYS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_SIDS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_STARS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_LOWER_AIRWAYS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_UPPER_AIRWAYS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.SCT_VORS, typeof(SctSectorDataReader))]
        [InlineData(InputDataType.FILE_HEADERS, typeof(FileHeaderDataReader))]
        public void TestItReturnsCorrectReader(InputDataType inputType, Type expectedReaderType)
        {
            Assert.Equal(expectedReaderType, SectorDataReaderFactory.Create(inputType).GetType());
        }
    }
}
