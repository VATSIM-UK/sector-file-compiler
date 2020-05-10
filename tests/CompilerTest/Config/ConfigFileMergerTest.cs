using System;
using Xunit;
using Moq;
using Compiler.Input;
using Compiler.Argument;
using Compiler.Config;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompilerTest.Config
{
    public class ConfigFileMergerTest
    {
        private readonly Mock<IFileInterface> mockInput1;
        private readonly Mock<IFileInterface> mockInput2;
        private readonly CompilerArguments arguments;

        public ConfigFileMergerTest()
        {
            this.mockInput1 = new Mock<IFileInterface>();
            this.mockInput2 = new Mock<IFileInterface>();
            this.arguments = new CompilerArguments();
        }

        [Fact]
        public void ItHandlesASingleConfigFile()
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


            this.mockInput1.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput1.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput1.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.arguments.ConfigFiles.Add(this.mockInput1.Object);
            Dictionary <string, List<string>> expectedOutput = 
                JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                    expectedObject.ToString()
                );
            Assert.Equal(expectedOutput, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }

        [Fact]
        public void ItHandlesMultipleConfigFiles()
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

            string config2 = @"{
              sct_artcc_high: [
                '../artcc.txt',
              ]
            }";

            // Populate the expected, we expect the relative paths to be resolved
            string expected = @"{
              sct_header: [
              ],
              sct_info: [
              ],
              sct_artcc_high: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JArray headerArray = (JArray)expectedObject["sct_header"];
            headerArray.Add(Path.GetFullPath("foo/header.txt"));

            JArray infoArray = (JArray)expectedObject["sct_info"];
            infoArray.Add(Path.GetFullPath("foo/bar/info1.txt"));
            infoArray.Add(Path.GetFullPath("foo/info2.txt"));

            JArray artccArray = (JArray)expectedObject["sct_artcc_high"];
            artccArray.Add(Path.GetFullPath("foo2/artcc.txt"));


            this.mockInput1.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput1.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput1.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.mockInput2.Setup(foo => foo.Contents()).Returns(config2);
            this.mockInput2.Setup(foo => foo.GetPath()).Returns("foo2/bar/baz.txt");
            this.mockInput2.Setup(foo => foo.DirectoryLocation()).Returns("foo2/bar");

            this.arguments.ConfigFiles.Add(this.mockInput1.Object);
            this.arguments.ConfigFiles.Add(this.mockInput2.Object);
            Dictionary<string, List<string>> expectedOutput =
                JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                    expectedObject.ToString()
                );
            Assert.Equal(expectedOutput, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }

        [Fact]
        public void ItRemovesDuplicateFiles()
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

            string config2 = @"{
              sct_artcc_high: [
                '../artcc.txt',
              ],
              sct_info: [
                'info1.txt',
              ]
            }";

            // Populate the expected, we expect the relative paths to be resolved
            string expected = @"{
              sct_header: [
              ],
              sct_info: [
              ],
              sct_artcc_high: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JArray headerArray = (JArray)expectedObject["sct_header"];
            headerArray.Add(Path.GetFullPath("foo/header.txt"));

            JArray infoArray = (JArray)expectedObject["sct_info"];
            infoArray.Add(Path.GetFullPath("foo/bar/info1.txt"));
            infoArray.Add(Path.GetFullPath("foo/info2.txt"));

            JArray artccArray = (JArray)expectedObject["sct_artcc_high"];
            artccArray.Add(Path.GetFullPath("foo/artcc.txt"));


            this.mockInput1.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput1.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput1.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.mockInput2.Setup(foo => foo.Contents()).Returns(config2);
            this.mockInput2.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput2.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.arguments.ConfigFiles.Add(this.mockInput1.Object);
            this.arguments.ConfigFiles.Add(this.mockInput2.Object);
            Dictionary<string, List<string>> expectedOutput =
                JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                    expectedObject.ToString()
                );

            dynamic actual = ConfigFileMerger.MergeConfigFiles(this.arguments);
            Assert.Equal(expectedOutput, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }
    }
}
