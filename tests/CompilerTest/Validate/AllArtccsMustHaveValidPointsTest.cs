using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllArtccsMustHaveValidPointsTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllArtccsMustHaveValidPoints rule;

        public AllArtccsMustHaveValidPointsTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();

            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));

            this.rule = new AllArtccsMustHaveValidPoints();
        }

        private Artcc GetArtcc(ArtccType type, string startPointIdentifier, string endPointIdentifier)
        {
            return new Artcc("test", type, new Point(startPointIdentifier), new Point(endPointIdentifier), "");
        }

        [Fact]
        public void TestItPassesOnValidPointRegular()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.REGULAR, "testfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointRegular()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.REGULAR, "nottestfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointRegular()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.REGULAR, "testfix", "nottestvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItPassesOnValidPointLow()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.LOW, "testfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.LOW, "nottestfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.LOW, "testfix", "nottestvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.HIGH, "testfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.HIGH, "nottestfix", "testvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            this.sectorElements.Add(this.GetArtcc(ArtccType.HIGH, "testfix", "nottestvor"));
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
