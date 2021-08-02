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
            file = new SectorDataFile(
                "_TestData/SectorDataFile/StreamTest.txt",
                new InputFileStreamFactory(),
                InputDataType.ESE_AGREEMENTS,
                new EseSectorDataReader()
            );
        }

        [Fact]
        public void ItSetsFullPath()
        {
            Assert.Equal(
                "_TestData/SectorDataFile/StreamTest.txt",
                file.FullPath
            );
        }

        [Fact]
        public void ItGetsParentDirectory()
        {
            Assert.Equal(
                "SectorDataFile",
                file.GetParentDirectoryName()
            );
        }

        [Fact]
        public void ItGetsFileName()
        {
            Assert.Equal(
                "StreamTest",
                file.GetFileName()
            );
        }

        [Fact]
        public void CurrentLineNumberStartsAtZero()
        {
            Assert.Equal(0, file.CurrentLineNumber);
        }

        [Fact]
        public void CurrentLineStartsAtEmpty()
        {
            Assert.Equal("", file.CurrentLine);
        }

        [Fact]
        public void ItHasADataType()
        {
            Assert.Equal(InputDataType.ESE_AGREEMENTS, file.DataType);
        }

        [Fact]
        public void TestItIteratesTheInputFile()
        {
            int expectedLine = 3;
            foreach (SectorData dataLine in file)
            {
                Assert.Equal(new List<string> { "Line", expectedLine.ToString() }, dataLine.dataSegments);

                Docblock expectedDocblock = new();
                expectedDocblock.AddLine(new Comment("Docblock " + (expectedLine - 2)));
                expectedDocblock.AddLine(new Comment("Docblock " + (expectedLine - 1)));
                Assert.Equal(expectedDocblock, dataLine.docblock);
                Assert.Equal(new Comment("Inline " + expectedLine), dataLine.inlineComment);
                Assert.Equal(expectedLine, file.CurrentLineNumber);
                Assert.Equal(new Definition("_TestData/SectorDataFile/StreamTest.txt", expectedLine), dataLine.definition);

                expectedLine += 4;
            }

            Assert.Equal(33, file.CurrentLineNumber);
        }
        
        [Fact]
        public void ItsEqualIfPathTheSame()
        {
            var file2 = new SectorDataFile(
                "_TestData/SectorDataFile/StreamTest.txt",
                new InputFileStreamFactory(),
                InputDataType.ESE_VRPS,
                new EseSectorDataReader()
            );
            Assert.True(file.Equals(file2));
        }
        
        [Fact]
        public void ItsNotEqualIfPathDifferent()
        {
            var file2 = new SectorDataFile(
                "_TestData/SectorDataFile/StreamTest2.txt",
                new InputFileStreamFactory(),
                InputDataType.ESE_AGREEMENTS,
                new EseSectorDataReader()
            );
            Assert.False(file.Equals(file2));
        }
        
        [Fact]
        public void ItsNotEqualIfNotSameType()
        {
            Assert.False(file.Equals(new object()));
        }
    }
}
