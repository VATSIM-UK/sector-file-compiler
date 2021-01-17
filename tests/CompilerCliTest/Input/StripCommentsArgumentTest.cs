using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class StripCommentsArgumentTest
    {
        private CompilerArguments arguments;
        private StripCommentsArgument commandLineArgument;

        public StripCommentsArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineArgument = new StripCommentsArgument();
        }
        
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            commandLineArgument.Parse(new List<string>(new string[] { }), arguments);
            Assert.True(arguments.StripComments);
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--strip-comments", commandLineArgument.GetSpecifier());
        }
    }
}
