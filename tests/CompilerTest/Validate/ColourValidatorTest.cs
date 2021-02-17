using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class ColourValidatorTest
    {
        private readonly SectorElementCollection sectorElements;

        public ColourValidatorTest()
        {
            sectorElements = new SectorElementCollection();
            sectorElements.Add(ColourFactory.Make("colour1"));
            sectorElements.Add(ColourFactory.Make("colour2"));
        }

        [Theory]
        [InlineData ("0")]
        [InlineData ("1")]
        [InlineData ("255")]
        [InlineData ("16777214")]
        [InlineData ("16777215")]
        public void TestItPassesOnValidColourIntegers(string value)
        {
            Assert.True(ColourValidator.IsValidColourInteger(value));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("abc")]
        [InlineData("red")]
        [InlineData("123.4")]
        [InlineData("16777215.0")]
        [InlineData("16777216")]
        public void TestItFailsOnInvalidColourIntegers(string value)
        {
            Assert.False(ColourValidator.IsValidColourInteger(value));
        }

        [Theory]
        [InlineData("colour1")]
        [InlineData("colour2")]
        public void TestItPassesIfColourIsDefined(string colour)
        {
            Assert.True(ColourValidator.ColourIsDefined(sectorElements, colour));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("colour1")]
        [InlineData("colour2")]
        public void TestItValidatesColour(string colour)
        {
            Assert.True(ColourValidator.ColourValid(sectorElements, colour));
        }

        [Theory]
        [InlineData("123.4")]
        [InlineData("colour3")]
        [InlineData("-1")]
        public void TestItFailsToValidateColour(string colour)
        {
            Assert.False(ColourValidator.ColourValid(sectorElements, colour));
        }
    }
}