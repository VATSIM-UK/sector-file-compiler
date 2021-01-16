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
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
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
            this.sectorElements.Add(GetArtcc(ArtccType.REGULAR, "testfix", "testvor"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointRegular()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.REGULAR, "nottestfix", "testvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointRegular()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.REGULAR, "testfix", "nottestvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointLow()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.LOW, "testfix", "testvor"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointLow()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.LOW, "nottestfix", "testvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointLow()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.LOW, "testfix", "nottestvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItPassesOnValidPointHigh()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.HIGH, "testfix", "testvor"));
            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidStartPointHigh()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.HIGH, "nottestfix", "testvor"));
            this.AssertValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidEndPointHigh()
        {
            this.sectorElements.Add(GetArtcc(ArtccType.HIGH, "testfix", "nottestvor"));
            this.AssertValidationErrors();
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllArtccsMustHaveValidPoints();
        }
    }
}
