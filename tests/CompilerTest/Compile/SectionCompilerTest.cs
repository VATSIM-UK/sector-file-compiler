using System.IO;
using System.Collections.Generic;
using Compiler.Compile;
using Compiler.Output;
using Compiler.Model;
using Compiler.Argument;
using Xunit;
using Moq;

namespace CompilerTest.Compile
{
    public class CompileEngineTest
    {
        private readonly SectorElementCollection elements;

        private readonly Mock<TextWriter> mockOutput;

        private readonly CompilerArguments arguments;

        private readonly SectionCompilerFactory compilerFactory;

        public CompileEngineTest()
        {
            this.arguments = new CompilerArguments();
            this.elements = new SectorElementCollection();
            this.mockOutput = new Mock<TextWriter>();
            this.compilerFactory = new SectionCompilerFactory(this.arguments, this.elements);
            this.arguments.OutFileEse = this.mockOutput.Object;

            this.elements.Add(new CommentLine("test1"), OutputSections.ESE_SIDSSTARS);
            this.elements.Add(new CommentLine("test2"), OutputSections.ESE_AIRSPACE);
        }

        [Fact]
        public void TestItCompilesSections()
        {
            List<SectionCompiler> sections = new List<SectionCompiler>
            {
                this.compilerFactory.Create(OutputSections.ESE_SIDSSTARS),
                this.compilerFactory.Create(OutputSections.ESE_AIRSPACE)
            };

            CompileEngine engine = new CompileEngine(sections);
            engine.Compile();

            this.mockOutput.Verify(foo => foo.Write("[SIDSSTARS]\r\n\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("; test1\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("\r\n"), Times.Exactly(2));
            this.mockOutput.Verify(foo => foo.Write("[AIRSPACE]\r\n\r\n"), Times.Once);
            this.mockOutput.Verify(foo => foo.Write("; test2\r\n"), Times.Once);
        }
    }
}
