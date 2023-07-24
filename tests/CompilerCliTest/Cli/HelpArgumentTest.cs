using CompilerCli.Cli;
using System;
using System.Collections.Generic;
using Xunit;

namespace CompilerCliTest.Cli
{
    public class HelpArgumentTest
    {
        private CliArguments arguments;
        private HelpArgument commandLineArgument;

        public HelpArgumentTest() {
            arguments = new CliArguments();
            commandLineArgument = new HelpArgument();
        }

        [Fact]
        public void TestItSetsHelpExitFlag() {
            commandLineArgument.Parse(new List<string>(), arguments);
            Assert.True(arguments.PauseOnFinish);
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues() {
            Assert.Throws<ArgumentException>(
                () => commandLineArgument.Parse(new List<string>(new[] { "a", "b" }), arguments)
            );
        }

        [Fact]
        public void TestItReturnsASpecifier() {
            Assert.Equal("--help", commandLineArgument.GetSpecifier());
        }

        [Fact]
        public void TestItReturnsAHelpMessage() {
            Assert.IsType<string>(HelpArgument.GetHelpMessage());
        }
    }
}
