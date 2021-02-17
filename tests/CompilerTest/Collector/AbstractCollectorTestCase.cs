using System.Collections.Generic;
using System.Linq;
using Compiler.Collector;
using Compiler.Model;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Collector
{
    public abstract class AbstractCollectorTestCase
    {
        protected readonly SectorElementCollection sectorElements = new();

        protected readonly OutputGroupRepository outputGroups = new();

        protected void AssertCollectedItems(IEnumerable<ICompilableElementProvider> expected)
        {
            List<ICompilableElementProvider> actual = GetCollector().GetCompilableElements().ToList();
            List<ICompilableElementProvider> expectedList = expected.ToList();
            Assert.Equal(expectedList.Count, actual.Count);

            for (int i = 0; i < expectedList.Count(); i++)
            {
                Assert.Same(expectedList[i], actual[i]);
            }
        }

        protected ICompilableElementCollector GetCollector()
        {
            return new CompilableElementCollectorFactory(sectorElements, outputGroups)
                .GetCollectorForOutputSection(GetOutputSection());
        }

        abstract protected OutputSectionKeys GetOutputSection();
    }
}
