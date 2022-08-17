using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler
{
    public class CheckConfigCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private CheckConfigCompilerArgument checkConfigCompilerArgument;

        public CheckConfigCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            checkConfigCompilerArgument = new CheckConfigCompilerArgument();
        }

        [Fact]
        public void TestItSetsCompileOnlyAsMode()
        {
            checkConfigCompilerArgument.Parse(new List<string>(), arguments);
            Assert.Equal(RunMode.CHECK_CONFIG, arguments.Mode);
        }
        
        [Fact]
        public void TestItThrowsExceptionOnValues()
        {
            Assert.Throws<ArgumentException>(
                () => checkConfigCompilerArgument.Parse(new List<string>(new[] { "a"}), arguments)
            );
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--check-config", checkConfigCompilerArgument.GetSpecifier());
        }
    }
}
