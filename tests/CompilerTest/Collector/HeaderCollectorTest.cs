﻿using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Collector
{
    public class HeaderCollectorTest: AbstractCollectorTestCase
    {
        [Fact]
        public void TestItReturnsElementsInOrder()
        {
            Header header = new Header(DefinitionFactory.Make(), new List<HeaderLine>());

            this.sectorElements.Add(header);

            IEnumerable<ICompilableElementProvider> expected = new List<ICompilableElementProvider>()
            {
                header
            };
            this.AssertCollectedItems(expected);
        }

        protected override OutputSectionKeys GetOutputSection()
        {
            return OutputSectionKeys.FILE_HEADER;
        }
    }
}
