using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class BuildVersionArgumentTest
    {
        private CompilerArguments arguments;
        private BuildVersionArgument commandLineArgument;

        public BuildVersionArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineArgument = new BuildVersionArgument();
        }
        
        [Fact]
        public void TestItSetsBuildVersion()
        {
            commandLineArgument.Parse(new List<string>(new[] { "VERSION" }), arguments);
            Assert.Equal("VERSION", arguments.BuildVersion);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            Assert.Throws<ArgumentException>(
                () => commandLineArgument.Parse(new List<string>(new string[] {}), arguments)
            );
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            Assert.Throws<ArgumentException>(
                () => commandLineArgument.Parse(new List<string>(new[] { "a", "b" }), arguments)
            );
        }

        [Fact]
        public void TestItReturnsASpecifier()
        {
            Assert.Equal("--build-version", commandLineArgument.GetSpecifier());
        }
    }
}
