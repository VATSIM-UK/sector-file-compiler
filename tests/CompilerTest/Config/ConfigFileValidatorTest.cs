using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.Equal("Key sct_header is not an array", ConfigFileValidator.lastError);
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
            Assert.Equal("Key not_sct_info is not a valid config section", ConfigFileValidator.lastError);
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
            Assert.Equal("Value 123 is not a valid string", ConfigFileValidator.lastError);
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
    }
}
