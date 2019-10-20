using Xunit;
using Compiler.Argument;
using Compiler.Input;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class OutputFileParserTest
    {
        [Fact]
        public void TestItSetsOutputFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            OutputFileParser parser = new OutputFileParser();

            arguments = parser.Parse("test.json", arguments);
            Assert.Equal(new InputFile("test.json"), arguments.OutFile);
        }
    }
}
