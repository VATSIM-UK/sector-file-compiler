using System.IO;
using Xunit;
using Moq;
using Compiler.Event;
using CompilerCli.Output;

namespace CompilerCliTest.Output
{
    public class ConsoleOutputTest
    {
        private readonly Mock<TextWriter> mockWriter;
        private readonly CompilationFinishedEvent cfe;
        private readonly ConsoleOutput output;

        public ConsoleOutputTest()
        {
            this.mockWriter = new Mock<TextWriter>();
            this.cfe = new CompilationFinishedEvent(false);
            this.output = new ConsoleOutput(this.mockWriter.Object);
        }

        [Fact]
        public void TestAllEventsAreRedirectedToOutput()
        {
            this.output.NewEvent(this.cfe);
            this.mockWriter.Verify(foo => foo.WriteLine("Compilation failed"), Times.Once);
        }
    }
}
