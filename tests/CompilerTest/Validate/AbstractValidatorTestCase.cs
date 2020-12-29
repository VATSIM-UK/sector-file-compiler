using Compiler.Argument;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public abstract class AbstractValidatorTestCase
    {
        protected SectorElementCollection sectorElements = new();

        protected Mock<IEventLogger> loggerMock = new();

        protected CompilerArguments args = new();

        protected void AssertNoValidationError()
        {
            this.AssertValidationErrors(0);
        }

        protected void AssertValidationErrors(int count = 1)
        {
            this.GetValidationRule().Validate(sectorElements, args, loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(count));
        }

        protected abstract IValidationRule GetValidationRule();
    }
}
