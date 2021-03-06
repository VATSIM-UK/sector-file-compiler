﻿using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class FreetextCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            OutputGroup group1 = new("1");
            OutputGroup group2 = new("2");
            outputGroups.AddGroupWithFiles(group1, new List<string>{"foo.txt"});
            outputGroups.AddGroupWithFiles(group2, new List<string>{"goo.txt"});

            Freetext first = FreetextFactory.Make(DefinitionFactory.Make("foo.txt"));
            Freetext second = FreetextFactory.Make(DefinitionFactory.Make("goo.txt"));
            Freetext third = FreetextFactory.Make(DefinitionFactory.Make("foo.txt"));

            sectorElements.Add(first);
            sectorElements.Add(second);
            sectorElements.Add(third);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                first,
                third,
                second
            };
            AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.ESE_FREETEXT;
        }
    }
}
