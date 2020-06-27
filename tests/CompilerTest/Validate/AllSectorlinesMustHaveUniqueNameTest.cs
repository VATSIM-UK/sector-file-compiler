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
    public class AllSectorlinesMustHaveUniqueNameTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Sectorline first;
        private readonly Sectorline second;
        private readonly Sectorline third;
        private readonly AllSectorlinesMustHaveUniqueName rule;
        private readonly CompilerArguments args;

        public AllSectorlinesMustHaveUniqueNameTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Sectorline(
                "ONE",
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                new List<SectorlineCoordinate>
                {
                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                },
                "commentname"
            );
            this.second = new Sectorline(
                "ONE",
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                new List<SectorlineCoordinate>
                {
                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                },
                "commentname"
            );
            this.third = new Sectorline(
                "NOTONE",
                new List<SectorlineDisplayRule>
                {
                    new SectorlineDisplayRule("TEST1", "TEST1", "TEST2", "comment1"),
                    new SectorlineDisplayRule("TEST2", "TEST2", "TEST1", "comment2")
                },
                new List<SectorlineCoordinate>
                {
                    new SectorlineCoordinate(new Coordinate("abc", "def"), "comment3"),
                    new SectorlineCoordinate(new Coordinate("ghi", "jkl"), "comment4"),
                },
                "commentname"
            );
            this.rule = new AllSectorlinesMustHaveUniqueName();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNameClash()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
