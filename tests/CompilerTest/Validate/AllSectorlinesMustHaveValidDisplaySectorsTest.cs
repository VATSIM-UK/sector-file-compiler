using Xunit;
using Compiler.Model;
using Compiler.Validate;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorlinesMustHaveValidDisplaySectorsTest: AbstractValidatorTestCase
    {
        public AllSectorlinesMustHaveValidDisplaySectorsTest()
        {
            sectorElements.Add(SectorFactory.Make("COOL1"));
            sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1", "COOL1", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL2", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "COOL2", "COOL1")]
        public void TestItPassesOnValidSector(string oneA, string twoA, string threeA, string oneB, string twoB, string threeB)
        {
            sectorElements.Add(
                SectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );
            sectorElements.Add(
                SectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );

            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("NOTCOOL1", "COOL1", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "NOTCOOL2", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL1", "NOTCOOL2", "COOL2", "COOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "NOTCOOL2", "COOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "NOTCOOL2", "COOL1")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "COOL2", "NOTCOOL1")]
        public void TestItFailsOnInvalidSector(string oneA, string twoA, string threeA, string oneB, string twoB, string threeB)
        {
            sectorElements.Add(
                SectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                    }
                )
            );
            
            sectorElements.Add(
                SectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );

            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorlinesMustHaveValidDisplaySectors();
        }
    }
}
