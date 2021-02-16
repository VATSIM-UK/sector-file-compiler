using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSidsMustBeUniqueTest: AbstractValidatorTestCase
    {
        private readonly SidStar first;
        private readonly SidStar second;
        private readonly SidStar third;
        private readonly SidStar fourth;

        public AllSidsMustBeUniqueTest()
        {
            first = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string>());
            second = SidStarFactory.Make(false, "EGKK", "26L", "ADMAG2X", new List<string>());
            third = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string>());
            fourth = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string> {"a"});
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesOnDifferentRoutes()
        {
            sectorElements.Add(first);
            sectorElements.Add(fourth);
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustBeUnique();
        }
    }
}
