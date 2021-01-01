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
    public class AllSectorsMustHaveUniqueNameTest: AbstractValidatorTestCase
    {
        private readonly Sector first;
        private readonly Sector second;
        private readonly Sector third;

        public AllSectorsMustHaveUniqueNameTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = SectorFactory.Make("ONE");
            this.second = SectorFactory.Make("ONE");
            this.third = SectorFactory.Make("NOTONE");
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnNameClash()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveUniqueName();
        }
    }
}
