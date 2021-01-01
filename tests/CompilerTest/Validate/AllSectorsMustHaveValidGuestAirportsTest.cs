using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidGuestAirportsTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidGuestAirportsTest()
        {
            this.sectorElements.Add(AirportFactory.Make("EGKK"));
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "EGKK")]
        [InlineData("EGCC", "EGCC", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGCC")]
        public void TestItPassesOnAllValid(string first, string second, string third)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>()
                    {
                        SectorGuestFactory.Make(firstAirport: first, secondAirport: second),
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: "*"),
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>()
                    {
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: first),
                        SectorGuestFactory.Make(firstAirport: "*", secondAirport: "*"),
                    }
                )
            );
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "WHAT", 2)]
        [InlineData("EGCC", "WHAT", "EGKK", 1)]
        [InlineData("EGKK", "WHAT", "WHAT", 2)]
        [InlineData("WHAT", "EGCC", "EGLL", 2)]
        [InlineData("WHAT", "WHAT", "WHAT", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>()
                    {
                        SectorGuestFactory.Make(firstAirport: first, secondAirport: second),
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: "*"),
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>()
                    {
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: first),
                        SectorGuestFactory.Make(firstAirport: "*", secondAirport: "*"),
                    }
                )
            );
            
            this.AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidGuestAirports();
        }
    }
}
