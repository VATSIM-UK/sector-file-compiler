using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;

namespace CompilerCliTest.Compiler
{
    public class StripCommentsCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private StripCommentsCompilerArgument commandLineCompilerArgument;

        public StripCommentsCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineCompilerArgument = new StripCommentsCompilerArgument();
        }
        
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            commandLineCompilerArgument.Parse(new List<string>(new string[] { }), arguments);
            Assert.True(arguments.StripComments);
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--strip-comments", commandLineCompilerArgument.GetSpecifier());
        }
    }
}
