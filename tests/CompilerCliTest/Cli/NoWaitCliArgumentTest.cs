using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Cli;
using CompilerCli.Compiler;

namespace CompilerCliTest.Cli
{
    public class NoWaitCliArgumentTest
    {
        private CliArguments arguments;
        private NoWaitCliArgument commandLineArgument;

        public NoWaitCliArgumentTest()
        {
            arguments = new CliArguments();
            commandLineArgument = new NoWaitCliArgument();
        }
        
        [Fact]
        public void TestItSetsBuildVersion()
        {
            commandLineArgument.Parse(new List<string>(), arguments);
            Assert.False(arguments.PauseOnFinish);
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
            Assert.Equal("--no-wait", commandLineArgument.GetSpecifier());
        }
    }
}
