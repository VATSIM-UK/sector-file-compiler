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
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
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
            sectorElements.Add(GetAirway(AirwayType.LOW, "testfix", "testvor"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            sectorElements.Add(GetAirway(AirwayType.LOW, "nottestfix", "testvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            sectorElements.Add(GetAirway(AirwayType.LOW, "testfix", "nottestvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            sectorElements.Add(GetAirway(AirwayType.HIGH, "testfix", "testvor"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            sectorElements.Add(GetAirway(AirwayType.HIGH, "nottestfix", "testvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            sectorElements.Add(GetAirway(AirwayType.HIGH, "testfix", "nottestvor"));
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllAirwaysMustHaveValidPoints();
        }
    }
}
