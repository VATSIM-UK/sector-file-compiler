using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Output;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class RwyOutputFileParserTest
    {
        [Fact]
        public void TestItSetsOutputFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            RwyOutputFileParser parser = new RwyOutputFileParser();

            arguments = parser.Parse(new List<string>(new[] { "test.rwy" }), arguments);
            Assert.Single(arguments.OutputFiles);
            Assert.IsType<RwyOutput>(arguments.OutputFiles[0]);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            RwyOutputFileParser parser = new RwyOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { }), arguments));
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            RwyOutputFileParser parser = new RwyOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new[] { "a", "b" }), arguments));
        }
    }
}
