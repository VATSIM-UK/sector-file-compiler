using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;
using Moq;

namespace CompilerTest.Validate
{
    public class ColourValidatorTest
    {
        private readonly SectorElementCollection sectorElements;

        public ColourValidatorTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.sectorElements.Add(ColourFactory.Make("colour1"));
            this.sectorElements.Add(ColourFactory.Make("colour2"));
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
            Assert.True(ColourValidator.ColourIsDefined(this.sectorElements, colour));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("colour1")]
        [InlineData("colour2")]
        public void TestItValidatesColour(string colour)
        {
            Assert.True(ColourValidator.ColourValid(this.sectorElements, colour));
        }

        [Theory]
        [InlineData("123.4")]
        [InlineData("colour3")]
        [InlineData("-1")]
        public void TestItFailsToValidateColour(string colour)
        {
            Assert.False(ColourValidator.ColourValid(this.sectorElements, colour));
        }
    }
}