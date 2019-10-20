using Xunit;
using System;
using Compiler.Argument;
using CompilerCli.Input;
using Compiler.Output;

namespace CompilerCliTest.Input
{
    public class VerbosityParserTest
    {
        [Fact]
        public void TestItSetsDebugVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            arguments = parser.Parse("debug", arguments);
            Assert.Equal(OutputVerbosity.Debug, arguments.Verbosity);
        }

        [Fact]
        public void TestItSetsInfoVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            arguments = parser.Parse("info", arguments);
            Assert.Equal(OutputVerbosity.Info, arguments.Verbosity);
        }

        [Fact]
        public void TestItSetsWarningVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            arguments = parser.Parse("warning", arguments);
            Assert.Equal(OutputVerbosity.Warning, arguments.Verbosity);
        }

        [Fact]
        public void TestItSetsErrorVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            arguments = parser.Parse("error", arguments);
            Assert.Equal(OutputVerbosity.Error, arguments.Verbosity);
        }

        [Fact]
        public void TestItSetsNullVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            arguments = parser.Parse("quiet", arguments);
            Assert.Equal(OutputVerbosity.Null, arguments.Verbosity);
        }

        [Fact]
        public void TestItThrowsExceptionOnUnknownVerbosity()
        {
            CompilerArguments arguments = new CompilerArguments();
            VerbosityParser parser = new VerbosityParser();

            Assert.Throws<ArgumentException>(() => parser.Parse("lolol", arguments));
        }
    }
}
