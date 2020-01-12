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
    public class AllSctStarsMustHaveValidColoursTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour definedColour;
        private readonly AllSctStarsMustHaveValidColours rule;
        private readonly CompilerArguments args;
        private readonly List<RouteSegment> segments1;
        private readonly List<RouteSegment> segments2;

        public AllSctStarsMustHaveValidColoursTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.definedColour = new Colour("colour1", -1, "test");
            this.rule = new AllSctStarsMustHaveValidColours();
            this.args = new CompilerArguments();
            this.segments1 = new List<RouteSegment>();
            this.segments2 = new List<RouteSegment>();
            this.sectorElements.Add(this.definedColour);
            this.sectorElements.Add(new SidStarRoute(SidStarType.STAR, "Test", this.segments1));
            this.sectorElements.Add(new SidStarRoute(SidStarType.STAR, "Test", this.segments2));
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "255", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "266", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "266", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnNullColours()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), null, "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), null, "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), null, "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour2", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour1", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "colour2", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "255", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "123456789", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "255", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegersAfterLooping()
        {
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "255", "comment"));
            this.segments1.Add(new RouteSegment(new Point("abc"), new Point("def"), "255", "comment"));
            this.segments2.Add(new RouteSegment(new Point("abc"), new Point("def"), "123456789", "comment"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
