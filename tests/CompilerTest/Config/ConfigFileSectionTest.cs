using Compiler.Config;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Config
{
    public class ConfigFileSectionTest
    {
        [Fact]
        public void TestItSetsJsonPath()
        {
            ConfigFileSection section = new ConfigFileSection(
                "foo",
                InputDataType.ESE_AGREEMENTS
            );
            Assert.Equal("foo", section.JsonPath);
        }
        
        [Fact]
        public void TestItSetsInputDataType()
        {
            ConfigFileSection section = new ConfigFileSection(
                "foo",
                InputDataType.ESE_AGREEMENTS
            );
            Assert.Equal(InputDataType.ESE_AGREEMENTS, section.DataType);
        }
        
        [Fact]
        public void TestItSetsDescriptorToNullIfNotSpecified()
        {
            ConfigFileSection section = new ConfigFileSection(
                "foo",
                InputDataType.ESE_AGREEMENTS
            );
            Assert.Null(section.OutputGroupDescriptor);
        }
        
        [Fact]
        public void TestItSetsDescriptorIfSpecified()
        {
            ConfigFileSection section = new ConfigFileSection(
                "foo",
                InputDataType.ESE_AGREEMENTS,
                "bar"
            );
            Assert.Equal("bar", section.OutputGroupDescriptor);
        }
    }
}
