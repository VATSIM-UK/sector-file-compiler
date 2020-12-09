using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllFixesMustBeUniqueTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Fix first;
        private readonly Fix second;
        private readonly Fix third;
        private readonly Fix fourth;
        private readonly AllFixesMustBeUnique rule;
        private readonly CompilerArguments args;

        public AllFixesMustBeUniqueTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Fix("DIKAS", new Coordinate("abc", "def"), "test");
            this.second = new Fix("DIKAS", new Coordinate("abd", "cef"), "test");
            this.third = new Fix("DIKAP", new Coordinate("abc", "def"), "test");
            this.fourth = new Fix("DIKAS", new Coordinate("abc", "def"), "test");
            this.rule = new AllFixesMustBeUnique();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfCoordinatesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesIfIdentifiersDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.fourth);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
