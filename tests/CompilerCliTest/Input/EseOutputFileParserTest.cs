using Xunit;
using System;
using System.Collections.Generic;
using Compiler.Argument;
using Compiler.Output;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class EseOutputFileParserTest
    {
        [Fact]
        public void TestItSetsOutputFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            EseOutputFileParser parser = new EseOutputFileParser();

            arguments = parser.Parse(new List<string>(new string[] { "test.ese" }), arguments);
            Assert.Single(arguments.OutputFiles);
            Assert.IsType<EseOutput>(arguments.OutputFiles[0]);
        }

        [Fact]
        public void TestItThrowsExceptionOnNoValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            EseOutputFileParser parser = new EseOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { }), arguments));
        }

        [Fact]
        public void TestItThrowsExceptionOnTooManyValues()
        {
            CompilerArguments arguments = new CompilerArguments();
            EseOutputFileParser parser = new EseOutputFileParser();

            Assert.Throws<ArgumentException>(() => parser.Parse(new List<string>(new string[] { "a", "b" }), arguments));
        }
    }
}
