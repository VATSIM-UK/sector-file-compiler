using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class BuildVersionParserTest
    {
        [Fact]
        public void TestItSetsConfigFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            BuildVersionParser parser = new BuildVersionParser();

            arguments = parser.Parse(new List<string>(new string[] { "testbuild" }), arguments);
            Assert.Equal("testbuild", arguments.BuildVersion);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            BuildVersionParser parser = new BuildVersionParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] {}), arguments));
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            BuildVersionParser parser = new BuildVersionParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { "a", "b" }), arguments));
        }
    }
}
