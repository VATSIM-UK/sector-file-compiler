using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class ContiguousRouteParserTest
    {
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            CompilerArguments arguments = new CompilerArguments();
            ContiguousRouteParser parser = new ContiguousRouteParser();

            arguments = parser.Parse(new List<string>(new string[] {}), arguments);
            Assert.True(arguments.EnforceContiguousRouteSegments);
        }
    }
}
