using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class RadarCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            RadarHole beforeFirst = RadarHoleFactory.Make();
            Radar first = RadarFactory.Make();
            Radar second = RadarFactory.Make();
            Radar third = RadarFactory.Make();
            RadarHole fourth = RadarHoleFactory.Make();
            RadarHole fifth = RadarHoleFactory.Make();

            sectorElements.Add(beforeFirst);
            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                first,
                second,
                third,
                beforeFirst,
                fourth,
                fifth
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_RADAR;
        }
    }
}
