using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class IgnoreWhenFileExistsTest
    {
        [Fact]
        public void ItFiltersIfFileExists()
        {
            Assert.False(
                new IgnoreWhenFileExists("_TestData/IgnoreWhenFileExists/Foo.txt").Filter("Bar.txt")
            );
        }
        
        [Fact]
        public void ItDoesntFilterIfDoesntExist()
        {
            Assert.True(
                new IgnoreWhenFileExists("_TestData/IgnoreWhenFileExists/Bar.txt").Filter("Bar.txt")
            );
        }
    }
}
