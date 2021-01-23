using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class ColoursCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Colour first = ColourFactory.Make("red");
            Colour second = ColourFactory.Make("green");
            Colour third = ColourFactory.Make("blue");

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
            return OutputSectionKeys.SCT_COLOUR_DEFS;
        }
    }
}
