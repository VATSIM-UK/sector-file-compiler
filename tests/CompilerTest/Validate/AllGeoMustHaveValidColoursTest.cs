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
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "0", "comment")
                }
            );
            this.sectorElements.Add(geo);
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItPassesOnValidDefinedColours()
        {
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "colour1", "comment")
                }
            );
            this.sectorElements.Add(geo);
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColours()
        {
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "notcolour1", "comment")
                }
            );
            this.sectorElements.Add(geo);
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidDefinedColoursAfterLooping()
        {
            Geo geo1 = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "colour1", "comment")
                }
            );
            Geo geo2 = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "notcolour1", "comment")
                }
            );
            this.sectorElements.Add(geo2);
            this.sectorElements.Add(geo1);
            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidColourIntegers()
        {
            Geo geo = new Geo(
                "TestGeo",
                new List<GeoSegment>
                {
                    new GeoSegment(new Point("test"), new Point("test"), "123456789", "comment")
                }
            );
            this.sectorElements.Add(geo);

            this.rule.Validate(this.sectorElements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
