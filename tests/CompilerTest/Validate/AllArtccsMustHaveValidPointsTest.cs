using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllArtccsMustHaveValidPointsTest: AbstractValidatorTestCase
    {
        public AllArtccsMustHaveValidPointsTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        private static ArtccSegment GetArtcc(ArtccType type, string startPointIdentifier, string endPointIdentifier)
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
        public void TestItPassesOnValidPointRegular()
        {
            sectorElements.Add(GetArtcc(ArtccType.REGULAR, "testfix", "testvor"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointRegular()
        {
            sectorElements.Add(GetArtcc(ArtccType.REGULAR, "nottestfix", "testvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointRegular()
        {
            sectorElements.Add(GetArtcc(ArtccType.REGULAR, "testfix", "nottestvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointLow()
        {
            sectorElements.Add(GetArtcc(ArtccType.LOW, "testfix", "testvor"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            sectorElements.Add(GetArtcc(ArtccType.LOW, "nottestfix", "testvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            sectorElements.Add(GetArtcc(ArtccType.LOW, "testfix", "nottestvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            sectorElements.Add(GetArtcc(ArtccType.HIGH, "testfix", "testvor"));
            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            sectorElements.Add(GetArtcc(ArtccType.HIGH, "nottestfix", "testvor"));
            AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            sectorElements.Add(GetArtcc(ArtccType.HIGH, "testfix", "nottestvor"));
            AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllArtccsMustHaveValidPoints();
        }
    }
}
