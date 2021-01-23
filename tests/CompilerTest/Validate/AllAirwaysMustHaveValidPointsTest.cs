using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllAirwaysMustHaveValidPointsTest: AbstractValidatorTestCase
    {

        public AllAirwaysMustHaveValidPointsTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        private static AirwaySegment GetAirway(AirwayType type, string startPointIdentifier, string endPointIdentifier)
        {
            return new(
                "test",
                type,
                new Point(startPointIdentifier),
                new Point(endPointIdentifier),
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
            );
        }

        [Fact]
        public void TestItPassesOnValidPointLow()
        {
            this.sectorElements.Add(GetAirway(AirwayType.LOW, "testfix", "testvor"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            this.sectorElements.Add(GetAirway(AirwayType.LOW, "nottestfix", "testvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            this.sectorElements.Add(GetAirway(AirwayType.LOW, "testfix", "nottestvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            this.sectorElements.Add(GetAirway(AirwayType.HIGH, "testfix", "testvor"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            this.sectorElements.Add(GetAirway(AirwayType.HIGH, "nottestfix", "testvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            this.sectorElements.Add(GetAirway(AirwayType.HIGH, "testfix", "nottestvor"));
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllAirwaysMustHaveValidPoints();
        }
    }
}
