using Xunit;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;

namespace CompilerCliTest.Compiler
{
    public class SkipValidationCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private SkipValidationCompilerArgument commandLineCompilerArgument;

        public SkipValidationCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineCompilerArgument = new SkipValidationCompilerArgument();
        }
        
        [Fact]
        public void TestItSetsCompilerArgument()
        {
            commandLineCompilerArgument.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.False(arguments.ValidateOutput);
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--skip-validation", commandLineCompilerArgument.GetSpecifier());
        }
    }
}
