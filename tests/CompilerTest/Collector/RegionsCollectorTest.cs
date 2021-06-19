using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class RegionsCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Region first = RegionFactory.Make(name: "Manchester");
            Region second = RegionFactory.Make(name: "Bristol");
            Region third = RegionFactory.Make(name: "London City");

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                second,
                third,
                first
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_REGIONS;
        }
    }
}
