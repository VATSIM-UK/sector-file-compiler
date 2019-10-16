using Xunit;
using Compiler.Input;

namespace CompilerTest.Input
{
    public class ArgumentFactoryTest
    {
        [Fact]
        public void ItReturnsStringArguments()
        {
            Argument expected = new Argument(ArgumentType.ConfigFile, "test");
            Assert.Equal(expected, ArgumentFactory.CreateFromString(ArgumentType.ConfigFile, "test"));
        }
    }
}
