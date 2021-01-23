using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class HighAirwaysCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            AirwaySegment first = AirwaySegmentFactory.Make(AirwayType.HIGH, "N864");
            AirwaySegment second = AirwaySegmentFactory.Make(AirwayType.HIGH, "N862");
            AirwaySegment third = AirwaySegmentFactory.Make(AirwayType.HIGH, "L9");

            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                third,
                second,
                first
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_HIGH_AIRWAY;
        }
    }
}
