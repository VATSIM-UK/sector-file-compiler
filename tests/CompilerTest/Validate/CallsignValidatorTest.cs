using Xunit;
using Compiler.Validate;

namespace CompilerTest.Validate
{
    public class CallsignValidatorTest
    {
        [Theory]
        [InlineData("LONCTR")]
        [InlineData("LON_SCT_CTR")]
        [InlineData("A_B_C_CTR")]
        [InlineData("A-B-C-CTR")]
        [InlineData("A_B-C-CTR")]
        [InlineData("LON_S_PLN")]
        [InlineData("ASDSADSADASDSADASD")]
        public void TestValidationFailure(string input)
        {
            Assert.False(CallsignValidator.Validate(input));
        }

        [Theory]
        [InlineData("LON_C_CTR")]
        [InlineData("LON_CTR")]
        [InlineData("EGGD_APP")]
        [InlineData("LXGB_R_APP")]
        [InlineData("LTC-CTR")]
        [InlineData("EGKK-F-APP")]
        [InlineData("EGKK-T_APP")]
        [InlineData("EGKK_T-APP")]
        [InlineData("ESSEX_APP")]
        [InlineData("THAMES-APP")]
        [InlineData("EGXX_FSS")]
        [InlineData("LON_SC_CTR")]
        [InlineData("EGSS_DEL")]
        [InlineData("EGLC_GND")]
        [InlineData("EGTT_INFO")]
        [InlineData("AF_OBS")]
        public void TestValidationSuccess(string input)
        {
            Assert.True(CallsignValidator.Validate(input));
        }
    }
}
