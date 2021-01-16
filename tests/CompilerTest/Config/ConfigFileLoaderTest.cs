using System.Collections.Generic;
using Compiler.Config;
using Xunit;
using Compiler.Exception;;

namespace CompilerTest.Config
{
    public class ConfigFileLoaderTest
    {
        private readonly ConfigFileLoader fileLoader;

        public ConfigFileLoaderTest()
        {
            this.fileLoader = new ConfigFileLoader();
        }
        
        [Theory]
        [InlineData("xyz", "Config file not found")]
        public void TestItThrowsExceptionOnBadData(string fileToLoad, string expectedMessage)
        {
            ConfigFileInvalidException exception = Assert.Throws<ConfigFileInvalidException>(
                () => fileLoader.LoadConfigFiles(new List<string> {fileToLoad})
            );
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
