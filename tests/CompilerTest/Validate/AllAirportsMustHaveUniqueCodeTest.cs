using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;

namespace CompilerTest.Validate
{
    public class AllAirportsMustHaveUniqueCodeTest
    {
        private readonly Airport airfield1;
        private readonly Airport airfield2;
        private readonly Airport airfield3;
        private readonly SectorElementCollection elements;
        private readonly AllAirportsMustHaveUniqueCode validator;
        private readonly Mock<IEventLogger> loggerMock;

        public AllAirportsMustHaveUniqueCodeTest()
        {
            this.elements = new SectorElementCollection();
            this.airfield1 = new Airport("a", "EGKK", new Coordinate("a", "b"), "a", "c");
            this.airfield2 = new Airport("a", "EGLL", new Coordinate("a", "b"), "a", "c");
            this.airfield3 = new Airport("a", "EGKK", new Coordinate("a", "b"), "a", "c");
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new AllAirportsMustHaveUniqueCode();
        }

        [Fact]
        public void TestItPassesOnNoDuplicates()
        {
            this.elements.Add(airfield1);
            this.elements.Add(airfield2);

            this.validator.Validate(this.elements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            this.elements.Add(airfield1);
            this.elements.Add(airfield2);
            this.elements.Add(airfield3);

            this.validator.Validate(this.elements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
