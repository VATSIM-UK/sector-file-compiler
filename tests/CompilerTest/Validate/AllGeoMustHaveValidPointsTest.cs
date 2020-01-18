using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllGeoMustHaveValidPointsTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllGeoMustHaveValidPoints rule;
        private readonly CompilerArguments args;

        public AllGeoMustHaveValidPointsTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));
            this.rule = new AllGeoMustHaveValidPoints();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            Geo geo1 = new Geo(new Point("testfix"), new Point("testvor"), "test", "comment");
            Geo geo2= new Geo(new Point("testndb"), new Point("testairport"), "test", "comment");

            this.sectorElements.Add(geo1);
            this.sectorElements.Add(geo2);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            Geo geo1 = new Geo(new Point("nottestfix"), new Point("testvor"), "test", "comment");
            Geo geo2 = new Geo(new Point("testndb"), new Point("nottestairport"), "test", "comment");

            this.sectorElements.Add(geo1);
            this.sectorElements.Add(geo2);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
