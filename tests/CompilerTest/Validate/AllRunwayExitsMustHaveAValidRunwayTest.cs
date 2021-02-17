using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRunwayExitsMustHaveAValidRunwayTest: AbstractValidatorTestCase
    {
        public AllRunwayExitsMustHaveAValidRunwayTest()
        {
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
            sectorElements.Add(RunwayFactory.Make("EGKK", "26L", "09"));
            sectorElements.Add(RunwayFactory.Make("EGLL", "27L", "09"));
            sectorElements.Add(RunwayFactory.Make("EGLL", "27R", "09"));
            sectorElements.Add(RunwayFactory.Make("EGCC", "23L", "09"));
        }

        [Theory]
        [InlineData("EGCC", "23L", "EGLL", "27R")]
        [InlineData("EGLL", "27L", "EGLL", "27R")]
        [InlineData("EGKK", "26L", "EGCC", "23L")]
        [InlineData("EGCC", "23L", "EGCC", "23L")]
        public void TestItPassesOnAllValid(string firstAirport, string firstRunway, string secondAirport, string secondRunway)
        {
            sectorElements.Add(
                GroundNetworkFactory.Make(
                    firstAirport,
                    new List<GroundNetworkRunwayExit>
                    {
                        GroundNetworkRunwayExitFactory.Make(firstRunway),
                        GroundNetworkRunwayExitFactory.Make(firstRunway)
                    }
                )
            );
            
            sectorElements.Add(
                GroundNetworkFactory.Make(
                    secondAirport,
                    new List<GroundNetworkRunwayExit>
                    {
                        GroundNetworkRunwayExitFactory.Make(secondRunway),
                        GroundNetworkRunwayExitFactory.Make(secondRunway)
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGCC", "23R", "EGLL", "27R", 1)]
        [InlineData("EGKK", "27L", "EGLL", "27R", 1)]
        [InlineData("EGLL", "26R", "EGLL", "23L", 2)]
        [InlineData("000A", "01", "000A", "00", 2)]
        public void TestItFailsOnInvalid(string firstAirport, string firstRunway, string secondAirport, string secondRunway, int failTimes)
        {
            sectorElements.Add(
                GroundNetworkFactory.Make(
                    firstAirport,
                    new List<GroundNetworkRunwayExit>
                    {
                        GroundNetworkRunwayExitFactory.Make(firstRunway),
                        GroundNetworkRunwayExitFactory.Make(firstRunway)
                    }
                )
            );
            
            sectorElements.Add(
                GroundNetworkFactory.Make(
                    secondAirport,
                    new List<GroundNetworkRunwayExit>
                    {
                        GroundNetworkRunwayExitFactory.Make(secondRunway),
                        GroundNetworkRunwayExitFactory.Make(secondRunway)
                    }
                )
            );
            
            AssertValidationErrors(failTimes);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRunwayExitsMustHaveAValidRunway();
        }
    }
}
