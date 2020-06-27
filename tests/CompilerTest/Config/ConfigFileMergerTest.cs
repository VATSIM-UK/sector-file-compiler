using Xunit;
using Moq;
using Compiler.Input;
using Compiler.Argument;
using Compiler.Config;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

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
              sct_header: {
                subsection_1: [
                  '../header.txt',
                ],
              },
              sct_info: [
                'info1.txt',
                '../info2.txt',
              ]
            }";

            // Populate the expected, we expect the relative paths to be resolved
            string expected = @"{
              sct_header: {
                subsection_1: [
                ],
              },
              sct_info: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JObject header = (JObject)expectedObject["sct_header"];
            JArray headerArray = (JArray)header["subsection_1"];
            headerArray.Add(Path.GetFullPath("foo/header.txt"));

            JArray infoArray = (JArray)expectedObject["sct_info"];
            infoArray.Add(Path.GetFullPath("foo/bar/info1.txt"));
            infoArray.Add(Path.GetFullPath("foo/info2.txt"));


            this.mockInput1.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput1.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput1.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.arguments.ConfigFiles.Add(this.mockInput1.Object);
            Assert.Equal(expectedObject, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }

        [Fact]
        public void ItHandlesMultipleConfigFiles()
        {
            string config = @"{
              sct_header: {
                subsection_1: [
                  '../header.txt',
                ],
              },
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
              sct_header: {
                subsection_1: [
                ],
              },
              sct_info: [
              ],
              sct_artcc_high: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JObject header = (JObject)expectedObject["sct_header"];
            JArray headerArray = (JArray)header["subsection_1"];
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
            Assert.Equal(expectedObject, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }

        [Fact]
        public void ItHandlesUnmergablefiles()
        {
            // Configs cant merge because of clashes between objects and arrays
            string config = @"{
              sct_header: {
                subsection_1: [
                  '../header.txt',
                ],
              },
              sct_info: [
                'info1.txt',
                '../info2.txt',
              ]
            }";

            string config2 = @"{
              sct_header: [
                'nope.txt',
              ],
            }";

            this.mockInput1.Setup(foo => foo.Contents()).Returns(config);
            this.mockInput1.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput1.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.mockInput2.Setup(foo => foo.Contents()).Returns(config2);
            this.mockInput2.Setup(foo => foo.GetPath()).Returns("foo/bar/baz.txt");
            this.mockInput2.Setup(foo => foo.DirectoryLocation()).Returns("foo/bar");

            this.arguments.ConfigFiles.Add(this.mockInput1.Object);
            this.arguments.ConfigFiles.Add(this.mockInput2.Object);

            var ex = Assert.Throws<ArgumentException>(() => ConfigFileMerger.MergeConfigFiles(this.arguments));
            Assert.Equal("Incompatible configs at key sct_header, cannot merge", ex.Message);
        }

        [Fact]
        public void ItRemovesDuplicateFiles()
        {
            string config = @"{
              sct_header: {
                subsection_1: [
                  '../header.txt',
                ],
              },
              sct_info: [
                'info1.txt',
                '../info2.txt',
              ]
            }";

            string config2 = @"{
              sct_header: {
                subsection_1: [
                  '../header.txt',
                ],
              },
              sct_artcc_high: [
                '../artcc.txt',
              ],
              sct_info: [
                'info1.txt',
              ]
            }";

            // Populate the expected, we expect the relative paths to be resolved
            string expected = @"{
              sct_header: {
                subsection_1: [
                ],
              },
              sct_info: [
              ],
              sct_artcc_high: [
              ]
            }";

            JObject expectedObject = JObject.Parse(expected);
            JObject header = (JObject)expectedObject["sct_header"];
            JArray headerArray = (JArray)header["subsection_1"];
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
            Assert.Equal(expectedObject, ConfigFileMerger.MergeConfigFiles(this.arguments));
        }
    }
}
