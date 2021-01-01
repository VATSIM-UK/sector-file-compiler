using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
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
            this.first = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string>());
            this.second = SidStarFactory.Make(false, "EGKK", "26L", "ADMAG2X", new List<string>());
            this.third = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string>());
            this.fourth = SidStarFactory.Make(true, "EGKK", "26L", "ADMAG2X", new List<string>() {"a"});
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesOnDifferentRoutes()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.fourth);
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSidsMustBeUnique();
        }
    }
}
