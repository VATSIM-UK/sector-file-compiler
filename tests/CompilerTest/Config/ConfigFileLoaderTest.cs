using System;
using Xunit;
using Moq;
using Compiler.Input;
using Compiler.Config;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CompilerTest.Config
{
    public class ConfigFileLoaderTest
    {
        private readonly Mock<IFileInterface> mockInput;

        public ConfigFileLoaderTest()
        {
            this.mockInput = new Mock<IFileInterface>();
        }

        [Fact]
        public void ItThrowsExceptionOnInvalidJson()
        {
            string badJson = @"[
              sct_header: 'Intel',
              sct_info: [
                '../info.txt',
              ]
            }";

            this.mockInput.Setup(foo => foo.Contents()).Returns(badJson);
            this.mockInput.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");

            var exception = Assert.Throws<ArgumentException>(
                () => ConfigFileLoader.LoadConfigFile(mockInput.Object)
            );
            Assert.StartsWith("Invalid JSON in foo/bar/baz.txt: ", exception.Message);
        }

        [Fact]
        public void ItThrowsExceptionInvalidFormat()
        {
            string badJson = @"{
              sct_header: [
                '../header.txt',
              ],
              not_sct_info: [
                '../info.txt',
              ]
            }";

            this.mockInput.Setup(foo => foo.Contents()).Returns(badJson);
            this.mockInput.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");

            var exception = Assert.Throws<ArgumentException>(
                () => ConfigFileLoader.LoadConfigFile(mockInput.Object)
            );
            Assert.StartsWith("Invalid format in ", exception.Message);
        }

        [Fact]
        public void ItReturnsNormalisedConfigFile()
        {
            string config = @"{
              sct_header: [
                '../header.txt',
              ],
              sct_info: [
                'info1.txt',
                '../info2.txt',
              ]
            }";

            // Populate the expected, we expect the relative paths to be resolved
            string expected = @"{
              sct_header: [
              ],
              sct_info: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JArray headerArray = (JArray)expectedObject["sct_header"];
            headerArray.Add(Path.GetFullPath("foo/header.txt"));

            JArray infoArray = (JArray)expectedObject["sct_info"];
            infoArray.Add(Path.GetFullPath("foo/bar/info1.txt"));
            infoArray.Add(Path.GetFullPath("foo/info2.txt"));


            this.mockInput.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            Assert.Equal(expectedObject, ConfigFileLoader.LoadConfigFile(this.mockInput.Object));
        }
    }
}
