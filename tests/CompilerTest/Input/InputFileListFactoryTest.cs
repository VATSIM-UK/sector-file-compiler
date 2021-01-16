using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Input
{
    public class InputFileListFactoryTest
    {
        private OutputGroupRepository outputGroups;
        private OutputGroup outputGroup;
        private SectorDataFileFactory sectorDataFileFactory;
        private ConfigInclusionRules inclusionRules;

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
            
            Assert.Equal(3, fileList.Count());
            using var enumerator = fileList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File1.txt", enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File2.txt", enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File3.txt", enumerator.Current.FullPath);
        }

        [Fact]
        public void TestItAddsInputFilesToOutputGroup()
        {
            InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups
            );
            
            Assert.Equal(3, this.outputGroup.FileList.Count);
            using var iterator = this.outputGroup.FileList.GetEnumerator();
            iterator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File1.txt", iterator.Current);
            iterator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File2.txt", iterator.Current);
            iterator.MoveNext();
            Assert.Equal("_TestData/InputFileListFactory\\File3.txt", iterator.Current);
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
