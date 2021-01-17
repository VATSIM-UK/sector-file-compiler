using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class ConfigFileArgumentTest
    {
        private CompilerArguments arguments;
        private ConfigFileArgument commandLineArgument;

        public ConfigFileArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineArgument = new ConfigFileArgument();
        }
        
        [Fact]
        public void TestItSetsConfigFile()
        {
            commandLineArgument.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.Single(arguments.ConfigFiles);
            Assert.Equal("test.json", arguments.ConfigFiles[0]);
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
            Assert.Equal("--config-file", commandLineArgument.GetSpecifier());
        }
    }
}
