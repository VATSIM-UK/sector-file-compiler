using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataFileTest
    {
        private readonly SectorDataFile file;

        public SectorDataFileTest()
        {
            this.file = new SectorDataFile("../../../../../Data/StreamTest.txt");
        }

        [Fact]
        public void ItSetsFullPath()
        {
            Assert.Equal("../../../../../Data/StreamTest.txt", this.file.FullPath);
        }

        [Fact]
        public void CurrentLineStartsAtZero()
        {
            Assert.Equal(0, this.file.CurrentLine);
        }

        [Fact]
        public void TestItIteratesTheInputFile()
        {
            int expectedLine = 1;
            foreach (string line in this.file)
            {
                Assert.Equal("Line " + expectedLine.ToString(), line);
                Assert.Equal(expectedLine++, this.file.CurrentLine);
            }

            Assert.Equal(8, this.file.CurrentLine);
        }
    }
}
