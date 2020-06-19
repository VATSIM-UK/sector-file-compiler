using System.IO;
using Compiler.Compile;
using Compiler.Output;
using Compiler.Model;
using Compiler.Transformer;
using Xunit;
using Moq;

namespace CompilerTest.Compile
{
    public class SectionCompilerTest
    {
        private readonly SectorElementCollection elements;

        private readonly Mock<TextWriter> mockOutput;

        public SectionCompilerTest()
        {
            this.elements = new SectorElementCollection();
            this.mockOutput = new Mock<TextWriter>();

            this.elements.Add(new CommentLine("test"), OutputSections.ESE_SIDSSTARS, Subsections.DEFAULT);
        }

        [Fact]
        public void TestItCompilesASection()
        {
            SectionCompiler compiler = new SectionCompiler(
                OutputSections.ESE_SIDSSTARS,
                this.elements,
                new TransformerChain(),
                this.mockOutput.Object
            );

            compiler.Compile();

            this.mockOutput.Verify(foo => foo.Write("[SIDSSTARS]\r\n\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("; test\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once);
        }

        [Fact]
        public void TestItAppliesTransformers()
        {
            TransformerChain chain = new TransformerChain();
            chain.AddTransformer(new RemoveAllComments());
            SectionCompiler compiler = new SectionCompiler(
                OutputSections.ESE_SIDSSTARS,
                this.elements,
                chain,
                this.mockOutput.Object
            );

            compiler.Compile();

            this.mockOutput.Verify(foo => foo.Write("[SIDSSTARS]\r\n\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write(""), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Once);
        }
    }
}
