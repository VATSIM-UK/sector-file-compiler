using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Config;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupDescriptionGeneratorTest
    {
        [Fact]
        public void TestItReturnsCorrectEnrouteDescription()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("Start enroute foo", OutputGroupDescriptionGenerator.GeneratEnrouteDescription(configSection));
        }

        [Fact]
        public void TestItReturnsCorrectMiscDescription()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("Start misc foo", OutputGroupDescriptionGenerator.GenerateMiscDescription(configSection));
        }
        
        [Fact]
        public void TestItReturnsCorrectAirportDescription()
        {
            ConfigFileSection configSection = ConfigFileSectionFactory.Make(descriptor: "foo", dataType: InputDataType.ESE_AGREEMENTS);
            Assert.Equal("Start EGLL foo", OutputGroupDescriptionGenerator.GenerateAirportDescription(configSection, "EGLL"));
        }
    }
}
