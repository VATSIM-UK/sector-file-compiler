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
        public void TestItSetsArgumentsFromCommandLine()
        {
            CompilerArguments expected = new CompilerArguments();
            expected.ConfigFiles.Add("test.json");

            CompilerArguments actual = ArgumentParser.CreateFromCommandLine(new string[] { "--config-file", "test.json" });
            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestItPassesAllValuesToParser()
        {
            CompilerArguments expected = new CompilerArguments();
            expected.ConfigFiles.Add("test1.json");
            expected.ConfigFiles.Add("test2.json");

            CompilerArguments actual = ArgumentParser.CreateFromCommandLine(new string[] { "--test-arg", "val1", "val2", "--config-file", "test1.json", "--config-file", "test2.json" });
            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestItAllowsFlagsWithNoValues()
        {
            CompilerArguments expected = new CompilerArguments();
            expected.ConfigFiles.Add("test.json");

            CompilerArguments actual = ArgumentParser.CreateFromCommandLine(new string[] { "--test-arg", "--config-file", "test.json" });
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
