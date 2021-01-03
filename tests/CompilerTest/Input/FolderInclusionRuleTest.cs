using System.Collections.Generic;
using System.Linq;
using Compiler.Input;
using Compiler.Output;
using Compiler.Exception;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Input
{
    public class FolderInclusionRuleTest
    {
        private readonly SectorDataFileFactory fileFactory = SectorDataFileFactoryFactory.Make();
        
        private string GetFilePath(string filename)
        {
            return $"_TestData/FolderInclusionRule\\{filename}";
        }
        
        [Fact]
        public void TestItThrowsExceptionOnMissingDirectory()
        {
            FolderInclusionRule rule = new (
                "_TestData/NotFolderInclusionRule",
                false,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            Assert.Throws<InputDirectoryNotFoundException>(() => rule.GetFilesToInclude(fileFactory));
        }
        
        [Fact]
        public void TestItLoadsFiles()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                false,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            Assert.Equal(3, includeFiles.Count());

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File1.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File2.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File3.txt"), enumerator.Current.FullPath);
        }
        
        [Fact]
        public void TestItLoadsFilesRecursively()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                true,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            Assert.Equal(4, includeFiles.Count());

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File1.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File2.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File3.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("Level2\\File4.txt"), enumerator.Current.FullPath);
        }
        
        [Fact]
        public void TestItHasAnExcludeList()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                false,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test"),
                true,
                new List<string>() {"File2.txt"}
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            Assert.Equal(2, includeFiles.Count());

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File1.txt"), enumerator.Current.FullPath);
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File3.txt"), enumerator.Current.FullPath);
        }
        
        [Fact]
        public void TestItHasAnIncludeList()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                false,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test"),
                false,
                new List<string>() {"File2.txt"}
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            Assert.Single(includeFiles);

            using IEnumerator<AbstractSectorDataFile> enumerator = includeFiles.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(this.GetFilePath("File2.txt"), enumerator.Current.FullPath);
        }
        
        [Fact]
        public void TestItReturnsOutputGroup()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                true,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test")
            );

            Assert.Equal(new OutputGroup("test"), rule.GetOutputGroup());
        }
    }
}
