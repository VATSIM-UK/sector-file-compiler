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
            first = FixFactory.Make("DIKAS", new Coordinate("abc", "def"));
            second = FixFactory.Make("DIKAS", new Coordinate("abd", "cef"));
            third = FixFactory.Make("DIKAP", new Coordinate("abc", "def"));
            fourth = FixFactory.Make("DIKAS", new Coordinate("abc", "def"));
        }

        [Fact]
        public void TestItPassesIfCoordinatesDifferent()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItPassesIfIdentifiersDifferent()
        {
            sectorElements.Add(first);
            sectorElements.Add(third);
            
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            sectorElements.Add(first);
            sectorElements.Add(fourth);
            
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllFixesMustBeUnique();
        }
    }
}
