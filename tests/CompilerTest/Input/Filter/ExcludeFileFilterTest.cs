using System.Collections.Generic;
using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class ExcludeFileFilterTest
    {
        private readonly ExcludeFileFilter filter;

        public ExcludeFileFilterTest()
        {
            filter = new ExcludeFileFilter(new List<string>{"Foo.txt"});
        }
        
        [Fact]
        public void ItFiltersOnMatch()
        {
            Assert.False(
                filter.Filter("_TestData/abc/Foo.txt")
            );
        }
        
        [Fact]
        public void ItDoesntFilterIfNotMatch()
        {
            Assert.True(
                filter.Filter("_TestData/abc/NotFoo.txt")
            );
        }
        
        [Fact]
        public void ItOverridesBaseFilter()
        {
            Assert.False(
                ((IFileFilter) filter).Filter("_TestData/abc/Foo.txt")
            );
        }
    }
}
