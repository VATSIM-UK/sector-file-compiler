using System.Collections.Generic;
using System.Linq;
using Compiler.Model;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Collector
{
    abstract public class AbstractCollectorTestCase
    {
        protected SectorElementCollection sectorElements = new();

        protected OutputGroupRepository outputGroups = new();

        protected void AssertCollectedItems(IEnumerable<ICompilableElementProvider> expected)
        {
            IEnumerable<ICompilableElementProvider> actual = this.GetCollector().GetCompilableElements();
            Assert.Equal(expected.Count(), actual.Count());

            using IEnumerator<ICompilableElementProvider> expectedEnumerator = expected.GetEnumerator();
            using IEnumerator<ICompilableElementProvider> actualEnumerator = actual.GetEnumerator();
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Same(expectedEnumerator.Current, actualEnumerator.Current);
                expectedEnumerator.MoveNext();
                actualEnumerator.MoveNext();
            }
        }

        abstract protected ICompilableElementCollector GetCollector();
    }
}
