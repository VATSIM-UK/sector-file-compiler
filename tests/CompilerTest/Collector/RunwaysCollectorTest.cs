using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class RunwaysCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Runway first = RunwayFactory.Make("EGKK", "08R");
            Runway second = RunwayFactory.Make("EGKK", "08L");
            Runway third = RunwayFactory.Make("EGKK", "26L");
            Runway fourth = RunwayFactory.Make("EGCC", "23R");
            Runway fifth = RunwayFactory.Make("EGGD", "09");

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                fourth,
                fifth,
                second,
                first,
                third
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_RUNWAY;
        }
    }
}
