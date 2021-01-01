using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class LabelsCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            OutputGroup group1 = new OutputGroup("1");
            group1.AddFile("foo.txt");
            OutputGroup group2 = new OutputGroup("2");
            group2.AddFile("goo.txt");
            this.outputGroups.Add(group1);
            this.outputGroups.Add(group2);

            Label first = LabelFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            Label second = LabelFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            Label third = LabelFactory.Make(definition: DefinitionFactory.Make("foo.txt"));

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
            return OutputSectionKeys.SCT_LABELS;
        }
    }
}
