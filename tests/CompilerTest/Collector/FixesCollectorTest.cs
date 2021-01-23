using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class FixesCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Fix first = FixFactory.Make("DIKAS");
            Fix second = FixFactory.Make("BOMBO");
            Fix third = FixFactory.Make("MONTY");

            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                second,
                first,
                third
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_FIXES;
        }
    }
}
