using System.Collections.Generic;
using Xunit;
using Moq;
using System.IO;
using Compiler.Input;
using Compiler.Output;
using Compiler.Transformer;

namespace CompilerTest.Output
{
    public class SectionTest
    {
        private readonly Mock<TextWriter> mockOutput;
        private readonly Mock<IFileInterface> mockInput1;
        private readonly Mock<IFileInterface> mockInput2;
        private readonly Mock<IFileInterface> mockInput3;

        public SectionTest()
        {
            this.mockInput1 = new Mock<IFileInterface>();
            this.mockInput2 = new Mock<IFileInterface>();
            this.mockInput3 = new Mock<IFileInterface>();
            this.mockInput1.Setup(foo => foo.GetAllLines()).Returns(new List<string>(new string[] { "a" }));
            this.mockInput2.Setup(foo => foo.GetAllLines()).Returns(new List<string>(new string[] { "b" }));
            this.mockInput3.Setup(foo => foo.GetAllLines()).Returns(new List<string>(new string[] { "c;comment" }));
            this.mockOutput = new Mock<TextWriter>();
        }

        [Fact]
        public void TestItWritesWithNoHeader()
        {
            Section section = new Section(
                new List<IFileInterface>(new IFileInterface[]{ mockInput1.Object, mockInput2.Object }),
                new TransformerChain(),
                null
            );

            section.WriteToFile(this.mockOutput.Object);

            this.mockOutput.Verify(foo => foo.Write("a\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("b\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once());
        }

        [Fact]
        public void TestItWritesWithHeader()
        {
            Section section = new Section(
                new List<IFileInterface>(new IFileInterface[] { mockInput1.Object, mockInput2.Object }),
                new TransformerChain(),
                "[FOO]"
            );

            section.WriteToFile(this.mockOutput.Object);

            this.mockOutput.Verify(foo => foo.Write("[FOO]\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("a\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("b\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once());
        }

        [Fact]
        public void TestItAppliesTransformers()
        {
            TransformerChain chain = new TransformerChain();
            chain.AddTransformer(new RemoveAllComments());
            Section section = new Section(
                new List<IFileInterface>(new IFileInterface[] { mockInput1.Object, mockInput2.Object, mockInput3.Object }),
                chain,
                "[FOO]"
            );

            section.WriteToFile(this.mockOutput.Object);

            this.mockOutput.Verify(foo => foo.Write("[FOO]\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("a\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("b\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("c\r\n"), Times.Once());
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once());
        }
    }
}
