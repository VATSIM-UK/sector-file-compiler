using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;

namespace CompilerCliTest.Compiler
{
    public class ConfigFileCompilerArgumentTest
    {
        private CompilerArguments arguments;
        private ConfigFileCompilerArgument commandLineCompilerArgument;

        public ConfigFileCompilerArgumentTest()
        {
            arguments = new CompilerArguments();
            commandLineCompilerArgument = new ConfigFileCompilerArgument();
        }
        
        [Fact]
        public void TestItSetsConfigFile()
        {
            commandLineCompilerArgument.Parse(new List<string>(new[] { "test.json" }), arguments);
            Assert.Single(arguments.ConfigFiles);
            Assert.Equal("test.json", arguments.ConfigFiles[0]);
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
            Assert.Equal("--config-file", commandLineCompilerArgument.GetSpecifier());
        }
    }
}
