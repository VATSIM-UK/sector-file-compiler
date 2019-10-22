using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.IO;
using Compiler.Input;
using Compiler.Output;

namespace CompilerTest.Output
{
    public class SectionTest
    {
        private readonly Mock<TextWriter> mockOutput;

        private readonly Mock<IFileInterface> mockInput1;
        private readonly Mock<IFileInterface> mockInput2;

        public SectionTest()
        {
            this.mockInput1 = new Mock<IFileInterface>();
            this.mockInput2 = new Mock<IFileInterface>();
            this.mockInput1.Setup(foo => foo.Contents()).Returns("a");
            this.mockInput2.Setup(foo => foo.Contents()).Returns("b");
            this.mockOutput = new Mock<TextWriter>();
        }

        [Fact]
        public void TestItWritesWithNoHeader()
        {
            Section section = new Section(
                new List<IFileInterface>(new IFileInterface[]{ mockInput1.Object, mockInput2.Object }),
                null
            );

            section.WriteToFile(this.mockOutput.Object);

            this.mockOutput.Verify(foo => foo.Write("a"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("b"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once());
        }

        [Fact]
        public void TestItWritesWithHeader()
        {
            Section section = new Section(
                new List<IFileInterface>(new IFileInterface[] { mockInput1.Object, mockInput2.Object }),
                "[FOO]"
            );

            section.WriteToFile(this.mockOutput.Object);

            this.mockOutput.Verify(foo => foo.Write("[FOO]\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("a"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("b"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once());
        }
    }
}
