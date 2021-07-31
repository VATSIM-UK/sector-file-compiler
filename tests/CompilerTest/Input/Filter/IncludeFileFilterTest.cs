using System.Collections.Generic;
using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class IncludeFileFilterTest
    {
        private readonly IncludeFileFilter filter;

        public IncludeFileFilterTest()
        {
            filter = new IncludeFileFilter(new List<string>{"Foo.txt"});
        }
        
        [Fact]
        public void ItFiltersOnNotMatch()
        {
            Assert.False(
                filter.Filter("_TestData/abc/NotFoo.txt")
            );
        }
        
        [Fact]
        public void ItDoesntFilterIfMatch()
        {
            Assert.True(
                filter.Filter("_TestData/abc/Foo.txt")
            );
        }
    }
}
