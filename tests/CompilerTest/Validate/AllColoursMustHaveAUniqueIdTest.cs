using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

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
        private readonly CompilerArguments args;

        public AllColoursMustHaveAUniqueIdTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Colour("colour1", 123, "test");
            this.second = new Colour("colour2", 123, "test");
            this.third = new Colour("colour1", 123, "test");
            this.rule = new AllColoursMustHaveAUniqueId();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
