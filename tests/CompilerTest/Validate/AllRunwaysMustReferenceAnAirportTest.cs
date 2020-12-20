using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRunwaysMustReferenceAnAirportTest
    {
        private readonly SectorElementCollection elements;
        private readonly AllRunwaysMustReferenceAnAirport validator;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly CompilerArguments args;

        public AllRunwaysMustReferenceAnAirportTest()
        {
            this.elements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new AllRunwaysMustReferenceAnAirport();
            this.args = new CompilerArguments();
            this.elements.Add(AirportFactory.Make());
            this.elements.Add(AirportFactory.Make());
        }

        [Fact]
        public void TestItPassesOnValidReferences()
        {
            this.elements.Add(RunwayFactory.Make("EGKK"));
            this.elements.Add(RunwayFactory.Make("EGLL"));
            this.elements.Add(RunwayFactory.Make("000A"));
            
            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidReferences()
        {
            this.elements.Add(RunwayFactory.Make("EGSS"));
            this.elements.Add(RunwayFactory.Make("EGGW"));
            this.elements.Add(RunwayFactory.Make("000A"));

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
