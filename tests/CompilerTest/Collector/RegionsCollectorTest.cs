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
            OutputGroup group1 = new("1");
            group1.AddFile("foo.txt");
            OutputGroup group2 = new("2");
            group2.AddFile("goo.txt");
            this.outputGroups.Add(group1);
            this.outputGroups.Add(group2);

            Region first = RegionFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            Region second = RegionFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            Region third = RegionFactory.Make(definition: DefinitionFactory.Make("foo.txt"));

            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                first,
                third,
                second
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_REGIONS;
        }
    }
}
