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

        protected void AssertNoValidationErrors()
        {
            AssertValidationErrors(0);
        }

        protected void AssertValidationErrors(int count = 1)
        {
            GetValidationRule().Validate(sectorElements, args, loggerMock.Object);
            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(count));
        }

        protected abstract IValidationRule GetValidationRule();
    }
}
