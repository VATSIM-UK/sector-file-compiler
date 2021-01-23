using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllFixesMustBeUniqueTest: AbstractValidatorTestCase
    {
        private readonly Fix first;
        private readonly Fix second;
        private readonly Fix third;
        private readonly Fix fourth;

        public AllFixesMustBeUniqueTest()
        {
            this.first = FixFactory.Make("DIKAS", new Coordinate("abc", "def"));
            this.second = FixFactory.Make("DIKAS", new Coordinate("abd", "cef"));
            this.third = FixFactory.Make("DIKAP", new Coordinate("abc", "def"));
            this.fourth = FixFactory.Make("DIKAS", new Coordinate("abc", "def"));
        }

        [Fact]
        public void TestItPassesIfCoordinatesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesIfIdentifiersDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.fourth);
            
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllFixesMustBeUnique();
        }
    }
}
