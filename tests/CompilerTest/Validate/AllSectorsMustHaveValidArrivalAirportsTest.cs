using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidArrivalAirportsTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidArrivalAirportsTest()
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
                    arrivalAirports: new List<SectorArrivalAirports>()
                    {
                        SectorArrivalAirportsFactory.Make(new List<string>(){first, second})
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    arrivalAirports: new List<SectorArrivalAirports>()
                    {
                        SectorArrivalAirportsFactory.Make(new List<string>(){second, third})
                    }
                )
            );
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "WHAT", 1)]
        [InlineData("EGCC", "WHAT", "EGKK", 2)]
        [InlineData("EGKK", "WHAT", "WHAT", 2)]
        [InlineData("WHAT", "EGCC", "EGLL", 1)]
        [InlineData("WHAT", "WHAT", "WHAT", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    arrivalAirports: new List<SectorArrivalAirports>()
                    {
                        SectorArrivalAirportsFactory.Make(new List<string>(){first, second})
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    arrivalAirports: new List<SectorArrivalAirports>()
                    {
                        SectorArrivalAirportsFactory.Make(new List<string>(){second, third})
                    }
                )
            );
            
            this.AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidArrivalAirports();
        }
    }
}
