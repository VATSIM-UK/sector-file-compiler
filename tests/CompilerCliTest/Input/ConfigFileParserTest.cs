using Xunit;
using Compiler.Argument;
using Compiler.Input;
using CompilerCli.Input;

namespace CompilerCliTest.Input
{
    public class ConfigFileParserTest
    {
        [Fact]
        public void TestItSetsConfigFile()
        {
            CompilerArguments arguments = new CompilerArguments();
            ConfigFileParser parser = new ConfigFileParser();

            arguments = parser.Parse("test.json", arguments);
            Assert.Equal(new InputFile("test.json"), arguments.ConfigFile);
        }
    }
}
