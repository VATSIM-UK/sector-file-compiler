using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRegionsMustHaveValidPointsTest: AbstractValidatorTestCase
    {
        public AllRegionsMustHaveValidPointsTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            sectorElements.Add(RegionFactory.Make(points: new List<Point> {new("testfix"), new("testndb")}));
            sectorElements.Add(RegionFactory.Make(points: new List<Point> {new("testvor"), new("testairport")}));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            sectorElements.Add(RegionFactory.Make(points: new List<Point> {new("nottestfix"), new("nottestndb")}));
            sectorElements.Add(RegionFactory.Make(points: new List<Point> {new("nottestvor"), new("nottestairport")}));

            AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRegionsMustHaveValidPoints();
        }
    }
}
