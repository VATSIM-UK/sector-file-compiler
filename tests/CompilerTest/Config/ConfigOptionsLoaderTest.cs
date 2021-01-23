using System.IO;
using Compiler.Argument;
using Compiler.Config;
using Compiler.Exception;
using Compiler.Output;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigOptionsLoaderTest
    {
        private ConfigOptionsLoader loader;
        private CompilerArguments arguments;

        public ConfigOptionsLoaderTest()
        {
            loader = ConfigOptionsLoaderFactory.Make();
            arguments = CompilerArgumentsFactory.Make();
        }

        [Theory]
        [InlineData("_TestData/ConfigOptionsLoader/NotObject/config.json", "Config options must be an object in file _TestData/ConfigOptionsLoader/NotObject/config.json")]
        public void TestItThrowsExceptionOnInvalidConfig(string filename, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => loader.LoadOptions(arguments, JObject.Parse(File.ReadAllText(filename)), filename)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void TestItIgnoresNoOptions()
        {
            string filename = "_TestData/ConfigOptionsLoader/NoOptions/config.json";
            loader.LoadOptions(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
        }
        
        [Fact]
        public void TestItLoadsConfig()
        {
            string filename = "_TestData/ConfigOptionsLoader/ValidConfig/config.json";
            loader.LoadOptions(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Single(arguments.OutputFiles);
            Assert.Equal(typeof(SctOutput), arguments.OutputFiles[0].GetType());
        }
    }
}