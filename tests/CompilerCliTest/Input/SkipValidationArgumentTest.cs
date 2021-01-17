using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class SkipValidationArgumentTest
    {
        private CompilerArguments arguments;
        private SkipValidationArgument commandLineArgument;

        public SkipValidationArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineArgument = new SkipValidationArgument();
        }
        
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            commandLineArgument.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.False(arguments.ValidateOutput);
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--skip-validation", commandLineArgument.GetSpecifier());
        }
    }
}
