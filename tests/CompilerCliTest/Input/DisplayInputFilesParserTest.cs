using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class DisplayInputFilesParserTest
    {
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            CompilerArguments arguments = new CompilerArguments();
            DisplayInputFilesParser parser = new DisplayInputFilesParser();

            arguments = parser.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.True(arguments.DisplayInputFiles);
        }
    }
}
