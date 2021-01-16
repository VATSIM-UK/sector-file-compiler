using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupFactoryTest
    {
        [Fact]
        public void TestItReturnsAirportGroupWithoutSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: null, dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateAirport(section, "EGLL");
            Assert.Equal("airport.SCT_FIXES", group.Key);
            Assert.Null(group.HeaderDescription);
        }

        [Fact]
        public void TestItReturnsAirportGroupWithSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateAirport(section, "EGLL");
            Assert.Equal("airport.SCT_FIXES.EGLL", group.Key);
            Assert.Equal("Start EGLL foo", group.HeaderDescription);
        }
        
        [Fact]
        public void TestItReturnsEnrouteGroupWithoutSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: null, dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateEnroute(section);
            Assert.Equal("enroute.SCT_FIXES", group.Key);
            Assert.Null(group.HeaderDescription);
        }

        [Fact]
        public void TestItReturnsEnrouteGroupWithSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateEnroute(section);
            Assert.Equal("enroute.SCT_FIXES", group.Key);
            Assert.Equal("Start enroute foo", group.HeaderDescription);
        }
        
        [Fact]
        public void TestItReturnsMiscGroupWithoutSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: null, dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateMisc(section);
            Assert.Equal("misc.SCT_FIXES", group.Key);
            Assert.Null(group.HeaderDescription);
        }

        [Fact]
        public void TestItReturnsMiscGroupWithSpecificDescriptor()
        {
            ConfigFileSection section = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.SCT_FIXES);
            OutputGroup group = OutputGroupFactory.CreateMisc(section);
            Assert.Equal("misc.SCT_FIXES", group.Key);
            Assert.Equal("Start misc foo", group.HeaderDescription);
        }
    }
}
