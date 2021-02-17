using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class HighArtccCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            ArtccSegment first = ArtccSegmentFactory.Make(ArtccType.HIGH, "EGTT");
            ArtccSegment second = ArtccSegmentFactory.Make(ArtccType.HIGH, "EGPX");
            ArtccSegment third = ArtccSegmentFactory.Make(ArtccType.HIGH, "EISN");

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                second,
                first,
                third
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.SCT_ARTCC_HIGH;
        }
    }
}
