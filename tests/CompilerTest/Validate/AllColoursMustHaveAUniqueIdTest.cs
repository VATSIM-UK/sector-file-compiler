using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllColoursMustHaveAUniqueIdTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour first;
        private readonly Colour second;
        private readonly Colour third;
        private readonly AllColoursMustHaveAUniqueId rule;

        public AllColoursMustHaveAUniqueIdTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Colour("colour1", 123);
            this.second = new Colour("colour2", 123);
            this.third = new Colour("colour1", 123);
            this.rule = new AllColoursMustHaveAUniqueId();
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            this.sectorElements.AddColour(this.first);
            this.sectorElements.AddColour(this.second);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.AddColour(this.first);
            this.sectorElements.AddColour(this.second);
            this.sectorElements.AddColour(this.third);
            this.rule.Validate(sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
