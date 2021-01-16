using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllSectorlineElementsMustHaveUniqueNameTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly Sectorline first;
        private readonly Sectorline second;
        private readonly Sectorline third;
        private readonly CircleSectorline fourth;
        private readonly CircleSectorline fifth;
        private readonly CircleSectorline sixth;
        private readonly AllSectorlineElementsMustHaveUniqueName rule;
        private readonly CompilerArguments args;

        public AllSectorlineElementsMustHaveUniqueNameTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new Sectorline(
                "ONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.second = new Sectorline(
                "ONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.third = new Sectorline(
                "NOTONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.fourth = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(3),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.fifth = new CircleSectorline(
                "NOTONEORTWO",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.sixth = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            this.rule = new AllSectorlineElementsMustHaveUniqueName();
            this.args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.third);
            this.sectorElements.Add(this.fifth);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNameClashTwoSectorlines()
        {
            this.sectorElements.Add(this.first);
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnNameClashTwoCircleSectorlines()
        {
            this.sectorElements.Add(this.fourth);
            this.sectorElements.Add(this.fifth);
            this.sectorElements.Add(this.sixth);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnNameClashMixed()
        {
            this.sectorElements.Add(this.second);
            this.sectorElements.Add(this.third);
            this.sectorElements.Add(this.fourth);
            this.sectorElements.Add(this.fifth);
            this.rule.Validate(sectorElements, this.args, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
