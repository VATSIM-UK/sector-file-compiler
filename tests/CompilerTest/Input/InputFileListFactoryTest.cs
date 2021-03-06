﻿using System.Collections.Generic;
using System.IO;
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
                    ConvertPath("_TestData/InputFileListFactory"),
                    false,
                    InputDataType.ESE_AGREEMENTS,
                    this.outputGroup
                )    
            );
        }

        private string ConvertPath(string path)
        {
            return path.Replace('/', Path.DirectorySeparatorChar);
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
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File1.txt"), files[0].FullPath);
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File2.txt"), files[1].FullPath);
            Assert.Equal(ConvertPath("_TestData/InputFileListFactory/File3.txt"), files[2].FullPath);
        }

        [Fact]
        public void TestItAddsInputFilesToOutputGroup()
        {
            InputFileListFactory.CreateFromInclusionRules(
                sectorDataFileFactory,
                inclusionRules,
                outputGroups
            );
            
            Assert.Equal(
                this.outputGroup,
                this.outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File1.txt")))
            );
            Assert.Equal(
                this.outputGroup,
                this.outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File2.txt")))
            );
            Assert.Equal(
                this.outputGroup,
                this.outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File3.txt")))
            );
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
                this.outputGroups.GetForDefinitionFile(DefinitionFactory.Make(ConvertPath("_TestData/InputFileListFactory/File1.txt")))
            );
        }
    }
}
