using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;

namespace CompilerTest.Validate
{
    public class AllSidsMustHaveAValidAirportTest
    {
        private readonly Airport airfield1;
        private readonly Airport airfield2;
        private readonly Airport airfield3;
        private readonly SidStar sid1;
        private readonly SidStar sid2;
        private readonly SidStar sid3;
        private readonly SectorElementCollection elements;
        private readonly AllSidsMustHaveAValidAirport validator;
        private readonly Mock<IEventLogger> loggerMock;

        public AllSidsMustHaveAValidAirportTest()
        {
            this.elements = new SectorElementCollection();
            this.airfield1 = new Airport("a", "EGKK", new Coordinate("a", "b"), "a", "c");
            this.airfield2 = new Airport("a", "EGLL", new Coordinate("a", "b"), "a", "c");
            this.airfield3 = new Airport("a", "EGCC", new Coordinate("a", "b"), "a", "c");
            this.sid1 = new SidStar("SID", "EGKK", "26L", "ADMAG2X", new List<string>(), "test");
            this.sid2 = new SidStar("STAR", "EGCC", "23R", "SONEX1X", new List<string>(), "test");
            this.sid3 = new SidStar("SID", "EGBB", "33", "WHI4D", new List<string>(), "test");
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new AllSidsMustHaveAValidAirport();

            this.elements.Add(airfield1);
            this.elements.Add(airfield2);
            this.elements.Add(airfield3);
        }

        [Fact]
        public void TestItPassesOnAllValid()
        {
            this.elements.Add(sid1);
            this.elements.Add(sid2);

            this.validator.Validate(this.elements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnDuplicates()
        {
            this.elements.Add(sid1);
            this.elements.Add(sid2);
            this.elements.Add(sid3);

            this.validator.Validate(this.elements, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Once);
        }
    }
}
