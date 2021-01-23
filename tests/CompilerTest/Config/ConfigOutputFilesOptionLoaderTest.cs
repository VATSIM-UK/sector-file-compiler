using System;
using System.IO;
using Compiler.Argument;
using Compiler.Config;
using Compiler.Exception;
using Compiler.Output;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigOutputFilesOptionLoaderTest
    {
        private readonly ConfigOutputFilesOptionLoader loader;
        private readonly CompilerArguments arguments;

        public ConfigOutputFilesOptionLoaderTest()
        {
            loader = new ConfigOutputFilesOptionLoader();
            arguments = CompilerArgumentsFactory.Make();
        }

        [Theory]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/NotAString/config-ese.json", "Invalid field ese_output in config file _TestData/ConfigOutputFilesOptionLoader/NotAString/config-ese.json - must be a string")]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/NotAString/config-rwy.json", "Invalid field rwy_output in config file _TestData/ConfigOutputFilesOptionLoader/NotAString/config-rwy.json - must be a string")]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/NotAString/config-sct.json", "Invalid field sct_output in config file _TestData/ConfigOutputFilesOptionLoader/NotAString/config-sct.json - must be a string")]
        public void TestItThrowsExceptionOnInvalidConfig(string filename, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }
        
        [Theory]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/ValidConfig/config-ese.json", typeof(EseOutput))]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/ValidConfig/config-rwy.json", typeof(RwyOutput))]
        [InlineData("_TestData/ConfigOutputFilesOptionLoader/ValidConfig/config-sct.json", typeof(SctOutput))]
        public void TestItCreatesOutputFiles(string filename, Type expectedType)
        {
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Single(arguments.OutputFiles);
            Assert.Equal(expectedType, arguments.OutputFiles[0].GetType());
        }
    }
}