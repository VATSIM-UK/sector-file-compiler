using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class FreetextCollectorTest: AbstractCollectorTestCase
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

            Freetext first = FreetextFactory.Make(DefinitionFactory.Make("foo.txt"));
            Freetext second = FreetextFactory.Make(DefinitionFactory.Make("goo.txt"));
            Freetext third = FreetextFactory.Make(DefinitionFactory.Make("foo.txt"));

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
            return OutputSectionKeys.ESE_FREETEXT;
        }
    }
}
