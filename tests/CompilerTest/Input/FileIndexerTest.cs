using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;
using Xunit;
using Moq;
using Compiler.Output;

namespace CompilerTest.Input
{
    public class FileIndexerTest
    {
        private const string configFileFolder = "foo\\bar";

        private readonly Dictionary<string, List<string>> configFile;

        private readonly Mock<ILoggerInterface> mockLogger;

        private readonly FileIndexer indexer;

        public FileIndexerTest()
        {
            this.mockLogger = new Mock<ILoggerInterface>();
            this.configFile = new Dictionary<string, List<string>>();
            this.configFile["positions"] = new List<string>(new string[] { "file1.txt", "file2.txt" });
            this.indexer = new FileIndexer(FileIndexerTest.configFileFolder, this.configFile, this.mockLogger.Object);
        }

        [Fact]
        public void TestItReturnsEmptyIfConfigFileHasNoFiles()
        {
            Assert.Equal(new List<IFileInterface>(), this.indexer.CreateFileListForSection(OutputSections.ESE_PREAMBLE));
        }

        [Fact]
        public void TestItCreatesFileList()
        {
            List<IFileInterface> expected = new List<IFileInterface>()
            {
                new InputFile("foo\\bar\\file1.txt"),
                new InputFile("foo\\bar\\file2.txt"),
            };

            Assert.Equal(expected, this.indexer.CreateFileListForSection(OutputSections.ESE_POSITIONS));
        }
    }
}
