using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;

namespace CompilerCliTest.Compiler
{
    public class BuildVersionCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private BuildVersionCompilerArgument commandLineCompilerArgument;

        public BuildVersionCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineCompilerArgument = new BuildVersionCompilerArgument();
        }
        
        [Fact]
        public void TestItSetsBuildVersion()
        {
            commandLineCompilerArgument.Parse(new List<string>(new[] { "VERSION" }), arguments);
            Assert.Equal("VERSION", arguments.BuildVersion);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            Assert.Throws<ArgumentException>(
                () => commandLineCompilerArgument.Parse(new List<string>(new string[] {}), arguments)
            );
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            Assert.Throws<ArgumentException>(
                () => commandLineCompilerArgument.Parse(new List<string>(new[] { "a", "b" }), arguments)
            );
        }

        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--build-version", commandLineCompilerArgument.GetSpecifier());
        }
    }
}
