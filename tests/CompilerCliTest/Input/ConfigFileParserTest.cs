using Xunit;
using Compiler.Argument;
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
            Assert.Equal("test.json", arguments.ConfigFilePath);
        }
    }
}
