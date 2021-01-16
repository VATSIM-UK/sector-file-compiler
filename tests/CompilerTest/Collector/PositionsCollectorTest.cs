using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class PositionsCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            OutputGroup group1 = new("1");
            group1.AddFile("foo.txt");
            OutputGroup group2 = new("2");
            group2.AddFile("goo.txt");
            this.outputGroups.Add(group1);
            this.outputGroups.Add(group2);
            
            ControllerPosition first = ControllerPositionFactory.Make(
                order: PositionOrder.MENTOR_POSITION, 
                definition: DefinitionFactory.Make("goo.txt")
            );
            ControllerPosition second = ControllerPositionFactory.Make(
                order: PositionOrder.MENTOR_POSITION, 
                definition: DefinitionFactory.Make("foo.txt")
            );
            ControllerPosition third = ControllerPositionFactory.Make(
                order: PositionOrder.PRE_POSITION, 
                definition: DefinitionFactory.Make("foo.txt")
            );
            ControllerPosition fourth = ControllerPositionFactory.Make(
                order: PositionOrder.CONTROLLER_POSITION, 
                definition: DefinitionFactory.Make("goo.txt")
            );
            ControllerPosition fifth = ControllerPositionFactory.Make(
                order: PositionOrder.CONTROLLER_POSITION, 
                definition: DefinitionFactory.Make("foo.txt")
            );
            ControllerPosition sixth = ControllerPositionFactory.Make(
                order: PositionOrder.CONTROLLER_POSITION, 
                definition: DefinitionFactory.Make("foo.txt")
            );

            this.sectorElements.Add(first);
            this.sectorElements.Add(second);
            this.sectorElements.Add(third);
            this.sectorElements.Add(fourth);
            this.sectorElements.Add(fifth);
            this.sectorElements.Add(sixth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                third,
                fifth,
                sixth,
                fourth,
                second,
                first
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_POSITIONS;
        }
    }
}
