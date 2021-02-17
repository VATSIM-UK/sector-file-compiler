using Xunit;
using Compiler.Model;
using Compiler.Validate;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorsMustHaveValidGuestControllerTest: AbstractValidatorTestCase
    {
        public AllSectorsMustHaveValidGuestControllerTest()
        {
            sectorElements.Add(ControllerPositionFactory.Make("BBR"));
            sectorElements.Add(ControllerPositionFactory.Make("BBF"));
            sectorElements.Add(ControllerPositionFactory.Make("LS"));
            sectorElements.Add(ControllerPositionFactory.Make("LC"));
        }

        [Theory]
        [InlineData("BBR", "BBF", "LS")]
        [InlineData("LC", "BBF", "BBR")]
        [InlineData("BBR", "BBR", "BBR")]
        public void TestItPassesOnValidControllers(string first, string second, string third)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(first),
                        SectorGuestFactory.Make(third),
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(second),
                        SectorGuestFactory.Make(third),
                    }
                )
            );
            
            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("BBR", "NOPE", "LS", 1)]
        [InlineData("NOPE", "BBF", "NOPE", 2)]
        [InlineData("NOPE", "NOPE", "NOPE", 2)]
        [InlineData("LS", "NOPE", "NOPE", 2)]
        [InlineData("NOPE", "NOPE", "LS", 2)]
        public void TestItFailsOnInvalidControllers(string first, string second, string third, int numErrors)
        {
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(first),
                        SectorGuestFactory.Make(third),
                    }
                )
            );
            sectorElements.Add(
                SectorFactory.Make(
                    guests: new List<SectorGuest>
                    {
                        SectorGuestFactory.Make(second),
                        SectorGuestFactory.Make(third),
                    }
                )
            );
            
            AssertValidationErrors(numErrors);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveValidGuestController();
        }
    }
}
