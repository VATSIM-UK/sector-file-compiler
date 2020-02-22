using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using System.Collections.Generic;

namespace CompilerTest.Validate
{
    public class AllRegionsMustHaveAValidColourTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour definedColour;
        private readonly AllRegionsMustHaveValidColours rule;
        private readonly CompilerArguments args;

        public AllRegionsMustHaveAValidColourTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.definedColour = new Colour("colour1", -1, "test");
            this.rule = new AllRegionsMustHaveValidColours();
            this.args = new CompilerArguments();
            this.sectorElements.Add(this.definedColour);
        }

        [Fact]
        public void TestItPassesOnValidColours()
        {
            this.sectorElements.Add(new Region("colour1", new List<Point>(), "comment"));
            this.sectorElements.Add(new Region("123", new List<Point>(), "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidColours()
        {
            this.sectorElements.Add(new Region("colour2", new List<Point>(), "comment"));
            this.sectorElements.Add(new Region("-123", new List<Point>(), "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
