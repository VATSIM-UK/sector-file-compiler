using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler
{
    public class LintCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private LintCompilerArgument lintCompilerArgument;

        public LintCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            lintCompilerArgument = new LintCompilerArgument();
        }

        [Fact]
        public void TestItSetsCompileOnlyAsMode()
        {
            lintCompilerArgument.Parse(new List<string>(), arguments);
            Assert.Equal(RunMode.LINT, arguments.Mode);
        }
        
        [Fact]
        public void TestItThrowsExceptionOnValues()
        {
            Assert.Throws<ArgumentException>(
                () => lintCompilerArgument.Parse(new List<string>(new[] { "a"}), arguments)
            );
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--lint", lintCompilerArgument.GetSpecifier());
        }
    }
}
