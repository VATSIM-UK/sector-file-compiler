using System.Collections.Generic;
using Compiler.Input;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Input
{
    public class HeaderDataFileTest
    {
        private readonly HeaderDataFile file;

        public HeaderDataFileTest()
        {
            this.file = new HeaderDataFile(
                "_TestData/HeaderDataFileTest/StreamTest.txt",
                new InputFileStreamFactory(),
                new EseSectorDataReader()
            );
        }

        [Fact]
        public void ItSetsFullPath()
        {
            Assert.Equal(
                "_TestData/HeaderDataFileTest/StreamTest.txt",
                this.file.FullPath
            );
        }

        [Fact]
        public void ItGetsParentDirectory()
        {
            Assert.Equal(
                "HeaderDataFileTest",
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
            Assert.Equal(InputDataType.FILE_HEADERS, this.file.DataType);
        }

        [Fact]
        public void TestItIteratesTheInputFile()
        {
            int expectedLine = 1;
            foreach (SectorData dataLine in this.file)
            {
                Assert.Equal(new List<string>(), dataLine.dataSegments);
                Assert.Equal(new Docblock(), dataLine.docblock);

                if (expectedLine % 2 == 0)
                {
                    Assert.Equal(new Comment(""), dataLine.inlineComment);
                } else
                {
                    Assert.Equal(new Comment("Line " + expectedLine), dataLine.inlineComment);
                }
                
                Assert.Equal(expectedLine, this.file.CurrentLineNumber);
                Assert.Equal(new Definition("_TestData/HeaderDataFileTest/StreamTest.txt", expectedLine), dataLine.definition);

                expectedLine++;
            }

            Assert.Equal(9, this.file.CurrentLineNumber);
        }
    }
}
