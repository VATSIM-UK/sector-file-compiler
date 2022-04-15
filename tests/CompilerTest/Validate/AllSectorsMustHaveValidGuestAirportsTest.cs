using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidGuestAirportsTest : AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidGuestAirportsTest()
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
        [InlineData("EHAM", "EDDM", "EIDW")]
        public void TestItPassesOnAllValid(string first, string second, string third)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(firstAirport: first, secondAirport: second),
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: "*"),
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: first),
                        SectorGuestFactory.Make(firstAirport: "*", secondAirport: "*"),
                    }
                )
            );

            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "EGNO", 2)]
        [InlineData("EGCC", "EGNO", "EGKK", 1)]
        [InlineData("EGKK", "EGNO", "EGNO", 2)]
        [InlineData("EGNO", "EGCC", "EGLL", 2)]
        [InlineData("EGNO", "EGNO", "EGNO", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(firstAirport: first, secondAirport: second),
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: "*"),
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(firstAirport: third, secondAirport: first),
                        SectorGuestFactory.Make(firstAirport: "*", secondAirport: "*"),
                    }
                )
            );

            AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidGuestAirports();
        }
    }
}
