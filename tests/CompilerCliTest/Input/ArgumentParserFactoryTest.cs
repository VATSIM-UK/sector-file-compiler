using System;
using CompilerCli.Input;
using Xunit;

namespace CompilerCliTest.Input
{
    public class ArgumentParserFactoryTest
    {
        [Theory]
        [InlineData(typeof(DefaultArgument))]
        [InlineData(typeof(SkipValidationArgument))]
        [InlineData(typeof(StripCommentsArgument))]
        [InlineData(typeof(ConfigFileArgument))]
        [InlineData(typeof(BuildVersionArgument))]
        public void TestItAddsArguments(Type type)
        {
            Assert.True(ArgumentParserFactory.Make().HasArgument(type));
        }
    }
}