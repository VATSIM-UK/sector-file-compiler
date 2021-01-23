using System.IO;
using Compiler.Argument;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Moq;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputWriterTest
    {
        private OutputWriter writer;
        private Mock<TextWriter> outputMock;

        public OutputWriterTest()
        {
            var arguments = CompilerArgumentsFactory.Make();
            arguments.StripComments = true;
            outputMock = new Mock<TextWriter>();
            writer = OutputWriterFactory.Make(
                arguments,
                "file.txt",
                new MockOutputStreamFactory(outputMock.Object)
            );
        }

        [Fact]
        public void TestItWritesOutputToFile()
        {
            writer.WriteLine("foo bar baz");
            outputMock.Verify(foo => foo.WriteLine("foo bar baz"), Times.Once);
        }
        
        [Fact]
        public void TestItTransformsLines()
        {
            writer.WriteLine("foo bar baz ; comment");
            outputMock.Verify(foo => foo.WriteLine("foo bar baz"), Times.Once);
        }
        
        [Fact]
        public void TestItDropsLinesRejectedByTransformer()
        {
            writer.WriteLine("; comment");
            outputMock.Verify(foo => foo.WriteLine(), Times.Never);
        }
    }
}