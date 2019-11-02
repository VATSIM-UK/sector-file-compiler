using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class StripCommentsParserTest
    {
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            CompilerArguments arguments = new CompilerArguments();
            StripCommentsParser parser = new StripCommentsParser();

            arguments = parser.Parse(new List<string>(new string[] { }), arguments);
            Assert.True(arguments.StripComments);
        }
    }
}
