﻿using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCircleSectorlinesMustHaveValidCentreTest: AbstractValidatorTestCase
    {
        public AllCircleSectorlinesMustHaveValidCentreTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidFix(string fix)
        {
            this.sectorElements.Add(CircleSectorlineFactory.Make(centre: fix));
            this.sectorElements.Add(CircleSectorlineFactory.Make(centre: fix));

            // This one is ignored by the rule
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    new Coordinate("abc", "def"),
                    5.5,
                    SectorLineDisplayRuleFactory.MakeList(2),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            this.AssertNoValidationError();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "testndb")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidFix(string firstFix, string secondFix)
        {
            this.sectorElements.Add(CircleSectorlineFactory.Make(centre: firstFix));
            this.sectorElements.Add(CircleSectorlineFactory.Make(centre: secondFix));

            // This one is ignored by the rule
            this.sectorElements.Add(
                new CircleSectorline(
                    "ONE",
                    new Coordinate("abc", "def"),
                    5.5,
                    SectorLineDisplayRuleFactory.MakeList(2),
                    DefinitionFactory.Make(),
                    DocblockFactory.Make(),
                    CommentFactory.Make()
                )
            );

            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCircleSectorlinesMustHaveValidCentre();
        }
    }
}
