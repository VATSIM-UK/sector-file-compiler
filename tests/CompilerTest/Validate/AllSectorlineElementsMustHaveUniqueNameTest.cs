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
            sectorElements = new SectorElementCollection();
            loggerMock = new Mock<IEventLogger>();
            first = new Sectorline(
                "ONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            second = new Sectorline(
                "ONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            third = new Sectorline(
                "NOTONE",
                SectorLineDisplayRuleFactory.MakeList(2),
                SectorlineCoordinateFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            fourth = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(3),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            fifth = new CircleSectorline(
                "NOTONEORTWO",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            sixth = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                SectorLineDisplayRuleFactory.MakeList(2),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
            rule = new AllSectorlineElementsMustHaveUniqueName();
            args = new CompilerArguments();
        }

        [Fact]
        public void TestItPassesIfAllNamesDifferent()
        {
            sectorElements.Add(first);
            sectorElements.Add(third);
            sectorElements.Add(fifth);
            rule.Validate(sectorElements, args, loggerMock.Object);

            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnNameClashTwoSectorlines()
        {
            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            rule.Validate(sectorElements, args, loggerMock.Object);

            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnNameClashTwoCircleSectorlines()
        {
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);
            sectorElements.Add(sixth);
            rule.Validate(sectorElements, args, loggerMock.Object);

            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }

        [Fact]
        public void TestItFailsOnNameClashMixed()
        {
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);
            rule.Validate(sectorElements, args, loggerMock.Object);

            loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
