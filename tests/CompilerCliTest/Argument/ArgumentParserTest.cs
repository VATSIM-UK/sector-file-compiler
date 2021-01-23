using System;
using Compiler.Argument;
using CompilerCli.Argument;
using CompilerCli.Cli;
using Xunit;

namespace CompilerCliTest.Argument
{
    public class ArgumentParserTest
    {
        private ArgumentParser parser;
        private CompilerArguments compilerArguments;
        private CliArguments cliArguments;

        public ArgumentParserTest()
        {
            parser = ArgumentParserFactory.Make();
            compilerArguments = CompilerArgumentsFactory.Make();
            cliArguments = CliArgumentsFactory.Make();
        }
        
        [Fact]
        public void TestItReturnsEmptyArgumentsIfNoneProvided()
        {
            CompilerArguments expectedCompiler = new();
            parser.CreateFromCommandLine(compilerArguments, cliArguments, new string[] { });
            Assert.True(expectedCompiler.Equals(compilerArguments));
            Assert.True(cliArguments.PauseOnFinish);
        }

        [Fact]
        public void TestItSetsCompilerArgumentsFromCommandLine()
        {
            parser.CreateFromCommandLine(compilerArguments, cliArguments, new[] { "--config-file", "test.json" });
            Assert.Single(compilerArguments.ConfigFiles);
            Assert.Equal("test.json", compilerArguments.ConfigFiles[0]);
        }
        
        [Fact]
        public void TestItSetsCliArgumentsFromCommandLine()
        {
            parser.CreateFromCommandLine(compilerArguments, cliArguments, new[] { "--no-wait"});
            Assert.False(cliArguments.PauseOnFinish);
        }

        [Fact]
        public void TestItPassesAllValuesToParser()
        {
            parser.CreateFromCommandLine(
                compilerArguments,
                cliArguments,
                new[] { "--config-file", "test.json", "--config-file", "test2.json" }
            );
            Assert.Equal(2, compilerArguments.ConfigFiles.Count);
            Assert.Equal("test.json", compilerArguments.ConfigFiles[0]);
            Assert.Equal("test2.json", compilerArguments.ConfigFiles[1]);
        }

        [Fact]
        public void TestItAllowsFlagsWithNoValues()
        {
            parser.CreateFromCommandLine(
                compilerArguments,
                cliArguments,
                new[] { "--test-arg", "--config-file", "test1.json" }
            );
            Assert.Single(compilerArguments.ConfigFiles);
            Assert.Equal("test1.json", compilerArguments.ConfigFiles[0]);
        }

        [Fact]
        public void TestItThrowsAnExceptionOnUnknownFlags()
        {
            Assert.Throws<ArgumentException>(
                () => parser.CreateFromCommandLine(
                    compilerArguments,
                    cliArguments,
                    new[] { "--whats-this", "test.json" }
                )
            );
        }
    }
}
