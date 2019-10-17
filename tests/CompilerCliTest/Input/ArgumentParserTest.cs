using System;
using Compiler.Argument;
using CompilerCli.Input;
using Xunit;

namespace CompilerCliTest.Input
{
    public class ArgumentParserTest
    {
        [Fact]
        public void TestItReturnsEmptyArgumentsIfNoneProvided()
        {
            CompilerArguments expected = new CompilerArguments();
            Assert.True(expected.Equals(ArgumentParser.CreateFromCommandLine(new string[] { })));
        }

        [Fact]
        public void TestItReturnsEmptyArgumentsIfOneProvided()
        {
            CompilerArguments expected = new CompilerArguments();
            Assert.True(expected.Equals(ArgumentParser.CreateFromCommandLine(new string[] { "test" })));
        }

        [Fact]
        public void TestItSetsArgumentsFromCommandLine()
        {
            CompilerArguments expected = new CompilerArguments();
            expected.ConfigFilePath = "test.json";

            CompilerArguments actual = ArgumentParser.CreateFromCommandLine(new string[] { "--config-file", "test.json" });
            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestItIgnoresExtraArgument()
        {
            CompilerArguments expected = new CompilerArguments();
            expected.ConfigFilePath = "test.json";

            CompilerArguments actual = ArgumentParser.CreateFromCommandLine(new string[] { "--config-file", "test.json", "lol" });
            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestItThrowsAnExceptionOnUnknownFlags()
        {
            Assert.Throws<ArgumentException>(
                () => ArgumentParser.CreateFromCommandLine(new string[] { "--whats-this", "test.json" })
            );
        }
    }
}
