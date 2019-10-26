using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Model;
using Compiler.Event;
using Compiler.Validate;
using Moq;

namespace CompilerTest.Validate
{
    public class AllSidsMustBeUniqueTest
    {
        private SectorElementCollection sectorElements;
        private Mock<IEventLogger> loggerMock;
        private SidStar first;
        private SidStar second;
        private SidStar third;
        private AllSidsMustBeUnique rule;

        public AllSidsMustBeUniqueTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.first = new SidStar("SID", "EGKK", "26L", "ADMAG2X", new List<string>());
            this.second = new SidStar("STAR", "EGKK", "26L", "ADMAG2X", new List<string>());
            this.third = new SidStar("SID", "EGKK", "26L", "ADMAG2X", new List<string>());
            this.rule = new AllSidsMustBeUnique();
        }

        [Fact]
        public void TestItPassesIfNoDuplicates()
        {
            this.sectorElements.AddSidStar(this.first);
            this.sectorElements.AddSidStar(this.second);
            this.rule.Validate(sectorElements, this.loggerMock.Object);

            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ICompilerEvent>()), Times.Never);

        }

        [Fact]
        public void TestItFailsIfThereAreDuplicates()
        {
            this.sectorElements.AddSidStar(this.first);
            this.sectorElements.AddSidStar(this.second);
            this.sectorElements.AddSidStar(this.third);
            this.rule.Validate(sectorElements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ICompilerEvent>()), Times.Once);
        }
    }
}
