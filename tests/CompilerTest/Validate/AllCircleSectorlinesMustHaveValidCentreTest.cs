using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllCircleSectorlinesMustHaveValidCentreTest: AbstractValidatorTestCase
    {
        public AllCircleSectorlinesMustHaveValidCentreTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Theory]
        [InlineData("testfix")]
        [InlineData("testvor")]
        [InlineData("testndb")]
        [InlineData("testairport")]
        public void TestItPassesOnValidFix(string fix)
        {
            sectorElements.Add(CircleSectorlineFactory.Make(centre: fix));
            sectorElements.Add(CircleSectorlineFactory.Make(centre: fix));

            // This one is ignored by the rule
            sectorElements.Add(
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

            AssertNoValidationErrors();
        }

        [Theory]
        [InlineData("nottestfix","testfix")]
        [InlineData("testvor", "nottestvor")]
        [InlineData("nottestndb", "testndb")]
        [InlineData("testairport", "nottestairport")]
        public void TestItFailsOnInvalidFix(string firstFix, string secondFix)
        {
            sectorElements.Add(CircleSectorlineFactory.Make(centre: firstFix));
            sectorElements.Add(CircleSectorlineFactory.Make(centre: secondFix));

            // This one is ignored by the rule
            sectorElements.Add(
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

            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllCircleSectorlinesMustHaveValidCentre();
        }
    }
}
