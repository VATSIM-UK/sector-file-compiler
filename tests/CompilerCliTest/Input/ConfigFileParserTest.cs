using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Input;
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
            Assert.Equal(new InputFile("test.json"), arguments.ConfigFiles);
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
