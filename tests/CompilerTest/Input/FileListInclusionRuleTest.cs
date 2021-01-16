using System.Collections.Generic;
using System.Linq;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Compiler.Exception;
using Xunit;

namespace CompilerTest.Input
{
    public class FileListInclusionRuleTest
    {
        private readonly SectorDataFileFactory fileFactory = SectorDataFileFactoryFactory.Make();
        
        private string GetFilePath(string filename)
        {
            return $"_TestData/FileListInclusionRule/{filename}";
        }
        
        [Fact]
        public void TestItLoadsFiles()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("File1.txt"),
                    this.GetFilePath("File2.txt"),
                },
                false,
                "",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(2, files.Count);
            Assert.Equal(this.GetFilePath("File1.txt"), files[0].FullPath);
            Assert.Equal(this.GetFilePath("File2.txt"), files[1].FullPath);
        }
        
        [Fact]
        public void TestItLoadsFilesCaseInsensitive()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("file1.txt"),
                    this.GetFilePath("file2.txt"),
                },
                false,
                "",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);

            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(2, files.Count);
            Assert.Equal(this.GetFilePath("file1.txt"), files[0].FullPath);
            Assert.Equal(this.GetFilePath("file2.txt"), files[1].FullPath);
        }
        
        [Fact]
        public void TestItLoadsFilesAndIgnoresMissingWhenSet()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("File1.txt"),
                    this.GetFilePath("File4.txt"),
                },
                true,
                "",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Single(files);
            Assert.Equal(this.GetFilePath("File1.txt"), files[0].FullPath);
        }
        
        [Fact]
        public void TestItDoesntIncludeFilesWhereOthersExist()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("File1.txt"),
                    this.GetFilePath("File2.txt"),
                },
                false,
                this.GetFilePath("File3.txt"),
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            Assert.Empty(includeFiles);
        }
        
        [Fact]
        public void TestItThrowsExceptionIfFileMissingAndAllowMissingNotSet()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("File1.txt"),
                    this.GetFilePath("File4.txt"),
                },
                false,
                "",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            Assert.Throws<InputFileNotFoundException>(() => rule.GetFilesToInclude(fileFactory));
        }

        [Fact]
        public void TestItReturnsOutputGroup()
        {
            FileListInclusionRule rule = new(
                new List<string>()
                {
                    this.GetFilePath("File1.txt"),
                    this.GetFilePath("File4.txt"),
                },
                false,
                "",
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );
            
            Assert.Equal(new OutputGroup("test"), rule.GetOutputGroup());
        }
    }
}
