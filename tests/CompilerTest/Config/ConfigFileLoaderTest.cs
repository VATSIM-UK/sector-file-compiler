using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ConfigFileLoaderTest()
        {
            this.fileLoader = new ConfigFileLoader();
        }
        
        [Theory]
        [InlineData("xyz", "Config file not found")]
        [InlineData("_TestData/ConfigFileLoader/InvalidJson/config.json", "Invalid JSON in _TestData/ConfigFileLoader/InvalidJson/config.json: Error reading JObject from JsonReader. Current JsonReader item is not an object: StartArray. Path '', line 1, position 1.")]
        [InlineData("_TestData/ConfigFileLoader/AirportNotObject/config.json", "Invalid airport config in _TestData/ConfigFileLoader/AirportNotObject/config.json must be an object")]
        [InlineData("_TestData/ConfigFileLoader/AirportKeyNotObject/config.json", "Invalid airport config[Airports] in _TestData/ConfigFileLoader/AirportKeyNotObject/config.json must be an object")]
        [InlineData("_TestData/ConfigFileLoader/InvalidTypeType/config.json", "Invalid type field for section misc.regions - must be \"files\" or \"folders\"")]
        [InlineData("_TestData/ConfigFileLoader/InvalidType/config.json", "Invalid type field for section misc.regions - must be \"files\" or \"folders\"")]
        [InlineData("_TestData/ConfigFileLoader/NoFolder/config.json", "Folder invalid in section enroute.ownership - must be string under key \"folder\"")]
        [InlineData("_TestData/ConfigFileLoader/FolderInvalid/config.json", "Folder invalid in section enroute.ownership - must be string under key \"folder\"")]
        [InlineData("_TestData/ConfigFileLoader/RecursiveInvalid/config.json", "Recursive must be a boolean in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/IncludeAndExclude/config.json", "Cannot specify both include and exclude for folders in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/ExcludeNotAnArray/config.json", "Exclude list must be an array in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/ExcludeFilesNotAString/config.json", "Exclude file must be a string in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/IncludeNotAnArray/config.json", "Include list must be an array in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/IncludeFilesNotAString/config.json", "Include file must be a string in section enroute.ownership")]
        [InlineData("_TestData/ConfigFileLoader/FilesListNotAnArray/config.json", "Files list invalid in section misc.regions - must be array under key \"files\"")]
        [InlineData("_TestData/ConfigFileLoader/FilesListMissing/config.json", "Files list invalid in section misc.regions - must be array under key \"files\"")]
        [InlineData("_TestData/ConfigFileLoader/IgnoreMissingNotBoolean/config.json", "Invalid ignore_missing value in section misc.regions - must be a boolean")]
        [InlineData("_TestData/ConfigFileLoader/ExceptWhereExistsNotString/config.json", "Invalid except_where_exists value in section misc.regions - must be a string")]
        [InlineData("_TestData/ConfigFileLoader/FilePathInvalid/config.json", "Invalid file path in section misc.regions - must be a string")]
        [InlineData("_TestData/ConfigFileLoader/ParentSectionNotArrayOrObject/config.json", "Invalid config section for enroute - must be an object or array of objects") ]
        [InlineData("_TestData/ConfigFileLoader/ParentSectionNotArrayOfObjects/config.json", "Invalid config section for enroute - must be an object or array of objects")]
        public void TestItThrowsExceptionOnBadData(string fileToLoad, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => fileLoader.LoadConfigFiles(new List<string> {fileToLoad})
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
                new List<string> {"_TestData/ConfigFileLoader/ValidConfig/config.json"}
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
            Assert.Equal("foo.txt", ownershipRule2.IncludeExcludeFiles[0]);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule2.GetOutputGroup());
            
            // Enroute ownership folder 3
            FolderInclusionRule ownershipRule3 = (FolderInclusionRule) ruleList[4];
            Assert.Equal(GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Non-UK"), ownershipRule3.Folder);
            Assert.False(ownershipRule3.Recursive);
            Assert.True(ownershipRule3.ExcludeList);
            Assert.Single(ownershipRule3.IncludeExcludeFiles);
            Assert.Equal("eur islands.txt", ownershipRule3.IncludeExcludeFiles[0]);
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
