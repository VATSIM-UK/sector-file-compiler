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
    public class AllGeoMustHaveValidPointsTest: AbstractValidatorTestCase
    {
        public AllGeoMustHaveValidPointsTest()
        {
            this.sectorElements.Add(FixFactory.Make("testfix"));
            this.sectorElements.Add(VorFactory.Make("testvor"));
            this.sectorElements.Add(NdbFactory.Make("testndb"));
            this.sectorElements.Add(AirportFactory.Make("testairport"));
        }

        [Fact]
        public void TestItPassesOnValidPoints()
        {
            this.sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testndb"),
                secondPoint: new Point("testairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testndb"), secondPoint: new Point("testairport")),
                }
            ));
            
            this.sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testfix"),
                secondPoint: new Point("testvor"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testfix"), secondPoint: new Point("testvor")),
                }
            ));

            this.AssertNoValidationErrors();
        }

        [Fact]
        public void TestItFailsOnInvalidPoint()
        {
            this.sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("nottestndb"),
                secondPoint: new Point("testairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("nottestvor"), secondPoint: new Point("testfix")),
                }
            ));
            
            this.sectorElements.Add(GeoFactory.Make(
                firstPoint: new Point("testndb"),
                secondPoint: new Point("nottestairport"),
                additionalSegments: new List<GeoSegment>
                {
                    GeoSegmentFactory.Make(firstPoint: new Point("testvor"), secondPoint: new Point("nottestfix")),
                }
            ));
            
            this.AssertValidationErrors(4);
        }

        protected override IValidationRule GetValidationRule()
        {
            return new AllGeoMustHaveValidPoints();
        }
    }
}
