using System;
using CompilerCli.Argument;
using CompilerCli.Cli;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Argument
{
    public class ArgumentParserFactoryTest
    {
        [Theory]
        [InlineData(typeof(DefaultCompilerArgument))]
        [InlineData(typeof(SkipValidationCompilerArgument))]
        [InlineData(typeof(StripCommentsCompilerArgument))]
        [InlineData(typeof(ConfigFileCompilerArgument))]
        [InlineData(typeof(BuildVersionCompilerArgument))]
        [InlineData(typeof(CheckConfigCompilerArgument))]
        [InlineData(typeof(LintCompilerArgument))]
        [InlineData(typeof(ValidateCompilerArgument))]
        public void TestItAddsCompilerArguments(Type type)
        {
            Assert.True(ArgumentParserFactory.Make().HasCompilerArgument(type));
        }
        
        [Theory]
        [InlineData(typeof(DefaultCliArgument))]
        [InlineData(typeof(NoWaitCliArgument))]
        public void TestItAddsCliArguments(Type type)
        {
            Assert.True(ArgumentParserFactory.Make().HasCliArgument(type));
        }
    }
}
