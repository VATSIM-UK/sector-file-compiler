using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class GeoCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            OutputGroup group1 = new("1");
            OutputGroup group2 = new("2");
            outputGroups.AddGroupWithFiles(group1, new List<string>{"foo.txt"});
            outputGroups.AddGroupWithFiles(group2, new List<string>{"goo.txt"});

            Geo first = GeoFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            Geo second = GeoFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            Geo third = GeoFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            RunwayCentreline fourth = RunwayCentrelineFactory.Make();;
            FixedColourRunwayCentreline fifth = FixedColourRunwayCentrelineFactory.Make();;

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                first,
                third,
                second,
                fifth,
                fourth
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_GEO;
        }
    }
}
