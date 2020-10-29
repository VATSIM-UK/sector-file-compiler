using Xunit;
using Moq;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Event;
using Compiler.Error;
using Compiler.Argument;

namespace CompilerTest.Validate
{
    public class AllRunwaysMustReferenceAnAirportTest
    {
        private readonly SectorElementCollection elements;
        private readonly AllRunwayDescriptionsMustReferenceAnAirport validator;
        private readonly Mock<IEventLogger> loggerMock;
        private readonly CompilerArguments args;

        public AllRunwaysMustReferenceAnAirportTest()
        {
            this.elements = new SectorElementCollection();
            this.loggerMock = new Mock<IEventLogger>();
            this.validator = new AllRunwayDescriptionsMustReferenceAnAirport();
            this.args = new CompilerArguments();
            this.elements.Add(
                new Airport("anairport", "EGKK", new Coordinate("a", "b"), "a", "c")
            );

            this.elements.Add(
                new Airport("anotherairport", "EGLL", new Coordinate("a", "b"), "a", "c")
            );
        }

        [Fact]
        public void TestItPassesOnValidReferences()
        {
            this.elements.Add(
                new Runway("27", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGKK anairport", "")
            );
            this.elements.Add(
                new Runway("27", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGLL anotherairport", "")
            );
            this.elements.Add(
                new Runway("00", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "000A Show adjacent departure airports", "")
            );

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Never);
        }

        [Fact]
        public void TestItFailsOnInvalidReferences()
        {
            this.elements.Add(
                new Runway("27", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "EGKK - anairport", "")
            );
            this.elements.Add(
                new Runway("27", 270, new Coordinate("abc", "def"), "09", 90, new Coordinate("abc", "def"), "notanotherairport", "")
            );

            this.validator.Validate(this.elements, this.args, this.loggerMock.Object);
            this.loggerMock.Verify(foo => foo.AddEvent(It.IsAny<ValidationRuleFailure>()), Times.Exactly(2));
        }
    }
}
