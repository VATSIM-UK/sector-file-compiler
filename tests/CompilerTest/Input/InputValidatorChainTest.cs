using Xunit;
using Moq;
using Compiler.Input;
using System.Collections.Generic;

namespace CompilerTest.Input
{
    public class InputValidatorChainTest
    {
        [Fact]
        public void ItReturnsTrueIfAllValidatorsPass()
        {
            var mock1 = new Mock<InputValidator>();
            mock1.Setup(validator => validator.Validate(1))
                .Returns(true);

            var mock2 = new Mock<InputValidator>();
            mock2.Setup(validator => validator.Validate(1))
                .Returns(true);

            InputValidatorChain chain = new InputValidatorChain(
                new List<InputValidator>(new InputValidator[] { mock1.Object, mock2.Object })
            );

            Assert.True(chain.Validate(1));
        }

        [Fact]
        public void ItReturnsFalseIfAValidatorFails()
        {
            var mock1 = new Mock<InputValidator>();
            mock1.Setup(validator => validator.Validate(1))
                .Returns(true);

            var mock2 = new Mock<InputValidator>();
            mock2.Setup(validator => validator.Validate(1))
                .Returns(false);

            InputValidatorChain chain = new InputValidatorChain(
                new List<InputValidator>(new InputValidator[] { mock1.Object, mock2.Object })
            );

            Assert.False(chain.Validate(1));
        }
    }
}
