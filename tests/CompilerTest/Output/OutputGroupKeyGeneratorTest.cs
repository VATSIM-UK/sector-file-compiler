using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupKeyGeneratorTest
    {
        [Fact]
        public void TestItReturnsCorrectEnrouteKey()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("enroute.ESE_AGREEMENTS", OutputGroupKeyGenerator.GeneratEnrouteKey(configSection));
        }
        
        [Fact]
        public void TestItReturnsCorrectMiscKey()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("misc.ESE_AGREEMENTS", OutputGroupKeyGenerator.GenerateMiscKey(configSection));
        }
        
        [Fact]
        public void TestItReturnsCorrectAirportKeyWithDescriptor()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("airport.ESE_AGREEMENTS.EGLL", OutputGroupKeyGenerator.GenerateAirportKey(configSection, "EGLL"));
        }
        
        [Fact]
        public void TestItReturnsCorrectAirportKeyWithoutDescriptor()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(descriptor: null, dataType: InputDataType.SCT_STARS);
            Assert.Equal("airport.SCT_STARS", OutputGroupKeyGenerator.GenerateAirportKey(configSection, "EGLL"));
        }
    }
}
