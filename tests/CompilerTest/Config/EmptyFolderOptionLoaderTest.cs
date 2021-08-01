using System.IO;
using Compiler.Argument;
using Compiler.Config;
using Compiler.Exception;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CompilerTest.Config
{
    public class EmptyFolderOptionLoaderTest
    {
        private EmptyFileOptionLoader loader;
        private CompilerArguments arguments;

        public EmptyFolderOptionLoaderTest()
        {
            loader = new EmptyFileOptionLoader();
            arguments = CompilerArgumentsFactory.Make();
        }

        [Theory]
        [InlineData("_TestData/EmptyFolderOptionLoader/TokenNotString/config.json", "empty_folder option must be a string")]
        [InlineData("_TestData/EmptyFolderOptionLoader/TokenNotValid/config.json", "Invalid option for empty_folder")]
        public void TestItThrowsExceptionOnInvalidConfig(string filename, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename)
            );
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void TestItDefaultsToIgnore()
        {
            string filename = "_TestData/EmptyFolderOptionLoader/NoToken/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Equal(CompilerArguments.EmptyFolderIgnore, arguments.EmptyFolderAction);
        }
        
        [Fact]
        public void TestItSetsIgnoreFromConfig()
        {
            string filename = "_TestData/EmptyFolderOptionLoader/Ignore/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Equal(CompilerArguments.EmptyFolderIgnore, arguments.EmptyFolderAction);
        }
        
        [Fact]
        public void TestItSetsWarningFromConfig()
        {
            string filename = "_TestData/EmptyFolderOptionLoader/Warning/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Equal(CompilerArguments.EmptyFolderWarning, arguments.EmptyFolderAction);
        }
        
        [Fact]
        public void TestItSetsErrorFromConfig()
        {
            string filename = "_TestData/EmptyFolderOptionLoader/Error/config.json";
            loader.LoadConfig(arguments, JObject.Parse(File.ReadAllText(filename)), filename);
            Assert.Equal(CompilerArguments.EmptyFolderError, arguments.EmptyFolderAction);
        }
    }
}
