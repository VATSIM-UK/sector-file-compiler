using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidDepartureAirportsTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidDepartureAirportsTest()
        {
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "EGKK")]
        [InlineData("EGCC", "EGCC", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGCC")]
        public void TestItPassesOnAllValid(string first, string second, string third)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    departureAirports: new List<SectorDepartureAirports>
                    {
                        SectorDepartureAirportsFactory.Make(new List<string> {first, second})
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    departureAirports: new List<SectorDepartureAirports>
                    {
                        SectorDepartureAirportsFactory.Make(new List<string> {second, third})
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "WHAT", 1)]
        [InlineData("EGCC", "WHAT", "EGKK", 2)]
        [InlineData("EGKK", "WHAT", "WHAT", 2)]
        [InlineData("WHAT", "EGCC", "EGLL", 1)]
        [InlineData("WHAT", "WHAT", "WHAT", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    departureAirports: new List<SectorDepartureAirports>
                    {
                        SectorDepartureAirportsFactory.Make(new List<string> {first, second})
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    departureAirports: new List<SectorDepartureAirports>
                    {
                        SectorDepartureAirportsFactory.Make(new List<string> {second, third})
                    }
                )
            );
            
            AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidDepartureAirports();
        }
    }
}
