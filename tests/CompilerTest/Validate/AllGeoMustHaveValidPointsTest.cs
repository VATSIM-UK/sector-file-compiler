using System.Collections.Generic;
using Xunit;
using Compiler.Model;
using Compiler.Validate;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Validate
{
    public class AllGeoMustHaveValidPointsTest: AbstractValidatorTestCase
    {
        public AllGeoMustHaveValidPointsTest()
        {
            sectorElements.Add(FixFactory.Make("testfix"));
            sectorElements.Add(VorFactory.Make("testvor"));
            sectorElements.Add(NdbFactory.Make("testndb"));
            sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testndb"),
                secondPoint: new Point("testairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testndb"), secondPoint: new Point("testairport")),
                }
            ));
            
            sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testfix"),
                secondPoint: new Point("testvor"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testfix"), secondPoint: new Point("testvor")),
                }
            ));

            AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("nottestndb"),
                secondPoint: new Point("testairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("nottestvor"), secondPoint: new Point("testfix")),
                }
            ));
            
            sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testndb"),
                secondPoint: new Point("nottestairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testvor"), secondPoint: new Point("nottestfix")),
                }
            ));
            
            AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllGeoMustHaveValidPoints();
        }
    }
}
