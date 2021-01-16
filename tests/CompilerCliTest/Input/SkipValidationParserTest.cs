using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class SkipValidationParserTest
    {
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            CompilerArguments arguments = new CompilerArguments();
            SkipValidationParser parser = new SkipValidationParser();

            arguments = parser.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.False(arguments.ValidateOutput);
        }
    }
}
