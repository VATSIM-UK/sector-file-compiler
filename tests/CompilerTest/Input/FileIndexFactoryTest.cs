using System.Collections.Generic;
using Compiler.Input;
using Xunit;
using Moq;
using Compiler.Output;
using Compiler.Event;
using Newtonsoft.Json.Linq;

namespace CompilerTest.Input
{
    public class FileIndexFactoryTest
    {
        private readonly JObject configFile;

        private readonly Mock<IEventLogger> events;

        public FileIndexFactoryTest()
        {
            this.events = new Mock<IEventLogger>();
            this.configFile = JObject.Parse(@"{
              ese_positions: {
                subsection_1: [
                  'info1.txt',
                  '../info2.txt',
                ],
                subsection_2: [
                  'info3.txt',
                ]
              },
              ese_airspace: [
                'airspace1.txt',
              ],
            }");
        }

        [Fact]
        public void TestItCreatesFileList()
        {
            FileIndex actual = FileIndexFactory.CreateFileIndex(
                this.configFile,
                this.events.Object
            );

            Assert.True(actual.Files.ContainsKey(OutputSections.ESE_POSITIONS));
            Assert.True(actual.Files.ContainsKey(OutputSections.ESE_AIRSPACE));
            Assert.Equal(new InputFile("info1.txt"), actual.GetFilesForSection(OutputSections.ESE_POSITIONS)[0]);
            Assert.Equal(new InputFile("../info2.txt"), actual.GetFilesForSection(OutputSections.ESE_POSITIONS)[1]);
            Assert.Equal(new InputFile("info3.txt"), actual.GetFilesForSection(OutputSections.ESE_POSITIONS)[2]);
            Assert.Equal(new InputFile("airspace1.txt"), actual.GetFilesForSection(OutputSections.ESE_AIRSPACE)[0]);
        }
    }
}
