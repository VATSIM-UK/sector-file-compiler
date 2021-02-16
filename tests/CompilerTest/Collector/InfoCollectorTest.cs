using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class InfoCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Info info = InfoFactory.Make();

            sectorElements.Add(info);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                info
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_INFO;
        }
    }
}
