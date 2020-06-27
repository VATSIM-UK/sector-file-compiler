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
    public class AllCircleSectorlinesMustHaveUniqueNameTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly CircleSectorline first;
        private readonly CircleSectorline second;
        private readonly CircleSectorline third;
        private readonly AllCircleSectorlinesMustHaveUniqueName rule;
        private readonly CompilerArguments args;

        public AllCircleSectorlinesMustHaveUniqueNameTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                new List<SectorlineDisplayRule>(),
                "commentname"
            );
            this.second = new CircleSectorline(
                "ONE",
                "EGGD",
                5.5,
                new List<SectorlineDisplayRule>(),
                "commentname"
            );
            this.third = new CircleSectorline(
                "NOTONE",
                "EGGD",
                5.5,
                new List<SectorlineDisplayRule>(),
                "commentname"
            );
            this.rule = new AllCircleSectorlinesMustHaveUniqueName();
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
