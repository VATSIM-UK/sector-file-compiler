using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            return $"_TestData/FolderInclusionRule{Path.DirectorySeparatorChar}{filename}";
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
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(3, files.Count);
            Assert.Equal(GetFilePath("File1.txt"), files[0].FullPath);
            Assert.Equal(GetFilePath("File2.txt"), files[1].FullPath);
            Assert.Equal(GetFilePath("File3.txt"), files[2].FullPath);
        }
        
        [Fact]
        public void TestItLoadsFilesBasedOnARegularExpression()
        {
            FolderInclusionRule rule = new (
                "_TestData/FolderInclusionRule",
                false,
                InputDataType.ESE_AGREEMENTS,
                new OutputGroup("test"),
                includePattern: new Regex("File[1|3].txt")
            );

            IEnumerable<AbstractSectorDataFile> includeFiles = rule.GetFilesToInclude(fileFactory);
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(2, files.Count);
            Assert.Equal(GetFilePath("File1.txt"), files[0].FullPath);
            Assert.Equal(GetFilePath("File3.txt"), files[1].FullPath);
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
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(4, files.Count);
            Assert.Equal(GetFilePath("File1.txt"), files[0].FullPath);
            Assert.Equal(GetFilePath("File2.txt"), files[1].FullPath);
            Assert.Equal(GetFilePath("File3.txt"), files[2].FullPath);
            Assert.Equal(GetFilePath($"Level2{Path.DirectorySeparatorChar}File4.txt"), files[3].FullPath);
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
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Equal(2, files.Count);
            Assert.Equal(GetFilePath("File1.txt"), files[0].FullPath);
            Assert.Equal(GetFilePath("File3.txt"), files[1].FullPath);
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
            List<AbstractSectorDataFile> files = includeFiles.ToList();
            Assert.Single(files);
            Assert.Equal(GetFilePath("File2.txt"), files[0].FullPath);
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
