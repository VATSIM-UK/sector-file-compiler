using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidActiveAirportTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidActiveAirportTest()
        {
            sectorElements.Add(AirportFactory.Make("EGKK"));
            sectorElements.Add(AirportFactory.Make("EGLL"));
            sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "EGKK")]
        [InlineData("EGCC", "EGCC", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGKK")]
        [InlineData("000A", "000A", "000A")]
        public void TestItPassesOnAllValid(string first, string second, string third)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(first),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(second),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "WHAT", 2)]
        [InlineData("WHAT", "EGCC", "EGKK", 1)]
        [InlineData("EGKK", "WHAT", "WHAT", 2)]
        [InlineData("WHAT", "WHAT", "WHAT", 2)]
        [InlineData("000B", "000A", "000C", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(first),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>
                    {
                        SectorActiveFactory.Make(second),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            
            AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidActiveAirport();
        }
    }
}
