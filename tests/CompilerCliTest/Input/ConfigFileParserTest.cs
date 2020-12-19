using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class ConfigFileParserTest
    {
        [Fact]
        public void TestItSetsConfigFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            ConfigFileParser parser = new ConfigFileParser();

            arguments = parser.Parse(new List<string>(new string[] { "test.json" }), arguments);
            Assert.Single(arguments.ConfigFiles[0]);
            Assert.Equal("test.json", arguments.ConfigFiles[0]);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            ConfigFileParser parser = new ConfigFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] {}), arguments));
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            ConfigFileParser parser = new ConfigFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { "a", "b" }), arguments));
        }
    }
}
