using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler
{
    public class ValidateCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private ValidateCompilerArgument validateCompilerArgument;

        public ValidateCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            validateCompilerArgument = new ValidateCompilerArgument();
        }

        [Fact]
        public void TestItSetsCompileOnlyAsMode()
        {
            validateCompilerArgument.Parse(new List<string>(), arguments);
            Assert.Equal(RunMode.VALIDATE, arguments.Mode);
        }
        
        [Fact]
        public void TestItThrowsExceptionOnValues()
        {
            Assert.Throws<ArgumentException>(
                () => validateCompilerArgument.Parse(new List<string>(new[] { "a"}), arguments)
            );
        }
        
        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--validate", validateCompilerArgument.GetSpecifier());
        }
    }
}
