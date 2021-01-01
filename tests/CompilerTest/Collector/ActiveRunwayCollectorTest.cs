using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class ActiveRunwayCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            ActiveRunway first = ActiveRunwayFactory.Make("EGLL", "09L", 0);
            ActiveRunway second = ActiveRunwayFactory.Make("EGLL", "27L", 0);
            ActiveRunway third = ActiveRunwayFactory.Make("EGLL", "09L", 1);
            ActiveRunway fourth = ActiveRunwayFactory.Make("EGLL", "09R", 0);
            ActiveRunway fifth = ActiveRunwayFactory.Make("EGKK", "26L", 0);
            
            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);
            this.sectorElements.Add(fourth);
            this.sectorElements.Add(fifth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                fifth,
                first,
                third,
                fourth,
                second
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.RWY_ACTIVE_RUNWAYS;
        }
    }
}
