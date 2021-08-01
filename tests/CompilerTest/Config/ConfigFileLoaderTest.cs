using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Argument;
using Compiler.Config;
using Xunit;
using Compiler.Exception;
using Compiler.Input;
using Compiler.Input.Filter;
using Compiler.Input.Generator;
using Compiler.Input.Rule;
using Compiler.Output;
using FileExists = Compiler.Input.Validator.FileExists;

namespace CompilerTest.Config
{
    public class ConfigFileLoaderTest
    {
        private readonly ConfigFileLoader fileLoader;
        private readonly CompilerArguments arguments;

        public ConfigFileLoaderTest()
        {
            arguments = new CompilerArguments();
            fileLoader = ConfigFileLoaderFactory.Make(arguments);
        }
        
        [Theory]
        [InlineData("xyz", "Config file not found")]
        [InlineData("_TestData/ConfigFileLoader/InvalidJson/config.json", "Invalid JSON in .*?: Error reading JObject from JsonReader. Current JsonReader item is not an object: StartArray\\. Path '', line 1, position 1\\.")]
        [InlineData("_TestData/ConfigFileLoader/NotObject/config.json", "Invalid JSON in .*?: Error reading JObject from JsonReader. Current JsonReader item is not an object: StartArray\\. Path '', line 1, position 1\\.")]
        public void TestItThrowsExceptionOnBadData(string fileToLoad, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => fileLoader.LoadConfigFiles(new List<string> {fileToLoad}, arguments)
            );
            Assert.Matches(expectedMessage, exception.Message);
        }

        private string GetFullFilePath(string relative)
        {
            return Path.GetFullPath(relative.Replace('/', Path.DirectorySeparatorChar));
        }

        [Fact]
        public void TestItLoadsAConfigFile()
        {
            ConfigInclusionRules rules = fileLoader.LoadConfigFiles(
                new List<string> {"_TestData/ConfigFileLoader/ValidConfig/config.json"}, arguments
            );

            List<IInclusionRule> ruleList = rules.ToList();
            Assert.Equal(6, ruleList.Count);

            // Airport - Basic
            InclusionRule airportBasicRule = (InclusionRule) ruleList[0];
            Assert.IsType<FileListGenerator>(airportBasicRule.ListGenerator);
            Assert.Equal(2, airportBasicRule.ListGenerator.GetPaths().Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/Basic2.txt"),
                },
                airportBasicRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportBasicRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.DoesNotContain(airportBasicRule.Filters, validator => validator.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Equal(InputDataType.SCT_AIRPORT_BASIC, airportBasicRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_AIRPORT_BASIC.EGLL", "Start EGLL Basic"), airportBasicRule.GetOutputGroup());
            
            // Airport - Geo
            InclusionRule airportGeoRule = (InclusionRule) ruleList[1];
            Assert.IsType<FileListGenerator>(airportGeoRule.ListGenerator);
            Assert.Single(airportGeoRule.ListGenerator.GetPaths());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/SMR/Geo.txt"),
                },
                airportGeoRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportGeoRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.Contains(airportGeoRule.Filters, validator => validator.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Equal(
                GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Airports/EGLL/SMR/Foo.txt"),
                (airportGeoRule.Filters.Where(validator => validator.GetType() == typeof(IgnoreWhenFileExists)).FirstOrDefault() as IgnoreWhenFileExists)?.FileToCheckAgainst
            );
            Assert.Equal(InputDataType.SCT_GEO, airportGeoRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_GEO.EGLL", "Start EGLL Geo"), airportGeoRule.GetOutputGroup());
            
            // Enroute ownership folder 1
            InclusionRule ownershipRule1 = (InclusionRule) ruleList[2];
            Assert.Equal(
                new List<string>{GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Alternate/Foo.txt")},
                ownershipRule1.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<RecursiveFolderFileListGenerator>(ownershipRule1.ListGenerator);
            Assert.Empty(ownershipRule1.Filters);
            Assert.Empty(ownershipRule1.Validators);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule1.GetOutputGroup());
            
            // Enroute ownership folder 2
            InclusionRule ownershipRule2 = (InclusionRule) ruleList[3];
            Assert.Equal(
                new List<string>{GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Foo/Foo.txt")},
                ownershipRule2.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<FolderFileListGenerator>(ownershipRule2.ListGenerator);
            Assert.Contains(ownershipRule2.Filters, fileFilter => fileFilter.GetType() == typeof(IncludeFileFilter));
            var filter = ownershipRule2.Filters.First(
                fileFilter => fileFilter.GetType() == typeof(IncludeFileFilter)
            ) as IncludeFileFilter ?? throw new InvalidOperationException();
            Assert.Single(filter.FileNames);
            Assert.Equal("Foo.txt", filter.FileNames.First());
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule2.GetOutputGroup());
            
            // Enroute ownership folder 3
            InclusionRule ownershipRule3 = (InclusionRule) ruleList[4];
            Assert.Equal(
                new List<string>{GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Ownership/Non-UK/EUR Islands.txt")},
                ownershipRule3.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<FolderFileListGenerator>(ownershipRule3.ListGenerator);
            Assert.Contains(ownershipRule3.Filters, fileFilter => fileFilter.GetType() == typeof(ExcludeFileFilter));
            var excludeFileFilter = ownershipRule3.Filters.First(
                fileFilter => fileFilter.GetType() == typeof(ExcludeFileFilter)
            ) as ExcludeFileFilter ?? throw new InvalidOperationException();
            Assert.Single(excludeFileFilter.FileNames);
            Assert.Equal("EUR Islands.txt", excludeFileFilter.FileNames.First());
            
            Assert.DoesNotContain(ownershipRule3.Filters, fileFilter => fileFilter.GetType() == typeof(FilePatternFilter));
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"), ownershipRule3.GetOutputGroup());
            
            // Misc regions
            InclusionRule miscRegions = (InclusionRule) ruleList[5];
            Assert.IsType<FileListGenerator>(miscRegions.ListGenerator);
            Assert.Equal(3, miscRegions.ListGenerator.GetPaths().Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_LTMA Airfield CAS.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_Severn Buffers.txt"),
                    GetFullFilePath("_TestData/ConfigFileLoader/ValidConfig/Misc/Regions_Uncontrolled airspace.txt"),
                },
                miscRegions.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportGeoRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.DoesNotContain(airportBasicRule.Filters, validator => validator.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Equal(InputDataType.SCT_AIRPORT_BASIC, airportBasicRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_AIRPORT_BASIC.EGLL", "Start EGLL Basic"), airportBasicRule.GetOutputGroup());
        }
    }
}
