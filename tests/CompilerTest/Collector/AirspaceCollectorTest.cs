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
            this.outputGroups.AddGroupWithFiles(group1, new List<string>{"foo.txt"});
            this.outputGroups.AddGroupWithFiles(group2, new List<string>{"goo.txt"});

            Sectorline line1 = SectorlineFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            Sectorline line2 = SectorlineFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            CircleSectorline line3 = CircleSectorlineFactory.Make(definition: DefinitionFactory.Make("foo.txt"));
            
            Sector sector1 = SectorFactory.Make(definition: DefinitionFactory.Make("goo.txt"));
            Sector sector2 = SectorFactory.Make(definition: DefinitionFactory.Make("foo.txt"));

            CoordinationPoint point1 = CoordinationPointFactory.Make(false, definition: DefinitionFactory.Make("foo.txt"));
            CoordinationPoint point2 = CoordinationPointFactory.Make(true, definition: DefinitionFactory.Make("foo.txt"));
            CoordinationPoint point3 = CoordinationPointFactory.Make(false, definition: DefinitionFactory.Make("goo.txt"));

            this.sectorElements.Add(line1);
            this.sectorElements.Add(line2);
            this.sectorElements.Add(line3);
            this.sectorElements.Add(sector1);
            this.sectorElements.Add(sector2);
            this.sectorElements.Add(point1);
            this.sectorElements.Add(point2);
            this.sectorElements.Add(point3);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                line1,
                line3,
                line2,
                sector2,
                sector1,
                point1,
                point3,
                point2
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_AIRSPACE;
        }
    }
}
