using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class SidStarsCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            SidStar first = SidStarFactory.Make(true, "EGKK", identifier: "LAM1X");
            SidStar second = SidStarFactory.Make(true, "EGKK", identifier: "ADMAG2X");
            SidStar third = SidStarFactory.Make(true, "EGKK", identifier: "ADMAG1X");
            SidStar fourth = SidStarFactory.Make(true, "EGCC", identifier: "LISTO1S");
            SidStar fifth = SidStarFactory.Make(false, "EGCC", identifier: "SANBA1R");
            SidStar sixth = SidStarFactory.Make(false, "EGCC", identifier: "LISTO1S");

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);
            sectorElements.Add(sixth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                fourth,
                first,
                second,
                third,
                fifth,
                sixth
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_SIDSSTARS;
        }
    }
}
