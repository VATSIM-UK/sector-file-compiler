using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Argument;
using Compiler.Config;
using Xunit;
using Compiler.Exception;
using Compiler.Input;
using Compiler.Output;

namespace CompilerTest.Config
{
    public class ConfigFileLoaderTest
    {
        private readonly ConfigFileLoader fileLoader;
        private readonly CompilerArguments arguments;

        public ConfigFileLoaderTest()
        {
            this.fileLoader = ConfigFileLoaderFactory.Make();
            this.arguments = new CompilerArguments();
        }
        
        [Theory]
        [InlineData("xyz", "Config file not found")]
        [InlineData("_TestData/ConfigFileLoader/InvalidJson/config.json", "Invalid JSON in _TestData/ConfigFileLoader/InvalidJson/config.json: Error reading JObject from JsonReader. Current JsonReader item is not an object: StartArray. Path '', line 1, position 1.")]
        [InlineData("_TestData/ConfigFileLoader/NotObject/config.json", "Invalid JSON in _TestData/ConfigFileLoader/NotObject/config.json: Error reading JObject from JsonReader. Current JsonReader item is not an object: StartArray. Path '', line 1, position 1.")]
        public void TestItThrowsExceptionOnBadData(string fileToLoad, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => fileLoader.LoadConfigFiles(new List<string> {fileToLoad}, this.arguments)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }

        private string GetFullFilePath(string relative)
        {
            return Path.GetFullPath(relative.Replace('/', Path.DirectorySeparatorChar));
        }

        [Fact]
        public void TestItLoadsAConfigFile()
        {
            ConfigInclusionRules rules = fileLoader.LoadConfigFiles(
                new List<string> {"_TestData/ConfigFileLoader/ValidConfig/config.json"}, this.arguments
            );

            List<IInclusionRule> ruleList = rules.ToList();
            Assert.Equal(6, ruleList.Count);
            

            // Airport - Basic
            FileListInclusionRule airportBasicRule = (FileListInclusionRule) ruleList[0];
            Assert.Equal(2, airportBasicRule.FileList.Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/Basic2.txt"),
                },
                airportBasicRule.FileList.ToList()
            );
            Assert.False(airportBasicRule.IgnoreMissing);
            Assert.Equal("", airportBasicRule.ExceptWhereExists);
            Assert.Equal(InputDataType.SCT_AIRPORT_BASIC, airportBasicRule.InputDataType);
            Assert.Equal(new OutputGroup("airport.SCT_AIRPORT_BASIC.EGLL", "Start EGLL Basic"), airportBasicRule.GetOutputGroup());
            
            // Airport - Geo
            FileListInclusionRule airportGeoRule = (FileListInclusionRule) ruleList[1];
            Assert.Single(airportGeoRule.FileList);
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/SMR/Geo.txt"),
                },
                airportGeoRule.FileList.ToList()
            );
            Assert.True(airportGeoRule.IgnoreMissing);
            Assert.Equal(GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/SMR/Foo.txt"), airportGeoRule.ExceptWhereExists);
            Assert.Equal(InputDataType.SCT_GEO, airportGeoRule.InputDataType);
            Assert.Equal(new OutputGroup("airport.SCT_GEO.EGLL", "Start EGLL Geo"), airportGeoRule.GetOutputGroup());
            
            // Enroute ownership folder 1
            FolderInclusionRule ownershipRule1 = (FolderInclusionRule) ruleList[2];
            Assert.Equal(GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Alternate"), ownershipRule1.Folder);
            Assert.True(ownershipRule1.Recursive);
            Assert.True(ownershipRule1.ExcludeList);
            Assert.Empty(ownershipRule1.IncludeExcludeFiles);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule1.GetOutputGroup());
            
            // Enroute ownership folder 2
            FolderInclusionRule ownershipRule2 = (FolderInclusionRule) ruleList[3];
            Assert.Equal(GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Foo"), ownershipRule2.Folder);
            Assert.False(ownershipRule2.Recursive);
            Assert.False(ownershipRule2.ExcludeList);
            Assert.Single(ownershipRule2.IncludeExcludeFiles);
            Assert.Equal("Foo.txt", ownershipRule2.IncludeExcludeFiles[0]);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule2.GetOutputGroup());
            
            // Enroute ownership folder 3
            FolderInclusionRule ownershipRule3 = (FolderInclusionRule) ruleList[4];
            Assert.Equal(GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Non-UK"), ownershipRule3.Folder);
            Assert.False(ownershipRule3.Recursive);
            Assert.True(ownershipRule3.ExcludeList);
            Assert.Single(ownershipRule3.IncludeExcludeFiles);
            Assert.Equal("EUR Islands.txt", ownershipRule3.IncludeExcludeFiles[0]);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule3.GetOutputGroup());
            
            // Misc regions
            FileListInclusionRule miscRegions = (FileListInclusionRule) ruleList[5];
            Assert.Equal(3, miscRegions.FileList.Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_LTMA Airfield CAS.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_Severn Buffers.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_Uncontrolled airspace.txt"),
                },
                miscRegions.FileList.ToList()
            );
            Assert.False(miscRegions.IgnoreMissing);
            Assert.Equal("", miscRegions.ExceptWhereExists);
            Assert.Equal(InputDataType.SCT_REGIONS, miscRegions.InputDataType);
            Assert.Equal(new OutputGroup("misc.SCT_REGIONS", "Start misc Regions"), miscRegions.GetOutputGroup());
        }
    }
}
