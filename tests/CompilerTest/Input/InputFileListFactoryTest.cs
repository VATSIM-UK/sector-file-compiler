using System.Collections.Generic;
using System.Linq;
using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Input
{
    public class InputFileListFactoryTest
    {
        private readonly OutputGroupRepository outputGroups;
        private readonly OutputGroup outputGroup;
        private readonly SectorDataFileFactory sectorDataFileFactory;
        private readonly ConfigInclusionRules inclusionRules;
        
        public InputFileListFactoryTest()
        {
            this.outputGroup = new OutputGroup("test");
            this.outputGroups = new OutputGroupRepository();
            this.sectorDataFileFactory = SectorDataFileFactoryFactory.Make(new List<string>());
            this.inclusionRules = new ConfigInclusionRules();
            inclusionRules.AddMiscInclusionRule(
                new FolderInclusionRule(
                    "_TestData/InputFileListFactory",
                    false,
                    InputDataType.ESE_AGREEMENTS,
                    this.outputGroup
                )    
            );
        }

        [Fact]
        public void TestItCreatesFileList()
        {
            InputFileList fileList = InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups
            );
            
            List<AbstractSectorDataFile> files = fileList.ToList();
            Assert.Equal(3, files.Count);
            Assert.Equal("_TestData/InputFileListFactory\\File1.txt", files[0].FullPath);
            Assert.Equal("_TestData/InputFileListFactory\\File2.txt", files[1].FullPath);
            Assert.Equal("_TestData/InputFileListFactory\\File3.txt", files[2].FullPath);
        }

        [Fact]
        public void TestItAddsInputFilesToOutputGroup()
        {
            InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups
            );
            
            List<string> files = this.outputGroup.FileList.ToList();
            Assert.Equal(3, files.Count);
            Assert.Equal("_TestData/InputFileListFactory\\File1.txt", files[0]);
            Assert.Equal("_TestData/InputFileListFactory\\File2.txt", files[1]);
            Assert.Equal("_TestData/InputFileListFactory\\File3.txt", files[2]);
        }
        
        [Fact]
        public void TestItAddsInpuOutputGroupToRepository()
        {
            InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups
            );

            Assert.Equal(
                this.outputGroup,
                this.outputGroups.GetForDefinitionFile(DefinitionFactory.Make("_TestData/InputFileListFactory\\File1.txt"))
            );
        }
    }
}
