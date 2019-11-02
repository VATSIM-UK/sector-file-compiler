using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class SctOutputFileParserTest
    {
        [Fact]
        public void TestItSetsOutputFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            SctOutputFileParser parser = new SctOutputFileParser();

            arguments = parser.Parse(new List<string>(new string[] { "test.sct" }), arguments);
            Assert.NotNull(arguments.OutFileSct);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            SctOutputFileParser parser = new SctOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { }), arguments));
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            SctOutputFileParser parser = new SctOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { "a", "b" }), arguments));
        }
    }
}
