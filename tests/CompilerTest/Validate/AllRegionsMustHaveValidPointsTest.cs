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
    public class AllRegionsMustHaveValidPointsTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly AllRegionsMustHaveValidPoints rule;
        private readonly CompilerArguments args;

        public AllRegionsMustHaveValidPointsTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.sectorElements.Add(new Fix("testfix", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Vor("testvor", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Ndb("testndb", "123.456", new Coordinate("abc", "def"), "test"));
            this.sectorElements.Add(new Airport("testairport", "testairport", new Coordinate("abc", "def"), "123.456", "test"));
            this.rule = new AllRegionsMustHaveValidPoints();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            List<Point> points1 = new List<Point>();
            points1.Add(new Point("testfix"));
            points1.Add(new Point("testndb"));

            List<Point> points2 = new List<Point>();
            points2.Add(new Point("testvor"));
            points2.Add(new Point("testairport"));

            Region region1 = new Region("Red", points1, "comment");
            Region region2 = new Region("Red", points2, "comment");

            this.sectorElements.Add(region1);
            this.sectorElements.Add(region2);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            List<Point> points1 = new List<Point>();
            points1.Add(new Point("nottestfix"));
            points1.Add(new Point("nottestndb"));

            List<Point> points2 = new List<Point>();
            points2.Add(new Point("nottestvor"));
            points2.Add(new Point("nottestairport"));

            Region region1 = new Region("Red", points1, "comment");
            Region region2 = new Region("Red", points2, "comment");

            this.sectorElements.Add(region1);
            this.sectorElements.Add(region2);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(4));
        }
    }
}
