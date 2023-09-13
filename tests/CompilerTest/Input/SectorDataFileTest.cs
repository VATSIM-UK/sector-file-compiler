using System;
using System.Collections.Generic;
using Compiler.Input;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataFileTest
    {
        private readonly SectorDataFile file;
        private readonly SectorDataFile arcGenFile;

        public SectorDataFileTest()
        {
            file = new SectorDataFile(
                "_TestData/SectorDataFile/StreamTest.txt",
                new InputFileStreamFactory(),
                InputDataType.ESE_AGREEMENTS,
                new EseSectorDataReader()
            );

            arcGenFile = new SectorDataFile(
                "_TestData/SectorDataFile/ArcGenTest.txt",
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
        public void TestItIteratesArcGen() {
            string[] lines = { "Test Region N051.31.15.000 W000.07.30.000 N051.31.14.715 W000.07.19.500", "Test Region N051.31.14.715 W000.07.19.500 N051.31.13.861 W000.07.09.079", "Test Region N051.31.13.861 W000.07.09.079 N051.31.12.444 W000.06.58.818", "Test Region N051.31.12.444 W000.06.58.818 N051.31.10.477 W000.06.48.794", "Test Region N051.31.10.477 W000.06.48.794 N051.31.07.973 W000.06.39.083", "Test Region N051.31.07.973 W000.06.39.083 N051.31.04.952 W000.06.29.760", "Test Region N051.31.04.952 W000.06.29.760 N051.31.01.436 W000.06.20.896", "Test Region N051.31.01.436 W000.06.20.896 N051.30.57.453 W000.06.12.558", "Test Region N051.30.57.453 W000.06.12.558 N051.30.53.033 W000.06.04.808", "Test Region N051.30.53.033 W000.06.04.808 N051.30.48.209 W000.05.57.708", "Test Region N051.30.48.209 W000.05.57.708 N051.30.43.018 W000.05.51.309", "Test Region N051.30.43.018 W000.05.51.309 N051.30.37.500 W000.05.45.662", "Test Region N051.30.37.500 W000.05.45.662 N051.30.31.696 W000.05.40.809", "Test Region N051.30.31.696 W000.05.40.809 N051.30.25.652 W000.05.36.787", "Test Region N051.30.25.652 W000.05.36.787 N051.30.19.411 W000.05.33.626", "Test Region N051.30.19.411 W000.05.33.626 N051.30.13.024 W000.05.31.351", "Test Region N051.30.13.024 W000.05.31.351 N051.30.06.537 W000.05.29.979", "Test Region N051.30.06.537 W000.05.29.979 N051.30.00.000 W000.05.29.521", "Test Region N051.30.00.000 W000.05.29.521 N051.29.53.463 W000.05.29.979", "Test Region N051.29.53.463 W000.05.29.979 N051.29.46.976 W000.05.31.351", "Test Region N051.29.46.976 W000.05.31.351 N051.29.40.589 W000.05.33.626", "Test Region N051.29.40.589 W000.05.33.626 N051.29.34.348 W000.05.36.787", "Test Region N051.29.34.348 W000.05.36.787 N051.29.28.304 W000.05.40.809", "Test Region N051.29.28.304 W000.05.40.809 N051.29.22.500 W000.05.45.662", "Test Region N051.29.22.500 W000.05.45.662 N051.29.16.982 W000.05.51.309", "Test Region N051.29.16.982 W000.05.51.309 N051.29.11.791 W000.05.57.708", "Test Region N051.29.11.791 W000.05.57.708 N051.29.06.967 W000.06.04.808", "Test Region N051.29.06.967 W000.06.04.808 N051.29.02.547 W000.06.12.558", "Test Region N051.29.02.547 W000.06.12.558 N051.28.58.564 W000.06.20.896", "Test Region N051.28.58.564 W000.06.20.896 N051.28.55.048 W000.06.29.760", "Test Region N051.28.55.048 W000.06.29.760 N051.28.52.027 W000.06.39.083", "Test Region N051.28.52.027 W000.06.39.083 N051.28.49.523 W000.06.48.794", "Test Region N051.28.49.523 W000.06.48.794 N051.28.47.556 W000.06.58.818", "Test Region N051.28.47.556 W000.06.58.818 N051.28.46.139 W000.07.09.079", "Test Region N051.28.46.139 W000.07.09.079 N051.28.45.285 W000.07.19.500", "Test Region N051.28.45.285 W000.07.19.500 N051.28.44.999 W000.07.30.000", "Test Region N051.28.44.999 W000.07.30.000 N051.28.45.285 W000.07.40.500", "Test Region N051.28.45.285 W000.07.40.500 N051.28.46.139 W000.07.50.921", "Test Region N051.28.46.139 W000.07.50.921 N051.28.47.556 W000.08.01.182", "Test Region N051.28.47.556 W000.08.01.182 N051.28.49.523 W000.08.11.206", "Test Region N051.28.49.523 W000.08.11.206 N051.28.52.027 W000.08.20.917", "Test Region N051.28.52.027 W000.08.20.917 N051.28.55.048 W000.08.30.240", "Test Region N051.28.55.048 W000.08.30.240 N051.28.58.564 W000.08.39.104", "Test Region N051.28.58.564 W000.08.39.104 N051.29.02.547 W000.08.47.442", "Test Region N051.29.02.547 W000.08.47.442 N051.29.06.967 W000.08.55.192", "Test Region N051.29.06.967 W000.08.55.192 N051.29.11.791 W000.09.02.292", "Test Region N051.29.11.791 W000.09.02.292 N051.29.16.982 W000.09.08.691", "Test Region N051.29.16.982 W000.09.08.691 N051.29.22.500 W000.09.14.338", "Test Region N051.29.22.500 W000.09.14.338 N051.29.28.304 W000.09.19.191", "Test Region N051.29.28.304 W000.09.19.191 N051.29.34.348 W000.09.23.213", "Test Region N051.29.34.348 W000.09.23.213 N051.29.40.589 W000.09.26.374", "Test Region N051.29.40.589 W000.09.26.374 N051.29.46.976 W000.09.28.649", "Test Region N051.29.46.976 W000.09.28.649 N051.29.53.463 W000.09.30.021", "Test Region N051.29.53.463 W000.09.30.021 N051.30.00.000 W000.09.30.479", "Test Region N051.30.00.000 W000.09.30.479 N051.30.06.537 W000.09.30.021", "Test Region N051.30.06.537 W000.09.30.021 N051.30.13.024 W000.09.28.649", "Test Region N051.30.13.024 W000.09.28.649 N051.30.19.411 W000.09.26.374", "Test Region N051.30.19.411 W000.09.26.374 N051.30.25.652 W000.09.23.213", "Test Region N051.30.25.652 W000.09.23.213 N051.30.31.696 W000.09.19.191", "Test Region N051.30.31.696 W000.09.19.191 N051.30.37.500 W000.09.14.338", "Test Region N051.30.37.500 W000.09.14.338 N051.30.43.018 W000.09.08.691", "Test Region N051.30.43.018 W000.09.08.691 N051.30.48.209 W000.09.02.292", "Test Region N051.30.48.209 W000.09.02.292 N051.30.53.033 W000.08.55.192", "Test Region N051.30.53.033 W000.08.55.192 N051.30.57.453 W000.08.47.442", "Test Region N051.30.57.453 W000.08.47.442 N051.31.01.436 W000.08.39.104", "Test Region N051.31.01.436 W000.08.39.104 N051.31.04.952 W000.08.30.240", "Test Region N051.31.04.952 W000.08.30.240 N051.31.07.973 W000.08.20.917", "Test Region N051.31.07.973 W000.08.20.917 N051.31.10.477 W000.08.11.206", "Test Region N051.31.10.477 W000.08.11.206 N051.31.12.444 W000.08.01.182", "Test Region N051.31.12.444 W000.08.01.182 N051.31.13.861 W000.07.50.921", "Test Region N051.31.13.861 W000.07.50.921 N051.31.14.715 W000.07.40.500", "Test Region N051.31.14.715 W000.07.40.500 N051.31.15.000 W000.07.30.000" };
            string[] lines2 = { "Test Region N051.29.17.000 W000.06.34.000 N051.29.16.696 W000.06.34.039", "Test Region N051.29.16.696 W000.06.34.039 N051.29.13.869 W000.06.33.999", "Test Region N051.29.13.869 W000.06.33.999 N051.29.11.051 W000.06.34.355", "Test Region N051.29.11.051 W000.06.34.355 N051.29.08.264 W000.06.35.104", "Test Region N051.29.08.264 W000.06.35.104 N051.29.05.527 W000.06.36.241", "Test Region N051.29.05.527 W000.06.36.241 N051.29.02.863 W000.06.37.756", "Test Region N051.29.02.863 W000.06.37.756 N051.29.00.291 W000.06.39.639", "Test Region N051.29.00.291 W000.06.39.639 N051.28.57.831 W000.06.41.874", "Test Region N051.28.57.831 W000.06.41.874 N051.28.55.501 W000.06.44.445", "Test Region N051.28.55.501 W000.06.44.445 N051.28.52.000 W000.06.49.000" };
            int i = 0;
            foreach (SectorData dataLine in arcGenFile) {
                if (i < lines.Length) {
                    Assert.Equal(lines[i], dataLine.rawData);
                } else {
                    Assert.Equal(lines2[i - lines.Length], dataLine.rawData);
                }
                i += 1;
            }
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
