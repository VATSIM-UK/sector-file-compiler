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
    public class AllGeoMustHaveValidColoursTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Colour definedColour;
        private readonly AllGeoMustHaveValidColours rule;
        private readonly CompilerArguments args;
        private readonly List<RouteSegment> segments1;
        private readonly List<RouteSegment> segments2;

        public AllGeoMustHaveValidColoursTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.definedColour = new Colour("colour1", -1, "test");
            this.rule = new AllGeoMustHaveValidColours();
            this.args = new CompilerArguments();
            this.segments1 = new List<RouteSegment>();
            this.segments2 = new List<RouteSegment>();
            this.sectorElements.Add(this.definedColour);
        }

        [Fact]
        public void TestItPassesOnValidColoursIntegers()
        {
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "0", "test"));
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "colour1", "test"));
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "notcolour1", "test"));
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "notcolour1", "test"));
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "colour1", "test"));
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            this.sectorElements.Add(new Geo(new Point("test"), new Point("test"), "123456789", "test"));

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
