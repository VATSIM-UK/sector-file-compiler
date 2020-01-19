using Xunit;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllLabelsMustHaveAValidColourTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour definedColour;
        private readonly AllLabelsMustHaveAValidColour rule;
        private readonly CompilerArguments args;

        public AllLabelsMustHaveAValidColourTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.definedColour = new Colour("colour1", -1, "test");
            this.rule = new AllLabelsMustHaveAValidColour();
            this.args = new CompilerArguments();
            this.sectorElements.Add(this.definedColour);
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Add(new Label("test", new Coordinate("abc", "def"), "colour1", "comment"));
            this.sectorElements.Add(new Label("test", new Coordinate("abc", "def"), "123", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            this.sectorElements.Add(new Label("test", new Coordinate("abc", "def"), "colour2", "comment"));
            this.sectorElements.Add(new Label("test", new Coordinate("abc", "def"), "-123", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
