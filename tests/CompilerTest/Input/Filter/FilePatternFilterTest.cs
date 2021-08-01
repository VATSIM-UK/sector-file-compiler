using System.Text.RegularExpressions;
using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class FilePatternFilterTest
    {
        private readonly FilePatternFilter filter;

        public FilePatternFilterTest()
        {
            filter = new FilePatternFilter(new Regex("Foo.txt"));
        }
        
        [Fact]
        public void ItFiltersOnNotMatchingPattern()
        {
            Assert.False(
                filter.Filter("_TestData/abc/Boo.txt")
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
