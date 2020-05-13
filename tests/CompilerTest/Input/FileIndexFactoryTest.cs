using System.Collections.Generic;
using Compiler.Input;
using Xunit;
using Moq;
using Compiler.Output;
using Compiler.Event;

namespace CompilerTest.Input
{
    public class FileIndexFactoryTest
    {
        private readonly Dictionary<string, List<string>> configFile;

        private readonly Mock<IEventLogger> events;

        public FileIndexFactoryTest()
        {
            this.events = new Mock<IEventLogger>();
            this.configFile = new Dictionary<string, List<string>>
            {
                ["ese_positions"] = new List<string>(new string[] { "file1.txt", "file2.txt" })
            };
        }

        [Fact]
        public void TestItCreatesFileList()
        {
            FileIndex actual = FileIndexFactory.CreateFileIndex(
                this.configFile,
                this.events.Object
            );

            Assert.True(actual.Files.ContainsKey(OutputSections.ESE_POSITIONS));
            Assert.Equal(new InputFile("file1.txt"), actual.GetFilesForSection(OutputSections.ESE_POSITIONS)[0]);
            Assert.Equal(new InputFile("file2.txt"), actual.GetFilesForSection(OutputSections.ESE_POSITIONS)[1]);
        }
    }
}
