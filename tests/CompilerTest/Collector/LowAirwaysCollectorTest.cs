using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class LowAirwaysCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            AirwaySegment first = AirwaySegmentFactory.Make(identifier:"N864");
            AirwaySegment second = AirwaySegmentFactory.Make(identifier: "N862");
            AirwaySegment third = AirwaySegmentFactory.Make(identifier: "L9");

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
            return OutputSectionKeys.SCT_LOW_AIRWAY;
        }
    }
}
