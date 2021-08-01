using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class AirspaceCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            OutputGroup group1 = new("1");
            OutputGroup group2 = new("2");
            outputGroups.AddGroupWithFiles(group1, new List<string>{"foo.txt"});
            outputGroups.AddGroupWithFiles(group2, new List<string>{"goo.txt"});

            Sectorline line1 = SectorlineFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            Sectorline line2 = SectorlineFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            CircleSectorline line3 = CircleSectorlineFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            
            Sector sector1 = SectorFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            Sector sector2 = SectorFactory.Make(definition: DefinitionFactory.Make("foo.txt"));

            CoordinationPoint point1 = CoordinationPointFactory.Make(false, definition: DefinitionFactory.Make("foo.txt"));
            CoordinationPoint point2 = CoordinationPointFactory.Make(true, definition: DefinitionFactory.Make("foo.txt"));
            CoordinationPoint point3 = CoordinationPointFactory.Make(false, definition: DefinitionFactory.Make("goo.txt"));

            sectorElements.Add(line1);
            sectorElements.Add(line2);
            sectorElements.Add(line3);
            sectorElements.Add(sector1);
            sectorElements.Add(sector2);
            sectorElements.Add(point1);
            sectorElements.Add(point2);
            sectorElements.Add(point3);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                line1,
                line3,
                line2,
                sector1,
                sector2,
                point1,
                point3,
                point2
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_AIRSPACE;
        }
    }
}
