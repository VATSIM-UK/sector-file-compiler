using Xunit;
using Newtonsoft.Json.Linq;
using Compiler.Config;

namespace CompilerTest.Config
{
    public class ConfigFileValidatorTest
    {
        [Fact]
        public void ItReturnsFalseIfTheValueIsNotAnArray()
        {
            string config = @"{
              sct_header: 'Intel',
              sct_info: [
                '../info.txt',
              ]
            }";

            Assert.False(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
            Assert.Equal("Key sct_header must be an array or object, String detected", ConfigFileValidator.LastError);
        }

        [Fact]
        public void ItReturnsFalseIfTheKeyIsNotvalid()
        {
            string config = @"{
              sct_header: [
                '../header.txt',
              ],
              not_sct_info: [
                '../info.txt',
              ]
            }";

            Assert.False(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
            Assert.Equal("Key not_sct_info is not a valid config section", ConfigFileValidator.LastError);
        }

        [Fact]
        public void ItReturnsFalseIfTheValueIsNotAString()
        {
            string config = @"{
              sct_header: [
                '../header.txt',
              ],
              sct_info: [
                '../info1.txt',
                123,
                '../info2.txt',
              ]
            }";

            Assert.False(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
            Assert.Equal("Value 123 is not a valid string", ConfigFileValidator.LastError);
        }

        [Fact]
        public void ItReturnsFalseIfTheSubsectionIsNotAnArray()
        {
            string config = @"{
              sct_header: {
                  subsection_1: {
                      subsection_2: [
                          '../header.txt',
                      ]
                  }
              },
            }";

            Assert.False(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
            Assert.Equal("Key subsection_1 is not an array", ConfigFileValidator.LastError);
        }

        [Fact]
        public void ItReturnsFalseIfTheSubsectionHasInvalidValues()
        {
            string config = @"{
              sct_header: {
                  subsection_1: [
                      1234,
                  ]
              },
            }";

            Assert.False(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
            Assert.Equal("Value 1234 is not a valid string", ConfigFileValidator.LastError);
        }

        [Fact]
        public void ItReturnsTrueIfConfigValid()
        {
            string config = @"{
              sct_header: [
                '../header.txt',
              ],
              sct_info: [
                '../info1.txt',
                '../info2.txt',
              ]
            }";

            Assert.True(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
        }

        [Fact]
        public void ItReturnsTrueIfConfigValidWithSubsections()
        {
            string config = @"{
              sct_header: [
                '../header.txt',
              ],
              sct_info: {
                 subsection_1: [
                     '../info1.txt',
                 ],
                 subsection_2: [
                     '../info2.txt',
                 ],
              }
            }";

            Assert.True(ConfigFileValidator.ConfigFileValid(JObject.Parse(config)));
        }
    }
}
