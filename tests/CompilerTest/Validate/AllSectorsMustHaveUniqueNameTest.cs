using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Validate;
using Moq;
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
            sectorElements = new SectorElementCollection();
            loggerMock = new Mock<IEventLogger>();
            first = SectorFactory.Make("ONE");
            second = SectorFactory.Make("ONE");
            third = SectorFactory.Make("NOTONE");
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            sectorElements.Add(first);
            sectorElements.Add(third);
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnNameClash()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllSectorsMustHaveUniqueName();
        }
    }
}
