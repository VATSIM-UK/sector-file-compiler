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
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using FileExists = Compiler.Input.Validator.FileExists;

namespace CompilerTest.Config
{
    public class ConfigIncludeLoaderTest
    {
        private readonly ConfigIncludeLoader fileLoader;
        private readonly ConfigInclusionRules includes;
        private readonly ITestOutputHelper output;

        public ConfigIncludeLoaderTest(ITestOutputHelper output)
        {
            fileLoader = ConfigIncludeLoaderFactory.Make(new CompilerArguments());
            includes = new ConfigInclusionRules();
            this.output = output;
        }

        [Theory]
        [InlineData("_TestData/ConfigIncludeLoader/AirportNotObject/config.json",
            "Invalid airport config in _TestData/ConfigIncludeLoader/AirportNotObject/config.json must be an object")]
        [InlineData("_TestData/ConfigIncludeLoader/AirportKeyNotObject/config.json",
            "Invalid airport config[Airports] in _TestData/ConfigIncludeLoader/AirportKeyNotObject/config.json must be an object")]
        [InlineData("_TestData/ConfigIncludeLoader/InvalidTypeType/config.json",
            "Invalid type field for section misc.regions - must be \"files\" or \"folders\"")]
        [InlineData("_TestData/ConfigIncludeLoader/InvalidType/config.json",
            "Invalid type field for section misc.regions - must be \"files\" or \"folders\"")]
        [InlineData("_TestData/ConfigIncludeLoader/NoFolder/config.json",
            "Folder invalid in section enroute.ownership - must be string under key \"folder\"")]
        [InlineData("_TestData/ConfigIncludeLoader/FolderInvalid/config.json",
            "Folder invalid in section enroute.ownership - must be string under key \"folder\"")]
        [InlineData("_TestData/ConfigIncludeLoader/RecursiveInvalid/config.json",
            "Recursive must be a boolean in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/IncludeAndExclude/config.json",
            "Cannot specify both include and exclude for folders in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/ExcludeNotAnArray/config.json",
            "Exclude list must be an array in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/ExcludeFilesNotAString/config.json",
            "Exclude file must be a string in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/IncludeNotAnArray/config.json",
            "Include list must be an array in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/IncludeFilesNotAString/config.json",
            "Include file must be a string in section enroute.ownership")]
        [InlineData("_TestData/ConfigIncludeLoader/FilesListNotAnArray/config.json",
            "Files list invalid in section misc.regions - must be array under key \"files\"")]
        [InlineData("_TestData/ConfigIncludeLoader/FilesListMissing/config.json",
            "Files list invalid in section misc.regions - must be array under key \"files\"")]
        [InlineData("_TestData/ConfigIncludeLoader/IgnoreMissingNotBoolean/config.json",
            "Invalid ignore_missing value in section misc.regions - must be a boolean")]
        [InlineData("_TestData/ConfigIncludeLoader/ExceptWhereExistsNotString/config.json",
            "Invalid except_where_exists value in section misc.regions - must be a string")]
        [InlineData("_TestData/ConfigIncludeLoader/FilePathInvalid/config.json",
            "Invalid file path in section misc.regions - must be a string")]
        [InlineData("_TestData/ConfigIncludeLoader/ParentSectionNotArrayOrObject/config.json",
            "Invalid config section for enroute - must be an object or array of objects")]
        [InlineData("_TestData/ConfigIncludeLoader/ParentSectionNotArrayOfObjects/config.json",
            "Invalid config section for enroute - must be an object or array of objects")]
        [InlineData("_TestData/ConfigIncludeLoader/PatternNotAString/config.json",
            "Pattern invalid in section enroute.ownership - must be a regular expression string")]
        [InlineData("_TestData/ConfigIncludeLoader/ExcludeDirectoryNotAnArray/config.json",
            "Invalid exclude_directory invalid in section misc.regions - must be an array of strings")]
        [InlineData("_TestData/ConfigIncludeLoader/ExcludeDirectoryContainsNonString/config.json",
            "Invalid exclude_directory invalid in section misc.regions - must be an array of strings")]
        public void TestItThrowsExceptionOnBadData(string fileToLoad, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => fileLoader.LoadConfig(includes, JObject.Parse(File.ReadAllText(fileToLoad)), fileToLoad)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }

        private string GetFullFilePath(string relative)
        {
            return Path.GetFullPath(relative.Replace('/', Path.DirectorySeparatorChar));
        }

        [Fact]
        public void TestItHandlesNoIncludes()
        {
            fileLoader.LoadConfig(
                includes,
                JObject.Parse(File.ReadAllText("_TestData/ConfigIncludeLoader/NoIncludes/config.json")),
                "_TestData/ConfigIncludeLoader/NoIncludes/config.json"
            );
            Assert.Empty(includes);
        }

        [Fact]
        public void TestItLoadsAConfigFile()
        {
            fileLoader.LoadConfig(
                includes,
                JObject.Parse(File.ReadAllText("_TestData/ConfigIncludeLoader/ValidConfig/config.json")),
                "_TestData/ConfigIncludeLoader/ValidConfig/config.json"
            );

            List<IInclusionRule> ruleList = includes.ToList();
            Assert.Equal(6, ruleList.Count);

            // Airport - Basic
            InclusionRule airportBasicRule = (InclusionRule)ruleList[0];
            Assert.IsType<FileListGenerator>(airportBasicRule.ListGenerator);
            Assert.Equal(2, airportBasicRule.ListGenerator.GetPaths().Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Airports/EGLL/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Airports/EGLL/Basic2.txt"),
                },
                airportBasicRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportBasicRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.DoesNotContain(airportBasicRule.Filters,
                fileFilter => fileFilter.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Contains(airportBasicRule.Filters,
                fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder));
            Assert.Contains(
                "EGLL",
                (airportBasicRule.Filters.First(fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder)) as
                    ExcludeByParentFolder)?.ParentFolders
                ?? throw new InvalidOperationException()
            );
            Assert.Equal(InputDataType.SCT_AIRPORT_BASIC, airportBasicRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_AIRPORT_BASIC.EGLL", "Start EGLL Basic"),
                airportBasicRule.GetOutputGroup());

            // Airport - Geo
            InclusionRule airportGeoRule = (InclusionRule)ruleList[1];
            Assert.IsType<FileListGenerator>(airportGeoRule.ListGenerator);
            Assert.Single(airportGeoRule.ListGenerator.GetPaths());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Airports/EGLL/SMR/Geo.txt"),
                },
                airportGeoRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportGeoRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.Contains(airportGeoRule.Filters, validator => validator.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Equal(
                GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Airports/EGLL/SMR/Foo.txt"),
                (airportGeoRule.Filters.Where(validator => validator.GetType() == typeof(IgnoreWhenFileExists))
                    .FirstOrDefault() as IgnoreWhenFileExists)?.FileToCheckAgainst
            );
            Assert.Equal(InputDataType.SCT_GEO, airportGeoRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_GEO.EGLL", "Start EGLL Geo"), airportGeoRule.GetOutputGroup());

            // Enroute ownership folder 1
            InclusionRule ownershipRule1 = (InclusionRule)ruleList[2];
            Assert.Equal(
                new List<string>
                    { GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Ownership/Alternate/Foo.txt") },
                ownershipRule1.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<RecursiveFolderFileListGenerator>(ownershipRule1.ListGenerator);
            Assert.Empty(ownershipRule1.Filters);
            Assert.Empty(ownershipRule1.Validators);
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"),
                ownershipRule1.GetOutputGroup());

            // Enroute ownership folder 2
            InclusionRule ownershipRule2 = (InclusionRule)ruleList[3];
            Assert.Equal(
                new List<string> { GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Ownership/Foo/Foo.txt") },
                ownershipRule2.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<FolderFileListGenerator>(ownershipRule2.ListGenerator);
            Assert.Contains(ownershipRule2.Filters, fileFilter => fileFilter.GetType() == typeof(IncludeFileFilter));
            var filter = ownershipRule2.Filters.First(
                fileFilter => fileFilter.GetType() == typeof(IncludeFileFilter)
            ) as IncludeFileFilter ?? throw new InvalidOperationException();
            Assert.Single(filter.FileNames);
            Assert.Equal("Foo.txt", filter.FileNames.First());
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"),
                ownershipRule2.GetOutputGroup());

            // Enroute ownership folder 3
            InclusionRule ownershipRule3 = (InclusionRule)ruleList[4];
            Assert.Equal(
                new List<string>
                    { GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Ownership/Non-UK/EUR Islands.txt") },
                ownershipRule3.ListGenerator.GetPaths().ToList()
            );
            Assert.IsType<FolderFileListGenerator>(ownershipRule3.ListGenerator);
            Assert.Contains(ownershipRule3.Filters, fileFilter => fileFilter.GetType() == typeof(ExcludeFileFilter));
            var excludeFileFilter = ownershipRule3.Filters.First(
                fileFilter => fileFilter.GetType() == typeof(ExcludeFileFilter)
            ) as ExcludeFileFilter ?? throw new InvalidOperationException();
            Assert.Single(excludeFileFilter.FileNames);
            Assert.Equal("EUR Islands.txt", excludeFileFilter.FileNames.First());

            Assert.Contains(ownershipRule3.Filters, fileFilter => fileFilter.GetType() == typeof(FilePatternFilter));
            var patternFilter = ownershipRule3.Filters.First(
                fileFilter => fileFilter.GetType() == typeof(FilePatternFilter)
            ) as FilePatternFilter ?? throw new InvalidOperationException();
            Assert.Equal(".*?", patternFilter.Pattern.ToString());
            Assert.Equal(new OutputGroup("enroute.ESE_OWNERSHIP", "Start enroute Ownership"),
                ownershipRule3.GetOutputGroup());

            // Misc regions
            InclusionRule miscRegions = (InclusionRule)ruleList[5];
            Assert.IsType<FileListGenerator>(miscRegions.ListGenerator);
            Assert.Equal(3, miscRegions.ListGenerator.GetPaths().Count());
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Misc/Regions_LTMA Airfield CAS.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Misc/Regions_Severn Buffers.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/ValidConfig/Misc/Regions_Uncontrolled airspace.txt"),
                },
                miscRegions.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(airportBasicRule.Validators, validator => validator.GetType() == typeof(FileExists));
            Assert.DoesNotContain(airportBasicRule.Filters,
                validator => validator.GetType() == typeof(IgnoreWhenFileExists));
            Assert.Equal(InputDataType.SCT_AIRPORT_BASIC, airportBasicRule.DataType);
            Assert.Equal(new OutputGroup("airport.SCT_AIRPORT_BASIC.EGLL", "Start EGLL Basic"),
                airportBasicRule.GetOutputGroup());
        }

        [Fact]
        public void TestItLoadsAirportConfigInRuleOrder()
        {
            fileLoader.LoadConfig(
                includes,
                JObject.Parse(File.ReadAllText("_TestData/ConfigIncludeLoader/AirportOrder/config.json")),
                "_TestData/ConfigIncludeLoader/AirportOrder/config.json"
            );

            List<IInclusionRule> ruleList = includes.ToList();
            Assert.Equal(4, ruleList.Count);

            // Airport - Basic, first rule for EGKK
            InclusionRule gatwickFirstRule = (InclusionRule)ruleList[0];
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGKK/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGKK/Basic2.txt"),
                },
                gatwickFirstRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(gatwickFirstRule.Filters,
                fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder));
            Assert.Contains(
                "EGKK",
                (gatwickFirstRule.Filters.First(
                    fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder)) as ExcludeByParentFolder)
                ?.ParentFolders
                ?? throw new InvalidOperationException()
            );

            // Airport - Basic, first rule for EGLL
            InclusionRule heathrowFirstRule = (InclusionRule)ruleList[1];
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGLL/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGLL/Basic2.txt"),
                },
                heathrowFirstRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Contains(heathrowFirstRule.Filters,
                fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder));
            Assert.Contains(
                "EGKK",
                (heathrowFirstRule.Filters.First(
                    fileFilter => fileFilter.GetType() == typeof(ExcludeByParentFolder)) as ExcludeByParentFolder)
                ?.ParentFolders
                ?? throw new InvalidOperationException()
            );

            // Airport - Basic, second rule for EGKK
            InclusionRule gatwickSecondRule = (InclusionRule)ruleList[2];
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGKK/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGKK/Basic2.txt"),
                },
                gatwickSecondRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Empty(gatwickSecondRule.Filters);

            // Airport - Basic, second rule for EGLL
            InclusionRule heathrowSecondRule = (InclusionRule)ruleList[3];
            Assert.Equal(
                new List<string>
                {
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGLL/Basic.txt"),
                    GetFullFilePath("_TestData/ConfigIncludeLoader/AirportOrder/Airports/EGLL/Basic2.txt"),
                },
                heathrowSecondRule.ListGenerator.GetPaths().ToList()
            );
            Assert.Empty(heathrowSecondRule.Filters);
        }
    }
}
