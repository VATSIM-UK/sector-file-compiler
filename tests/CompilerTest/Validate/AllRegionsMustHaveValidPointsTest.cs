using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Validate;
using Moq;
using Compiler.Argument;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllRegionsMustHaveValidPointsTest: AbstractValidatorTestCase
    {
        public AllRegionsMustHaveValidPointsTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            this.sectorElements.Add(RegionFactory.Make(points: new List<Point>() {new("testfix"), new("testndb")}));
            this.sectorElements.Add(RegionFactory.Make(points: new List<Point>() {new("testvor"), new("testairport")}));

            this.AssertNoValidationError();
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            this.sectorElements.Add(RegionFactory.Make(points: new List<Point>() {new("nottestfix"), new("nottestndb")}));
            this.sectorElements.Add(RegionFactory.Make(points: new List<Point>() {new("nottestvor"), new("nottestairport")}));

            this.AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllRegionsMustHaveValidPoints();
        }
    }
}
