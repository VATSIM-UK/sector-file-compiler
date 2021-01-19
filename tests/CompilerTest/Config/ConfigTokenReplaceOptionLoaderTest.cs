using System.IO;
using System.Linq;
using Compiler.Argument;
using Compiler.Config;
using Compiler.Exception;
using Compiler.Transformer;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigTokenReplaceOptionLoaderTest
    {
        private ConfigTokenReplaceOptionLoader loader;
        private CompilerArguments arguments;

        public ConfigTokenReplaceOptionLoaderTest()
        {
            loader = new ConfigTokenReplaceOptionLoader();
            arguments = CompilerArgumentsFactory.Make();
        }

        [Theory]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/NotArray/config.json", "Invalid replace option - must be an array of objects")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/ItemNotObject/config.json", "Invalid replace item - must be an object")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/NoReplaceToken/config.json", "Invalid replace token - must be a string")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/ReplaceTokenNotString/config.json", "Invalid replace token - must be a string")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/NoReplaceType/config.json", "Invalid replace type - must be a string")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/ReplaceTypeNotString/config.json", "Invalid replace type - must be a string")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/ReplaceTypeInvalid/config.json", "Invalid replace type - must be date or version")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/DateFormatMissing/config.json", "Missing date format in replace")]
        [InlineData("_TestData/ConfigTokenReplaceOptionLoader/DateFormatNotString/config.json", "Invalid date format in replace, must be a string")]
        public void TestItThrowsExceptionOnInvalidConfig(string filename, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void TestItHandlesNoReplaceOption()
        {
            string filename = "_TestData/ConfigTokenReplaceOptionLoader/NoReplaceOption/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Empty(this.arguments.TokenReplacers);
        }
        
        [Fact]
        public void TestItAddsVersionReplacement()
        {
            string filename = "_TestData/ConfigTokenReplaceOptionLoader/VersionReplacement/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Single(arguments.TokenReplacers);
            Assert.IsType<TokenBuildVersionReplacer>(arguments.TokenReplacers.First());
            Assert.Equal("TEST", ((TokenBuildVersionReplacer) arguments.TokenReplacers.First()).Token);
        }
        
        [Fact]
        public void TestItAddsDateReplacement()
        {
            string filename = "_TestData/ConfigTokenReplaceOptionLoader/DateReplacement/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Single(arguments.TokenReplacers);
            Assert.IsType<TokenDateReplacer>(arguments.TokenReplacers.First());
            Assert.Equal("TEST", ((TokenDateReplacer) arguments.TokenReplacers.First()).Token);
            Assert.Equal("Y-m-d", ((TokenDateReplacer) arguments.TokenReplacers.First()).Format);
        }
    }
}