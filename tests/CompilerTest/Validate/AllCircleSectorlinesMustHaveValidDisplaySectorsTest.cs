﻿using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCircleSectorlinesMustHaveValidDisplaySectorsTest: AbstractValidatorTestCase
    {
        public AllCircleSectorlinesMustHaveValidDisplaySectorsTest()
        {
            this.sectorElements.Add(SectorFactory.Make("COOL1"));
            this.sectorElements.Add(SectorFactory.Make("COOL2"));
        }

        [Theory]
        [InlineData("COOL1", "COOL1", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL2", "COOL1", "COOL2", "COOL1", "COOL2")]
        [InlineData("COOL1", "COOL1", "COOL2", "COOL2", "COOL2", "COOL1")]
        public void TestItPassesOnValidSector(string oneA, string twoA, string threeA, string oneB, string twoB, string threeB)
        {
            this.sectorElements.Add(
                CircleSectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );
            this.sectorElements.Add(
                CircleSectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );

            this.AssertNoValidationErrors();
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
            this.sectorElements.Add(
                CircleSectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneA, twoA, threeA),
                    }
                )
            );
            this.sectorElements.Add(
                CircleSectorlineFactory.Make(
                    displayRules: new List<SectorlineDisplayRule> {
                        SectorLineDisplayRuleFactory.Make(oneB, twoB, threeB),
                    }
                )
            );

            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCircleSectorlinesMustHaveValidDisplaySectors();
        }
    }
}
