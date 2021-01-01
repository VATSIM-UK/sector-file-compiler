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
    public class AllSectorsMustHaveValidActiveAirportTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidActiveAirportTest()
        {
            this.sectorElements.Add(AirportFactory.Make("EGKK"));
            this.sectorElements.Add(AirportFactory.Make("EGLL"));
            this.sectorElements.Add(AirportFactory.Make("EGCC"));
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "EGKK")]
        [InlineData("EGCC", "EGCC", "EGKK")]
        [InlineData("EGKK", "EGLL", "EGKK")]
        [InlineData("000A", "000A", "000A")]
        public void TestItPassesOnAllValid(string first, string second, string third)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>()
                    {
                        SectorActiveFactory.Make(first),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>()
                    {
                        SectorActiveFactory.Make(second),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            
            this.AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("EGLL", "EGCC", "WHAT", 2)]
        [InlineData("WHAT", "EGCC", "EGKK", 1)]
        [InlineData("EGKK", "WHAT", "WHAT", 2)]
        [InlineData("WHAT", "WHAT", "WHAT", 2)]
        [InlineData("000B", "000A", "000C", 2)]
        public void TestItFailsOnInvalid(string first, string second, string third, int timesCalled)
        {
            this.sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>()
                    {
                        SectorActiveFactory.Make(first),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            this.sectorElements.Add(
                SectorFactory.Make(
                    active: new List<SectorActive>()
                    {
                        SectorActiveFactory.Make(second),
                        SectorActiveFactory.Make(third)
                    }
                )
            );
            
            this.AssertValidationErrors(timesCalled);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidActiveAirport();
        }
    }
}
