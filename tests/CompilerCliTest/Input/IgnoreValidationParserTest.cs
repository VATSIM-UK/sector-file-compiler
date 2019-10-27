using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class IgnoreValidationParserTest
    {
        [Fact]
        public void TestItSetsConfigFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            IgnoreValidationParser parser = new IgnoreValidationParser();

            arguments = parser.Parse(new List<string>(new string[] { "test.json" }), arguments);
            Assert.False(arguments.ValidateOutput);
        }
    }
}
