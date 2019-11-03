using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllColoursMustBeValidTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour first;
        private readonly Colour second;
        private readonly Colour third;
        private readonly Colour fourth;
        private readonly Colour fifth;
        private readonly AllColoursMustBeValid rule;

        public AllColoursMustBeValidTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Colour("colour1", -1);
            this.second = new Colour("colour2", 0);
            this.third = new Colour("colour3", 255);
            this.fourth = new Colour("colour4", 16777215);
            this.fifth = new Colour("colour5", 16777216);
            this.rule = new AllColoursMustBeValid();
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.AddColour(this.second);
            this.sectorElements.AddColour(this.third);
            this.sectorElements.AddColour(this.fourth);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNegativeValues()
        {
            this.sectorElements.AddColour(this.first);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnValuesOverMaximum()
        {
            this.sectorElements.AddColour(this.fifth);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
