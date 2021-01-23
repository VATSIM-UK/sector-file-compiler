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

            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);
            this.sectorElements.Add(fourth);
            this.sectorElements.Add(fifth);
            this.sectorElements.Add(sixth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                fourth,
                third,
                second,
                first,
                sixth,
                fifth
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_SIDSSTARS;
        }
    }
}
