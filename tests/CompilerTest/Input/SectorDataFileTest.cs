using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataFileTest
    {
        private readonly SectorDataFile file;

        public SectorDataFileTest()
        {
            this.file = new SectorDataFile(
                "_TestData/StreamTest.txt"
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
        public void TestItIteratesTheInputFile()
        {
            int expectedLine = 1;
            foreach (string line in this.file)
            {
                Assert.Equal("Line " + expectedLine.ToString(), line);
                Assert.Equal("Line " + expectedLine.ToString(), this.file.CurrentLine);
                Assert.Equal(expectedLine++, this.file.CurrentLineNumber);
            }

            Assert.Equal(8, this.file.CurrentLineNumber);
        }
    }
}
