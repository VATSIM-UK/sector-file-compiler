using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class StripNewlinesParserTest
    {
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            CompilerArguments arguments = new CompilerArguments();
            StripNewlinesParser parser = new StripNewlinesParser();

            arguments = parser.Parse(new List<string>(new string[] {}), arguments);
            Assert.True(arguments.RemoveBlankLines);
        }
    }
}
