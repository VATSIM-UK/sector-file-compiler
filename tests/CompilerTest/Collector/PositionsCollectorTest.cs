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
            OutputGroup group2 = new("2");
            outputGroups.AddGroupWithFiles(group1, new List<string>{"foo.txt"});
            outputGroups.AddGroupWithFiles(group2, new List<string>{"goo.txt"});
            
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

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);
            sectorElements.Add(fourth);
            sectorElements.Add(fifth);
            sectorElements.Add(sixth);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                third,
                fifth,
                sixth,
                fourth,
                second,
                first
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_POSITIONS;
        }
    }
}
