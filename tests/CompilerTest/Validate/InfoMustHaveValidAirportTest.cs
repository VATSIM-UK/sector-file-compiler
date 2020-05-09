using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class InfoMustHaveValidAirportTest
    {
        private readonly SectorElementCollection elements;
        private readonly InfoMustHaveValidAirport validator;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly CompilerArguments args;

        public InfoMustHaveValidAirportTest()
        {
            this.elements = new SectorElementCollection();
            this.elements.Add(new Airport("a", "EGKK", new Coordinate("a", "b"), "a", "c"));
            this.elements.Add(new Airport("a", "EGLL", new Coordinate("a", "b"), "a", "c"));
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new InfoMustHaveValidAirport();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesOnValidAirport()
        {
            this.elements.Add(new Info(
                "Test",
                "LON_CTR",
                "EGLL",
                new Coordinate("abc", "def"),
                0,
                0,
                0,
                0
            ));

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidAirport()
        {
            this.elements.Add(new Info(
                "Test",
                "LON_CTR",
                "EGXX",
                new Coordinate("abc", "def"),
                0,
                0,
                0,
                0
            ));

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
