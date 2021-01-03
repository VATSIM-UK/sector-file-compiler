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
            Assert.Equal(2, includeFiles.Count());

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File1.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File2.txt"), enumerator.Current.FullPath);
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
            Assert.Single(includeFiles);

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File1.txt"), enumerator.Current.FullPath);
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
