using System.Collections.Generic;
using Compiler.Input;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataFileTest
    {
        private readonly SectorDataFile file;

        public SectorDataFileTest()
        {
            this.file = new SectorDataFile(
                "_TestData/StreamTest.txt",
                new InputFileStreamFactory(),
                InputDataType.ESE_AGREEMENTS,
                new EseSectorDataReader()
            );
        }

        [Fact]
        public void ItSetsFullPath()
        {
            Assert.Equal(
                "_TestData/StreamTest.txt",
                this.file.FullPath
            );
        }

        [Fact]
        public void ItGetsParentDirectory()
        {
            Assert.Equal(
                "_TestData",
                this.file.GetParentDirectoryName()
            );
        }

        [Fact]
        public void ItGetsFileName()
        {
            Assert.Equal(
                "StreamTest",
                this.file.GetFileName()
            );
        }

        [Fact]
        public void CurrentLineNumberStartsAtZero()
        {
            Assert.Equal(0, this.file.CurrentLineNumber);
        }

        [Fact]
        public void CurrentLineStartsAtEmpty()
        {
            Assert.Equal("", this.file.CurrentLine);
        }

        [Fact]
        public void ItHasADataType()
        {
            Assert.Equal(InputDataType.ESE_AGREEMENTS, this.file.DataType);
        }

        [Fact]
        public void TestItIteratesTheInputFile()
        {
            int expectedLine = 3;
            foreach (SectorData dataLine in this.file)
            {
                Assert.Equal(new List<string> { "Line", expectedLine.ToString() }, dataLine.dataSegments);

                Docblock expectedDocblock = new Docblock();
                expectedDocblock.AddLine(new Comment("Docblock " + (expectedLine - 2)));
                expectedDocblock.AddLine(new Comment("Docblock " + (expectedLine - 1)));
                Assert.Equal(expectedDocblock, dataLine.docblock);
                Assert.Equal(new Comment("Inline " + expectedLine), dataLine.inlineComment);
                Assert.Equal(expectedLine, this.file.CurrentLineNumber);
                Assert.Equal(new Definition("_TestData/StreamTest.txt", expectedLine), dataLine.definition);

                expectedLine += 4;
            }

            Assert.Equal(33, this.file.CurrentLineNumber);
        }
    }
}
