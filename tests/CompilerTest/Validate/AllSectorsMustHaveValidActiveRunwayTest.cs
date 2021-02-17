using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidActiveRunwayTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidActiveRunwayTest()
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
        [InlineData("000A", "00", "000A", "01")]
        public void TestItPassesOnAllValid(string firstAirport, string firstRunway, string secondAirport, string secondRunway)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(firstAirport, firstRunway),
                        SectorActiveFactory.Make(secondAirport, secondRunway),
                    }
                )
            );
            
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(firstAirport, firstRunway),
                        SectorActiveFactory.Make(secondAirport, secondRunway),
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGCC", "23R", "EGLL", "27R", 2)]
        [InlineData("EGKK", "27L", "EGLL", "27R", 2)]
        [InlineData("EGKK", "26R", "EGKK", "26R", 2)] // Double failures only count once
        [InlineData("EGLL", "26R", "EGLL", "23L", 2)]
        [InlineData("000B", "01", "000A", "04", 2)]
        public void TestItFailsOnInvalid (string firstAirport, string firstRunway, string secondAirport, string secondRunway, int failTimes)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(firstAirport, firstRunway),
                        SectorActiveFactory.Make(secondAirport, secondRunway),
                    }
                )
            );
            
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(firstAirport, firstRunway),
                        SectorActiveFactory.Make(secondAirport, secondRunway),
                    }
                )
            );
            
            AssertValidationErrors(failTimes);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidActiveRunway();
        }
    }
}
