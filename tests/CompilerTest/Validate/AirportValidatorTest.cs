using Xunit;
using Compiler.Validate;

namespace CompilerTest.Validate
{
    public class AirportValidatorTest
    {
        [Theory]
        [InlineData("000A")]
        [InlineData("LHR")]
        [InlineData("EGLLL")]
        [InlineData("egll")]
        [InlineData("*")]
        [InlineData("")]
        public void TestValidationFailureIcaoCode(string input)
        {
            Assert.False(AirportValidator.IcaoValid(input));
        }

        [Theory]
        [InlineData("EGLL")]
        [InlineData("KJFK")]
        [InlineData("LXGB")]
        public void TestValidationSuccessIcaoCode(string input)
        {
            Assert.True(AirportValidator.IcaoValid(input));
        }

        [Theory]
        [InlineData("LHR")]
        [InlineData("EGLLL")]
        [InlineData("")]
        [InlineData("egll")]
        [InlineData("*")]
        public void TestValidationFailureEuroscope(string input)
        {
            Assert.False(AirportValidator.ValidEuroscopeAirport(input));
        }

        [Theory]
        [InlineData("000A")]
        [InlineData("EGLL")]
        [InlineData("KJFK")]
        [InlineData("LXGB")]
        public void TestValidationSuccessEuroscope(string input)
        {
            Assert.True(AirportValidator.ValidEuroscopeAirport(input));
        }

        [Theory]
        [InlineData("000A")]
        [InlineData("LHR")]
        [InlineData("EGLLL")]
        [InlineData("egll")]
        [InlineData("")]
        public void TestValidationFailureSectorGuest(string input)
        {
            Assert.False(AirportValidator.ValidSectorGuestAirport(input));
        }

        [Theory]
        [InlineData("EGLL")]
        [InlineData("KJFK")]
        [InlineData("LXGB")]
        [InlineData("*")]
        public void TestValidationSuccessSectorGuest(string input)
        {
            Assert.True(AirportValidator.ValidSectorGuestAirport(input));
        }
    }
}
