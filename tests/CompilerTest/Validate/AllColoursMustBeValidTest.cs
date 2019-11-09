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
            this.first = new Colour("colour1", -1, "test");
            this.second = new Colour("colour2", 0, "test");
            this.third = new Colour("colour3", 255, "test");
            this.fourth = new Colour("colour4", 16777215, "test");
            this.fifth = new Colour("colour5", 16777216, "test");
            this.rule = new AllColoursMustBeValid();
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.sectorElements.Add(this.fourth);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNegativeValues()
        {
            this.sectorElements.Add(this.first);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnValuesOverMaximum()
        {
            this.sectorElements.Add(this.fifth);
            this.rule.Validate(this.sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
