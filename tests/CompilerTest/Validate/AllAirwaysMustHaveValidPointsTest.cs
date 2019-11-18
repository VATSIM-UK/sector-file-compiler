using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllAirwaysMustHaveValidPointsTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllAirwaysMustHaveValidPoints rule;

        public AllAirwaysMustHaveValidPointsTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllAirwaysMustHaveValidPoints();
        }

        private Airway GetAirway(AirwayType type, string startPointIdentifier, string endPointIdentifier)
        {
            return new Airway("test", type, new Point(startPointIdentifier), new Point(endPointIdentifier), "");
        }

        [Fact]
        public void TestItPassesOnValidPointLow()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.LOW, "testfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.LOW, "nottestfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.LOW, "testfix", "nottestvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.HIGH, "testfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.HIGH, "nottestfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            this.sectorElements.Add(this.GetAirway(AirwayType.HIGH, "testfix", "nottestvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
