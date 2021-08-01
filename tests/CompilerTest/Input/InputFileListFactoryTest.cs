using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Config;
using Compiler.Event;
using Compiler.Input;
using Compiler.Input.Builder;
using Compiler.Input.Generator;
using Compiler.Input.Sorter;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;
using Moq;

namespace CompilerTest.Input
{
    public class InputFileListFactoryTest
    {
        private readonly OutputGroupRepository outputGroups;
        private readonly OutputGroup outputGroup;
        private readonly InputFileList fileList;

        public InputFileListFactoryTest()
        {
            outputGroup = new OutputGroup("test");
            outputGroups = new OutputGroupRepository();
            SectorDataFileFactory sectorDataFileFactory = SectorDataFileFactoryFactory.Make(new List<string>());
            ConfigInclusionRules inclusionRules = new();
            inclusionRules.AddMiscInclusionRule(
                FileInclusionRuleBuilder.Begin()
                    .SetGenerator(new FolderFileListGenerator(ConvertPath("_TestData/InputFileListFactory")))
                    .AddSorter(new AlphabeticalPathSorter())
                    .SetDataType(InputDataType.ESE_AGREEMENTS)
                    .SetOutputGroup(outputGroup)
                    .Build()
            );
            fileList = InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups,
                new Mock<IEventLogger>().Object
            );
        }

        private string ConvertPath(string path)
        {
            return path.Replace('/', Path.DirectorySeparatorChar);
        }

        [Fact]
        public void TestItCreatesFileList()
        {
            List<AbstractSectorDataFile> files = fileList.ToList();
            Assert.Equal(3, files.Count);
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File1.txt"), files[0].FullPath);
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File2.txt"), files[1].FullPath);
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File3.txt"), files[2].FullPath);
        }

        [Fact]
        public void TestItAddsInputFilesToOutputGroup()
        {
            Assert.Equal(
                outputGroup,
                outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File1.txt")))
            );
            Assert.Equal(
                outputGroup,
                outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File2.txt")))
            );
            Assert.Equal(
                outputGroup,
                outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File3.txt")))
            );
        }
        
        [Fact]
        public void TestItAddsOutputGroupToRepository()
        {
            Assert.Equal(
                outputGroup,
                outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File1.txt")))
            );
        }
    }
}
